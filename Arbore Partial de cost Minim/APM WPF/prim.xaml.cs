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
    /// Interaction logic for prim.xaml
    /// </summary>
    public partial class prim : UserControl
    {
        Graf graf;
        int started = 0;
        Nod nextSelected;
        int Nr, n, sum;
        Converter<Int32, string> conv = new Converter<int, string>(Convert.ToString);
        //List<Muchie> CoadaMuchii;
        List<Muchie> CoadaMuchii;

        List<Muchie> Rez = new List<Muchie>();
        List<int> NoduriSol;

        public prim()
        {


        }

        int cmp(Muchie a, Muchie b)
        {
            return a.dist.CompareTo(b.dist);
        }

        public prim(Graf g)
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


            CoadaMuchii = new List<Muchie>();
            Rez.Clear();


            WatchMuchii.ItemsSource = CoadaMuchii;

            WatchNoduri.Items.Refresh();
            WatchMuchii.Items.Refresh();
            WatchSelected.Items.Refresh();

            Nr = 0;
            sum = 0;
            this.WatchNr.Content = "Muchii selectate: 0";

            NoduriSol = new List<int>();
            if (n > 0)
                nextSelected = graf.noduri[0];
            addMuchii();

            prepare_next_step();
        }

        void addMuchii()
        {
            foreach (Muchie m in nextSelected.vec)
            {
                int y = numar(other(m));
              
                if (!NoduriSol.Contains(y))
                    CoadaMuchii.Add(m);
            }
            CoadaMuchii.Sort();
            WatchMuchii.Items.Refresh();
        }

        Nod other(Muchie muchie)
        {
            if (muchie.x == nextSelected)
                return muchie.y;
            else
                return muchie.x;
        }

        int numar(Nod nod)
        {
            return Int32.Parse(nod.Content.ToString());
        }

        Muchie selectMuchie()
        {
            int i;
            for (i = 0; i < CoadaMuchii.Count; i++)
                if (NoduriSol.Contains(numar(CoadaMuchii[i].x)) && NoduriSol.Contains(numar(CoadaMuchii[i].y)))
                {
                    CoadaMuchii.RemoveAt(i);
                    i--;
                }
                else break;
            return CoadaMuchii[i];
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


            if (NoduriSol.Count == 0)
            {
                NoduriSol.Add(1);
                Explicatii.Text = "Alegem ca nod de început nodul 1 și adăugăm la Heap toate muchiile care pornesc de aici\n";
            }
            else
            {
                Explicatii.Text = "Selectat fiind nodul " + nextSelected.Content.ToString() + " adaugam la Heap toate muchiile către noduri care nu fac parte din arborele curent\n";
            }



            Muchie m = selectMuchie();
            if (NoduriSol.Contains(numar(m.x)))
                nextSelected = m.y;
            else
                nextSelected = m.x;



            Explicatii.Text += "Muchia dintre nodurile " + m.Xstr + " si " + m.Ystr + " are cost minim asa ca selectam nodul " + nextSelected.Content.ToString() + " si adăugăm muchia la soluție"; 

        }

        void functie(object sender, EventArgs e)
        {
            if (n == 0)
            {
                System.Windows.MessageBox.Show("Nu există Noduri", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Nr == n - 1)
            {
                System.Windows.MessageBox.Show("Terminat cu succes", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (CoadaMuchii.Count == 0)
            {
                System.Windows.MessageBox.Show("Graful nu este conex", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Muchie m = CoadaMuchii[0];
           
            CoadaMuchii.RemoveAt(0);

            WatchSelected.Items.Refresh();
       
            #region update graph
          
            Nr++;
          
            int y = numar(nextSelected);

            NoduriSol.Add(y);
           
            Rez.Add(m);
            m.x.val = m.y.val = 1;
            
            addMuchii();
             
            sum += m.dist;
            WatchNr.Content = "Muchii selectate: " + Nr;

            VisualStateManager.GoToState(m, "selected", false);

            WatchMuchii.Items.Refresh();
            WatchNoduri.Items.Refresh();
            WatchSelected.Items.Refresh();
            WatchSelected.InvalidateVisual();

            #endregion

            prepare_next_step();
        }

        void derulare(object sender, EventArgs e)
        {
            while (Nr < n - 1 && CoadaMuchii.Count != 0)
                functie(sender, e);

        }

    }
}
