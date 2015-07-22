using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace APM_WPF
{
    public partial class Graf
    {
        Canvas myCanvas;
        public List<Nod> noduri = new List<Nod>();
        public List<Muchie> muchii = new List<Muchie>();

        public Object MovingObject;
        double FirstX, FirstY;
        public int can_move = 1;
        public int can_edit = 1;
        
        public Graf(Canvas canvas)
        {
            myCanvas = canvas;
            myCanvas.PreviewMouseMove += this.MouseMove;
            myCanvas.PreviewMouseUp += this.MouseUp;
        }

        public Nod new_nod(Point epos)
        {
            Nod nod = new Nod();
          //  Nod.myCanvas = myCanvas;
            nod.PreviewMouseLeftButtonDown += this.MouseDown;
            nod.PreviewMouseLeftButtonUp += this.MouseUp;
            nod.PreviewMouseRightButtonUp += this.DeleteNod;
            nod.Content = noduri.Count + 1;
            Canvas.SetLeft(nod, 53);
            Canvas.SetTop(nod, 42);
            Canvas.SetZIndex(nod, noduri.Count + 51);

            MovingObject = nod;
            noduri.Add(nod);
            FirstX =  epos.X;
            FirstY =  epos.Y;

            nod.myCanvas = myCanvas;
            myCanvas.Children.Add(nod);
            
            return nod;
        }

        void MouseDown(object sender, MouseEventArgs e)
        {
            if (can_move == 0 || can_edit == 0) return;
            MovingObject = sender;
            FirstX = e.GetPosition(sender as Control).X;
            FirstY = e.GetPosition(sender as Control).Y;
        }

        void MouseUp(object sender, MouseEventArgs e)
        {
            MovingObject = null;
        }

        void DeleteNod(object sender, EventArgs e)
        {
            if (can_edit == 0) return;
            noduri.Remove(sender as Nod);
            (sender as Nod).Delete();
            refresh();
        }

        void MouseMove(object sender, MouseEventArgs e)
        {
            if (can_move == 0 || can_edit == 0) return;
            if (e.LeftButton == MouseButtonState.Pressed && MovingObject != null)
            {
                double newX, newY;
                newX = e.GetPosition(myCanvas).X - FirstX;
                newY = e.GetPosition(myCanvas).Y - FirstY;
                if(newX > 0 && newX < myCanvas.ActualWidth - 40)
                    (MovingObject as FrameworkElement).SetValue(Canvas.LeftProperty, newX);
                if(newY > 50 && newY < myCanvas.ActualHeight - 40)
                    (MovingObject as FrameworkElement).SetValue(Canvas.TopProperty, newY);

                (MovingObject as Nod).Actual_Pos();       
            }
        }

        public Muchie new_muchie(Nod n1, Nod n2, int dist = 0)
        {
            if (muchii.FindAll(muci => muci.x == n1 && muci.y == n2 || muci.x == n2 && muci.y == n1).Count != 0) return null;
            Muchie muc = new Muchie(n1, n2, dist, myCanvas, this);

            n1.vec.Add(muc);
            n2.vec.Add(muc);
            muchii.Add(muc);
            myCanvas.Children.Add(muc);

            muc.Actual_Pos();
        
            n1.IsEnabled = true;
            n2.IsEnabled = true;

            return muc;
        }

        public void refresh(object sender, SizeChangedEventArgs e)
        {
            refresh();
        }

        public void refresh(int mod = 0)
        {
            if(mod == 0)
            {
                MovingObject = null;
                for(int i = 0; i < noduri.Count; i++)
                {
                    noduri[i].IsEnabled = true;
                    if (Canvas.GetLeft(noduri[i]) + 40 > myCanvas.ActualWidth)
                        Canvas.SetLeft(noduri[i], myCanvas.ActualWidth - 40);
                    if (Canvas.GetTop(noduri[i]) + 40 > myCanvas.ActualHeight)
                        Canvas.SetTop(noduri[i], myCanvas.ActualHeight - 40);
                    noduri[i].Actual_Pos();
                    noduri[i].Content = i + 1;
                }

                for (int i = 0; i < muchii.Count; i++)
                    muchii[i].textBox.IsReadOnly = false;

                for(int i = 0; i < muchii.Count; i++)
                {
                    VisualStateManager.GoToState(muchii[i], "normal", false);
                }
            }
            else if(mod == 1) //kruskal
            {
                for (int i = 0; i < noduri.Count; i++)
                {
                    noduri[i].IsEnabled = false;
                    noduri[i].Actual_Pos();
                    noduri[i].Content = i + 1;

                    Color col = Color.FromRgb((byte)((noduri[i].val * 50)%175 + 75), (byte)((noduri[i].val * 37)%175 + 75), (byte)((noduri[i].val * 112)%175 + 75));

                    Ellipse eli = (Ellipse)noduri[i].Template.FindName("cerc", noduri[i]);
                    eli.Fill = new SolidColorBrush(col);
                    eli.Stroke = Brushes.Black;

                    noduri[i].ApplyTemplate();
                }
            }
        }

        public void AutoArange()
        {
            int n = noduri.Count;
            double teta = 2.0 * Math.PI/n;
            double R = Math.Min(myCanvas.ActualWidth / 3.0, myCanvas.ActualHeight/3.0);
            double my = myCanvas.ActualWidth / 2.0 -20;
            double mx = myCanvas.ActualHeight / 2.0 +10;

            for(int i = 0; i < n; i++)
            {
                double x, y;
                x = mx - R * Math.Cos(i * teta);
                y = my - R * Math.Sin(i * teta);

                noduri[i].SetValue(Canvas.LeftProperty, y);
                noduri[i].SetValue(Canvas.TopProperty, x);

                noduri[i].Actual_Pos();
            }
        }

        public void Sterge()
        {
            while(noduri.Count > 0)
            {
                Nod aux = noduri[0];
                noduri.Remove(aux);
                aux.Delete();
            }
            refresh();
        }
    }
}
