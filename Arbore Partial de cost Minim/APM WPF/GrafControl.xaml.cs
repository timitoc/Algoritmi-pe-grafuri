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
using System.IO;
using Microsoft.Win32;


namespace APM_WPF
{
    /// <summary>
    /// Interaction logic for GrafControl.xaml
    /// </summary>
    public partial class GrafControl : UserControl
    {
        public Graf graf;
        Nod n1 = null;
        int construct = 0, muchiestate = 0;

        public GrafControl()
        {
            this.InitializeComponent();
            graf = new Graf(graf_grid);
            graf.can_edit = 0;

            graf_grid.SizeChanged += graf.refresh;

            muchie1.textBox.Text = "nou";
            muchie1.textBox.IsReadOnly = true;
            muchie1.MouseDown += add_muchie;
            invbut.Click += add_muchie;

            graf_nod_but.PreviewMouseDown += new_nod;

            toolbox.Visibility = System.Windows.Visibility.Collapsed;
        }

        void construieste(object sender, EventArgs e)
        {
            if(construct == 0)
            {
                construct = 1;
                graf.can_edit = 1;
                toolbox.Visibility = System.Windows.Visibility.Visible;
            }
            else if (construct == 1)
            {
                construct = 0;
                graf.can_edit = 0;
                toolbox.Visibility = System.Windows.Visibility.Collapsed;
            }

            graf.refresh();
        }

        public void nu_construi(object sender = null, EventArgs e = null)
        {
            construct = 0;
            graf.can_edit = 0;
            toolbox.Visibility = System.Windows.Visibility.Collapsed;
            graf.refresh(1);
        }

        void add_muchie(object sender, EventArgs e)
        {
            if (construct == 0) return;

            if (muchiestate == 0)
            {
                muchiestate = 1;
                VisualStateManager.GoToState(muchie1, "selected", false);
                graf.can_move = 0;
            }
            else
            {
                muchiestate = 0;
                VisualStateManager.GoToState(muchie1, "normal", false);
                graf.can_move = 1;
                graf.refresh();
            }
        }

        void new_nod(object sender, MouseEventArgs e)
        {
            if (construct == 0) return;
            if (muchiestate == 1)
                add_muchie(sender, e);
            n1 = null;
            Nod b = graf.new_nod(e.GetPosition(graf_nod_but));
            b.Click += draw_muchie;
        }

        void draw_muchie(object sender, EventArgs e)
        {
            if (construct == 0 || muchiestate == 0) return;

            if (n1 == null)
            {
                n1 = sender as Nod;
                n1.IsEnabled = false;
                return;
            }

            Nod n2 = sender as Nod;
            graf.new_muchie(n1, n2);

            n1 = null;
            add_muchie(sender, e);
        }


        void AutoArange(object sender, EventArgs e)
        {
            graf.AutoArange();
        }

        private void Sterge(object sender, EventArgs e)
        {
            graf.Sterge();
        }

        private void generate(object sender, EventArgs e)
        {
            graf.Sterge();
            if (construct == 1) construieste(null, null);

            int n = 7;

            for (int i = 0; i < n; i++)
            {
               Nod aux = graf.new_nod(new Point(100, 100));
               aux.Click += this.draw_muchie;
            }
                

            graf.AutoArange();

            int cost;
            Random r = new Random();

            for(int i = 0; i < n; i++)
                for(int j = i+1; j < n; j++)
                {
                    cost = r.Next(-20, 30);
                    if (cost > 0)
                        graf.new_muchie(graf.noduri[i], graf.noduri[j], cost);
                }

            graf.refresh();
        }

        private void save(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "graf";
            dialog.Filter = "Graf file (*.graf)|*.graf|Text File (*.txt)|*.txt|In file (*.in) |*.in";

            if(dialog.ShowDialog() == true)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(dialog.FileName, false))
                {
                    file.WriteLine("" + graf.noduri.Count + " " + graf.muchii.Count);
                    foreach (Muchie m in graf.muchii)
                        file.WriteLine(m.Xstr + " " + m.Ystr + " " +m.Diststr);
                }

            }
        }

        private void Load(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Graf file (*.graf)|*.graf|Text File (*.txt)|*.txt|In file (*.in) |*.in"; ;

            string aux;

            if(dialog.ShowDialog() == true)
            {
                graf.Sterge();
                if (construct == 1) construieste(null, null);
                StreamReader ST = new StreamReader(dialog.FileName);

                try
                {
                    aux = ST.ReadLine();
                    int n, m, x, y, d;

                    n = Convert.ToInt32(aux.Split(' ')[0]);
                    m = Convert.ToInt32(aux.Split(' ')[1]);
                    for (int i = 0; i < n; i++)
                    {
                        Nod nou = graf.new_nod(new Point(100, 100));
                        nou.Click += this.draw_muchie;
                    }
                        
                    
                    for(int i = 0; i < m; i++)
                    {
                        aux = ST.ReadLine();
                        x = Convert.ToInt32(aux.Split(' ')[0]);
                        y = Convert.ToInt32(aux.Split(' ')[1]);
                        d = Convert.ToInt32(aux.Split(' ')[2]);

                        graf.new_muchie(graf.noduri[x - 1], graf.noduri[y - 1], d);
                    }
                    
                    graf.AutoArange();
                }
                catch { System.Windows.MessageBox.Show("Eroare deschidere fisier", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error); }
           
            }
        }

    }
}
