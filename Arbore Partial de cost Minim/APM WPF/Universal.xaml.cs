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
    /// Interaction logic for universal.xaml
    /// </summary>
    public partial class Universal : UserControl
    {
 
        Algorithm algorithm;
        Graf graf;
        String codePath;
        int n;
      

        public Universal(Graf graf, string codePath)
        {
            this.InitializeComponent();
            this.graf = graf;
            this.codePath = codePath;
        }

        int cmp(Muchie a, Muchie b)
        {
            return a.dist.CompareTo(b.dist);
        }

        private String Body(String path)
        {
            String content = System.IO.File.ReadAllText(path, AlgorithmUI.GetEncoding(path));
            for (int i = 0; i < content.Length-8; i++)
                if (content.Substring(i, 8).Equals("***using"))
                    return content.Substring(i + 3);
            return content;
        }

        public void setup(object sender = null, EventArgs e = null)
        {
            n = graf.noduri.Count;
            if (n == 0)
            {
                MessageBox.Show("Introduceți un graf");
                return;
            }
            for (int i = 0; i < n; i++)
            {
                graf.noduri[i].val = i + 1;
                graf.noduri[i].info = i + 1;
                graf.noduri[i].Disable();
                graf.noduri[i].Update();
            }

            for (int i = 0; i < graf.muchii.Count; i++)
                graf.muchii[i].textBox.IsReadOnly = true;

            graf.muchii.Sort(cmp);

            try
            {
                algorithm = new Algorithm(graf, Body(codePath));
            }
            catch
            {
                MessageBox.Show("Algoritmul nu a putut fi setat.\nExistă erori în algoritm sau acesta nu respectă formatul .algo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            WatchNoduri.ItemsSource = graf.noduri;
            updateData();
        }

        public void functie(object sender = null, EventArgs e = null)
        {
            if (algorithm == null)
            {
                MessageBox.Show("Algoritmul nu a fost încă setat", "Apăsați butonul \"Setup\"", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            try
            {
                if (algorithm.isFinished())
                    return;
                algorithm.callExecute();
                updateData();
                if (algorithm.isFinished())
                {
                    MessageBox.Show((String)algorithm.getValue("alerta"));
                }
            }
            catch
            {
                MessageBox.Show("S-au întâmpinat erori în execuția algoritmului.\nExistă erori în algoritm sau acesta nu respectă formatul .algo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void updateData()
        {
            WatchMuchii.ItemsSource = algorithm.getWatchMuchii();
            WatchSelected.ItemsSource = algorithm.getWatchSelected();
            Explicatii.Text = algorithm.getExplicatii();
            int[] nodesInfo = algorithm.getNodesInfo();
            for (int i = 1; i <= n; i++)
                graf.noduri[i - 1].info = nodesInfo[i];
            WatchNoduri.Items.Refresh();
            WatchNr.Content = "Muchii selectate: " + algorithm.getWatchSelected().Count;

            List<KeyValuePair<int, int>> colorChanges = algorithm.getColorChanges();
            foreach (var change in colorChanges)
            {
                graf.noduri[change.Value - 1].val = graf.noduri[change.Key - 1].val;
            }
            graf.refresh(1);
            algorithm.eraseColorChanges();
        }

        public void derulare(object sender = null, EventArgs e = null)
        {
            if (algorithm == null)
            {
                MessageBox.Show("Algoritmul nu a fost încă setat", "Apăsați butonul \"Setup\"", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            try
            {
                int count = 0, power = 100;
                while (true)
                {
                    while (!algorithm.isFinished() && count < power)
                    {
                        functie();
                        count++;
                    }
                    if (algorithm.isFinished())
                        return;
                    var result = MessageBox.Show("Funcția \"executa\" a fost rulată deja de " + count + " ori. Se poate ca aceasta să fi intrat intr-o buclă infinită. Doriți să continuați?", " Doriți să continuați?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.No)
                        return;
                    power *= 10;
                }
                
            }
            catch
            {
                MessageBox.Show("S-au întâmpinat erori în execuția algoritmului.\nExistă erori în algoritm sau acesta nu respectă formatul .algo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }   
        }
    }
}
