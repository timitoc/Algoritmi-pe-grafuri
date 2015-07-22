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
	/// <summary>
	/// Interaction logic for Muchie.xaml
	/// </summary>
	public partial class Muchie : UserControl, IComparable<Muchie>
	{
        public Nod x, y;
        public int dist;
        Canvas myCanvas;
        Graf graf;

		public Muchie(Nod xx = null, Nod yy = null, int dist = 0, Canvas myC = null, Graf gr = null)
		{
			this.InitializeComponent();
            textBox.MaxLength = 3;
            textBox.TextChanged += new TextChangedEventHandler(this.actualizare);
            textBox.Text = "" + dist;
            myCanvas = myC;
            graf = gr;
            x = xx;
            y = yy;
		}

        public Muchie()
        {
            this.InitializeComponent();
        }


        public void Delete()
        {
            x.vec.RemoveAll(muc => muc.x == this.x && muc.y == this.y);
            y.vec.RemoveAll(muc => muc.x == this.x && muc.y == this.y);
            myCanvas.Children.Remove(this);
            graf.muchii.RemoveAll(muc => muc.x == this.x && muc.y == this.y);
        }

        void actualizare(object sender, EventArgs e)
        {
            try
            {
                if (textBox.Text == "" && textBox.IsFocused || textBox.Text == "-" && textBox.IsFocused)
                    dist = 0;
                else dist = int.Parse(textBox.Text);
            }
            catch
            {
                MessageBox.Show("Costul unei muchii trebuie să fie un număr întreg", "Eroare Parsare", MessageBoxButton.OK, MessageBoxImage.Error);
                textBox.Text = "0";
                dist = 0;
            }
        }

        

        public void Actual_Pos()
        {
            if (Canvas.GetTop(x) > Canvas.GetTop(y))
            {
                Nod aux;
                aux = x;
                x = y;
                y = aux;
            }

            double marjax = 0, marjay = 0;

            Height = Canvas.GetTop(y) - Canvas.GetTop(x);
            if (Height < 20) { marjay = (20 - Height) / 2; Height = 20; }
            

            if (Canvas.GetLeft(x) < Canvas.GetLeft(y))
            {
                System.Windows.VisualStateManager.GoToState(this, "normalpos", false);
                Width = (Canvas.GetLeft(y) - Canvas.GetLeft(x));

                if (Width < 20) { marjax = (30 - Width) / 2;  Width = 20; }
                Canvas.SetLeft(this, Canvas.GetLeft(x) + 20  - marjax);
                Canvas.SetTop(this, Canvas.GetTop(x) + 20 - marjay);
            }
            else
            {
                System.Windows.VisualStateManager.GoToState(this, "reversepos", false);
                Width = (Canvas.GetLeft(x) - Canvas.GetLeft(y));
                if (Width < 20) { marjax = (30 - Width) / 2; Width = 20; }
                Canvas.SetTop(this, Canvas.GetTop(x) + 20);
                Canvas.SetLeft(this, Canvas.GetLeft(y) + 20);
            }
        }

        public int CompareTo(Muchie b)
        {
            if (dist >= b.dist) 
                return 1;
            return -1;
        }

        public string Xstr { get { return x.Content.ToString(); } }
        public string Ystr { get { return y.Content.ToString(); } }
        public string Diststr { get { return dist.ToString(); } }

	}
}