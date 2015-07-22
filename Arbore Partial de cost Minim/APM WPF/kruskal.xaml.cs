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
    /// Interaction logic for kruskal.xaml
    /// </summary>
    public partial class kruskal : UserControl
    {
        Graf graf;
        int started = 0;
        int Nr, n, sum;
        Converter<Int32, string> conv = new Converter<int, string>(Convert.ToString);
        List<Muchie> CoadaMuchii;
        List<Muchie> Rez = new List<Muchie>();

        public kruskal()
        {
            
            
        }

        int cmp(Muchie a, Muchie b)
        {
            return a.dist.CompareTo(b.dist);
        }

        public kruskal(Graf g)
        {
            this.InitializeComponent();
            graf = g;

            Rez = new List<Muchie>();

            WatchSelected.ItemsSource = Rez;
            WatchNoduri.ItemsSource = graf.noduri;

        }

        void setup(object sender = null, EventArgs e = null)
        {
            n = graf.noduri.Count;
            for (int i = 0; i < n; i++)
            {
                graf.noduri[i].val = i + 1;
                graf.noduri[i].Disable();
                graf.noduri[i].Update();
            }

            for (int i = 0; i < graf.muchii.Count; i++)
                graf.muchii[i].textBox.IsReadOnly = true;

            graf.muchii.Sort(cmp);

            
            CoadaMuchii = new List<Muchie>(graf.muchii);

            WatchMuchii.ItemsSource = CoadaMuchii;
            WatchNoduri.Items.Refresh();
            Rez.Clear();
            WatchSelected.Items.Refresh();

            Nr = 0;
            sum = 0;
            this.WatchNr.Content = "Muchii selectate: 0";

            prepare_next_step();
        }

        void prepare_next_step()
        {
            graf.refresh(1);

            if (Nr == n - 1)
                System.Windows.MessageBox.Show("Terminat cu succes", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

            if (n == 0) { Explicatii.Text = "Introduceți graful."; return; }

            if (Nr == n - 1)
            {
                Explicatii.Text = "S-a obținut arborele parțial de cost minim pentru graful dat:\n";
                Explicatii.Text += "Suma costurilor este: " + sum;
                return;
            }

            if (graf.muchii.Count == 0) { Explicatii.Text = "Nu există muchii - graful nu este conex!"; return; }
            if (CoadaMuchii.Count == 0) { Explicatii.Text = "Graful nu este conex!"; return; }

            

            Explicatii.Text = "Luăm următoarea muchie și verificăm dacă vârfurile sunt sau nu în același arbore:";
            if(CoadaMuchii[0].x.val == CoadaMuchii[0].y.val)
            {
                Explicatii.Text += "\n    Nodurile " + CoadaMuchii[0].Xstr + " și " + CoadaMuchii[0].Ystr + "   fac parte din același arbore. \n    Prin urmare trecem la următoarea muchie din coadă.";
            }
            else
            {
                Explicatii.Text += "\n    Nodurile " + CoadaMuchii[0].Xstr + " și " + CoadaMuchii[0].Ystr + "   fac parte din arbori distincți. \n    Prin urmare selectăm muchia în soluție și unim arborii.";
            }

        }

        void functie(object sender, EventArgs e)
        { 
            if(n == 0)
            {
                System.Windows.MessageBox.Show("Nu există Noduri", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Nr == n - 1)
            {
                System.Windows.MessageBox.Show("Terminat cu succes", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if(CoadaMuchii.Count == 0)
            {
                System.Windows.MessageBox.Show("Graful nu este conex", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Muchie m = CoadaMuchii[0];
            CoadaMuchii.RemoveAt(0);

            if(m.x.val != m.y.val)
            {
                Nr++;
                sum += m.dist;
                WatchNr.Content = "Muchii selectate: " + Nr;

                VisualStateManager.GoToState(m, "selected", false);
                int aux = m.y.val;
                Rez.Add(m);

                for (int i = 0; i < n; i++)
                    if (graf.noduri[i].val == aux)
                        graf.noduri[i].val = m.x.val;
            }

            WatchMuchii.Items.Refresh();
            WatchNoduri.Items.Refresh();
            WatchSelected.Items.Refresh();

            
            prepare_next_step();
        }

        void derulare(object sender, EventArgs e)
        {
            while (Nr < n - 1 && CoadaMuchii.Count != 0)
                functie(sender, e);
            
        }

    }
}
