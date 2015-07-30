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

    public partial class MainWindow : Window
    {
        FlowDocumentReader readST = new FlowDocumentReader();
        FlowDocumentReader readDR = new FlowDocumentReader();

        private double menItemWidth;

        public MainWindow()
        {
            InitializeComponent();

            Stanga.Children.Add(readST);
            Dreapta.Children.Add(readDR);

            readST.Visibility = System.Windows.Visibility.Collapsed;
            readDR.Visibility = System.Windows.Visibility.Collapsed;

            readDR.ViewingMode = FlowDocumentReaderViewingMode.Scroll;
            readST.ViewingMode = FlowDocumentReaderViewingMode.Scroll;

            Totul.SizeChanged += Totul_SizeChanged;

            string[] files = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\algorithms");
            foreach(string file in files)
                addMenuAlgorithm(file);
           
        }

        private Size getStringRenderSize(String s, MenuItem item)
        {
            var formated = new FormattedText(s, System.Globalization.CultureInfo.CurrentUICulture, System.Windows.FlowDirection.LeftToRight,
                new Typeface(item.FontFamily, item.FontStyle, item.FontWeight, item.FontStretch),
                item.FontSize,
                item.Foreground);
            return new Size(20+formated.Width, formated.Height);
        }

        private int cmp(MenuItem a, MenuItem b)
        {
            return  getStringRenderSize(a.Header.ToString(), a).Width.CompareTo(getStringRenderSize(b.Header.ToString(), b).Width);
        }

        void Totul_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Totul_SizeChanged(e.NewSize.Width);
        }

        void Totul_SizeChanged(double newWidth)
        {
            menItemWidth = newWidth / MainMenu.Items.Count;
            MenuItem[] items = new MenuItem[MainMenu.Items.Count];
            for (int i = 0; i < MainMenu.Items.Count; i++)
                items[i] = (MenuItem)MainMenu.Items.GetItemAt(i);
            Array.Sort(items, cmp);
            for (int i = 0; i < items.Length; i++)
            {
                MenuItem item = items[i];
                double wantedSize = getStringRenderSize(item.Header.ToString(), item).Width;
                if (menItemWidth < wantedSize)
                    item.Width = menItemWidth;
                else
                {
                    item.Width = wantedSize;
                    menItemWidth += (menItemWidth - wantedSize) / (MainMenu.Items.Count - i - 1);
                }

            }
        }

        private void addMenuAlgorithm(String path)
        {
            String pure = System.IO.Path.GetFileNameWithoutExtension(path);
            MenuItem bigMenuItem = new MenuItem();
            bigMenuItem.Header = pure;
            bigMenuItem.FontSize = 18;

            MenuItem demonstratie = new MenuItem();
            demonstratie.Header = "Demonstrație";
            demonstratie.Click += (sender, e) =>
                {
                    universal_demo(sender, e, path);
                };

            MenuItem teorie = new MenuItem();
            teorie.Header = "Teorie";
            teorie.Click += (sender, e) =>
                {
                    universal_teorie(sender, e, pure + "_Teorie");
                };

            MenuItem sterge = new MenuItem();
            sterge.Header = "Ștergere";
            sterge.Click += (object sender, RoutedEventArgs e) =>
                {
                    MainMenu.Items.Remove(bigMenuItem);
                    System.IO.File.Delete(path);
                    Totul_SizeChanged(Totul.ActualWidth);
                };


            bigMenuItem.Items.Add(teorie);
            bigMenuItem.Items.Add(demonstratie);
            bigMenuItem.Items.Add(sterge);

            MainMenu.Items.Add(bigMenuItem);

            FlowDocument newDoc = new FlowDocument();
            newDoc.Blocks.Add(new Paragraph(new Run(getTheory(System.IO.File.ReadAllText(path), System.IO.Path.GetFileNameWithoutExtension(path)))));

            Application.Current.Resources[pure + "_Teorie"] = newDoc;
        }

        private void Algoritm_Nou(object sender, EventArgs e)
        {
            new AlgorithmUI().Show();
        }

        String getTheory(String content, String name)
        {
            String toR = "";
            for (int i = 0; i < content.Length - 8; i++)
            {
                if (content.Substring(i, 8).Equals("***using"))
                    return toR;
                toR += content[i];
            }
            MessageBox.Show("S-au întâmpinat erori în încărcarea algoritmului \"" + name + "\".\nExistă erori în algoritm sau acesta nu respectă formatul .algo" +"\nEste posibil ca acesta să nu funcționeze corect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return toR;
        }

        private void Adauga_Algoritm(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog x = new System.Windows.Forms.OpenFileDialog();
            x.Filter = "Algorithm File (*.algo)|*.algo|Text File (*.txt)|*.txt";
            if (x.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String ext = System.IO.Path.GetFileNameWithoutExtension(x.FileName) + ".algo";
                String newPath = AppDomain.CurrentDomain.BaseDirectory + "\\algorithms\\" + ext;

                if (System.IO.File.Exists(newPath))
                {
                    
                    var res = MessageBox.Show("Exista deja un algoritm cu acelasi nume.\nDoriți să-l înlocuiți?", "Confirmă Adăugarea", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.No)
                        return;
                    
                    if (!validAlgo(x.FileName))
                    {
                        MessageBox.Show("Fisierul nu are un format .algo valid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
       
                    foreach (MenuItem mi in MainMenu.Items)
                    {
                        if (mi.Header.Equals(System.IO.Path.GetFileNameWithoutExtension(x.FileName)))
                        {
                            ((MenuItem)mi.Items[2]).RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
                            break;
                        }
                    }
                    System.IO.File.Copy(x.FileName, newPath, true);
                    addMenuAlgorithm(newPath);
                    Totul_SizeChanged(Totul.ActualWidth);
                    return;
                }
                if (!validAlgo(x.FileName))
                {
                    MessageBox.Show("Fisierul nu are un format .algo valid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                System.IO.File.Copy(x.FileName, newPath, true);

                addMenuAlgorithm(newPath);

                Totul_SizeChanged(Totul.ActualWidth);
            }   
        }

        private bool validAlgo(string newPath)
        {
            string content = System.IO.File.ReadAllText(newPath);
            for (int i = 0; i < content.Length-8; i++)
                if (content.Substring(i, 8).Equals("***using"))
                    return true;
            return false;
        }

        void kruskal_teorie(object sender, EventArgs e)
        {
            foreach (FrameworkElement f in Dreapta.Children)
                f.Visibility = System.Windows.Visibility.Collapsed;

            readDR.Document = Application.Current.Resources["Kruskal_Teorie"] as FlowDocument;
            readDR.Visibility = System.Windows.Visibility.Visible;
        }

        void prim_teorie(object sender, EventArgs e)
        {
            foreach (FrameworkElement f in Dreapta.Children)
                f.Visibility = System.Windows.Visibility.Collapsed;

            readDR.Document = Application.Current.Resources["Prim_Teorie"] as FlowDocument;
            readDR.Visibility = System.Windows.Visibility.Visible;
        }
        void universal_teorie(object sender, EventArgs e)
        {
            universal_teorie(sender, e, "yey_Teorie");
        }

        void universal_teorie(object sender, EventArgs e, String resource)
        {
            foreach (FrameworkElement f in Dreapta.Children)
                f.Visibility = System.Windows.Visibility.Collapsed;

            readDR.Document = Application.Current.Resources[resource] as FlowDocument;
            readDR.Visibility = System.Windows.Visibility.Visible;
        }

        void APM_Enunt(object sender, EventArgs e)
        {
            foreach (FrameworkElement f in Stanga.Children)
                f.Visibility = System.Windows.Visibility.Collapsed;

            readST.Document = Application.Current.Resources["Enunt"] as FlowDocument;
            readST.Visibility = System.Windows.Visibility.Visible;
        }

        void disjunct(object sender, EventArgs e)
        {
            foreach (FrameworkElement f in Stanga.Children)
                f.Visibility = System.Windows.Visibility.Collapsed;

            readST.Document = Application.Current.Resources["Disjunct"] as FlowDocument;
            readST.Visibility = System.Windows.Visibility.Visible;
        }
       
        void info_but(object sender, EventArgs e)
        {
            if (info_bloc.Visibility == System.Windows.Visibility.Visible)
                info_bloc.Visibility = System.Windows.Visibility.Collapsed;
            else
            {
                info_bloc.Visibility = System.Windows.Visibility.Visible;
                GrafGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
     

        void kruskal_demo(object sender, EventArgs e)
        {
            foreach (FrameworkElement f in Stanga.Children)
                f.Visibility = System.Windows.Visibility.Collapsed;
            foreach (FrameworkElement f in Dreapta.Children)
                f.Visibility = System.Windows.Visibility.Collapsed;

            GrafGrid.Visibility = System.Windows.Visibility.Visible;
            kruskal watch = new kruskal(GrafGrid.graf);
            (watch.meniu.Items[0] as MenuItem).Click += GrafGrid.nu_construi;
            Dreapta.Children.Add(watch);
            
        }

        void prim_demo(object sender, EventArgs e)
        {
            foreach (FrameworkElement f in Stanga.Children)
                f.Visibility = System.Windows.Visibility.Collapsed;
            foreach (FrameworkElement f in Dreapta.Children)
                f.Visibility = System.Windows.Visibility.Collapsed;

            GrafGrid.Visibility = System.Windows.Visibility.Visible;
            prim watch = new prim(GrafGrid.graf);
            (watch.meniu.Items[0] as MenuItem).Click += GrafGrid.nu_construi;
            Dreapta.Children.Add(watch);

        }

        void universal_demo(object sender, EventArgs e, String path)
        {
            foreach (FrameworkElement f in Stanga.Children)
                f.Visibility = System.Windows.Visibility.Collapsed;
            foreach (FrameworkElement f in Dreapta.Children)
                f.Visibility = System.Windows.Visibility.Collapsed;
            String codePath = path;
            GrafGrid.Visibility = System.Windows.Visibility.Visible;
            Universal watch = new Universal(GrafGrid.graf, codePath);
            (watch.meniu.Items[0] as MenuItem).Click += GrafGrid.nu_construi;
            Dreapta.Children.Add(watch);
        }

        void Editor_graf(object sender, EventArgs e)
        {
            foreach (FrameworkElement f in Stanga.Children)
                f.Visibility = System.Windows.Visibility.Collapsed;

            GrafGrid.Visibility = System.Windows.Visibility.Visible;
        }
        
    }
}
