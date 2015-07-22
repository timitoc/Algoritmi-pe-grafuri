using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace APM_WPF
{
    /// <summary>
    /// Interaction logic for AlgorithmUI.xaml
    /// </summary>
    public partial class AlgorithmUI : Window
    {
        String teorie;
        LineCounter lineCounter;

        public AlgorithmUI()
        {
            InitializeComponent();
            myInit();
        }

        /**
         * Method for getting text-file encoding
         * Source: http://stackoverflow.com/questions/3825390/effective-way-to-find-any-files-encoding
         */
        public static Encoding GetEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            return Encoding.ASCII;
        }

        private void myInit()
        {
            usings.Text = "using System;" + "\n" + "using System.Collections.Generic;";
            for (int i = 1; i <= 30; i++)
                lineNumbering.Text += i + "\n";
            mainGrid.SizeChanged += mainGrid_SizeChanged;
            lineCounter = new LineCounter(0);
            Verifica_Codul();
        }

        void mainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = e.NewSize.Width;
            double height = e.NewSize.Height;
            lineCounter.Update(height);
            lineNumbering.Text = lineCounter.textBlock.Text;
        }

        private String getBoxString(RichTextBox textBox)
        {
            return new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd).Text;
        }

        private String getBoxString(TextBox textBox)
        {
            return textBox.Text;
        }

        private String buildCode()
        {
            String toR = "";
            toR += getBoxString(usings) + "\n";
            toR += System.IO.File.ReadAllText("assets/bucket1.txt");
            toR += getBoxString(start) + "\n}\n";
            toR += getBoxString(declarations);
            toR += "\n\t public void executa()\n\t{";
            toR += getBoxString(executa) + "\n";
            toR += System.IO.File.ReadAllText("assets/bucket2.txt");
            return toR;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (Verifica_Codul() == false) // nu este valid
            {
                MessageBox.Show("Codul are erori. Nu poate fi salvat !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            String totalCode = " ";
            if (teorie != null)
                totalCode += teorie;
            totalCode += "***";
            totalCode += buildCode();
            System.Windows.Forms.SaveFileDialog x = new System.Windows.Forms.SaveFileDialog();
            x.Filter = "Algorithm File (*.algo)|*.algo|Text File (*.txt)|*.txt";
            var result = x.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                String path = x.FileName;
                System.IO.File.WriteAllText(path, totalCode, Encoding.Unicode);
                MessageBox.Show("Algoritm salvat cu succes", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog x = new System.Windows.Forms.OpenFileDialog();
            x.Filter = "Algorithm File (*.algo)|*.algo|Text File (*.txt)|*.txt";
            if (x.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            String content = System.IO.File.ReadAllText(x.FileName, Encoding.Unicode);
            for (int i = 0; i < content.Length-8; i++)
                if (content.Substring(i, 8).Equals("***using"))
                {
                    try
                    {
                        teorie = content.Substring(0, i);
                        PopulateBoxes(content.Substring(i + 3));
                        if (teorie.Length > 1)
                            addTeorieVizualize();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Selected file is not a valid .algo file");
                    }
                    return;
                }
            MessageBox.Show("Selected file is not a valid .algo file");
        }

        private void Adauga_Teorie(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog x = new System.Windows.Forms.OpenFileDialog();
            x.Filter = "Text File (*.txt)|*.txt";
            if (x.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            teorie = File.ReadAllText(x.FileName, GetEncoding(x.FileName));
            addTeorieVizualize();
        }

        private void addTeorieVizualize()
        {
            ((MenuItem)bigMenu.Items.GetItemAt(4)).Visibility = System.Windows.Visibility.Visible;
        }

        private int hSize(TextBox text)
        {
            return text.Text.Split('\n').Length;
        }

        private int ConvertLine(int line)
        {
            int actLine = 0, heightParser = 0, toDel = 0;
            heightParser += hSize(usings);
            if (line <= heightParser)
                return line - toDel;
            toDel += 34 - 24;
            heightParser += hSize(start) + 34;
            if (line <= heightParser)
                return line - toDel;
            toDel += 0;
            heightParser += hSize(declarations)+1;
            if (line <= heightParser)
                return line - toDel;
            toDel += -1;
            heightParser += hSize(executa) + 2;
            if (line <= heightParser)
                return line - toDel;
            toDel += 8;
            heightParser += toDel;
            if (line <= heightParser)
                MessageBox.Show("Something terible happened");

            return line - toDel;
        }

        private void Verifica_Codul(object sender, RoutedEventArgs e)
        {
            Verifica_Codul();
        }

        private bool Verifica_Codul()
        {
            String code = buildCode();
            CompilerParameters compilerparams = new CompilerParameters();
            compilerparams.GenerateExecutable = compilerparams.GenerateInMemory = false;

            CompilerResults cR = (new CSharpCodeProvider()).CreateCompiler().CompileAssemblyFromSource(compilerparams, code);
            errorPanel.Children.Clear();
            if (cR.Errors.HasErrors)
            {           
                foreach (CompilerError error in cR.Errors)
                {
                    String s = String.Format("Line {0},{1}\t: {2}\n",
                           ConvertLine(error.Line), error.Column, error.ErrorText);
                    Label lab = new Label();
                    lab.Content = s;
                    lab.Foreground = Brushes.IndianRed;
                    lab.Padding = new Thickness(3, 3, 3, 3);
                    errorPanel.Children.Add(lab);
                    Separator sep = new Separator();
                    sep.Height = 4;
                    sep.Margin = new Thickness(0, 0, 0, 0);
                    errorPanel.Children.Add(sep);
                }
                return false;
            }
            else
            {
                Label lab = new Label();
                lab.Content = "The code is valid, no errors found !";
                lab.Foreground = Brushes.Green;
                lab.Padding = new Thickness(3, 3, 3, 3);
                errorPanel.Children.Add(lab);
                return true;
            }
        }

        private void PopulateBoxes(String content)
        {
            int i = 0, par;
            usings.Text = start.Text = declarations.Text = executa.Text = "";
            for (; i < content.Length; i++)
                if (content.Substring(i, 9).Equals("namespace"))
                    break;
                else
                    usings.Text += content[i];
            for (; i < content.Length; i++)
                if (content.Substring(i, 5).Equals("start"))
                {
                    while (content[i] != '{') i++;
                    i++;
                    break;
                }
            par = 1;
            for (; i < content.Length; i++)
            {
                if (content[i] == '{')
                    par++;
                else if (content[i] == '}')
                {
                    par--;
                    if (par == 0)
                    {
                        i++;
                        break;
                    }
                }
                start.Text += content[i];
            }
            for (; i < content.Length; i++)
            {
                if (content.Substring(i, 19).Equals("public void executa"))
                {
                    while (content[i] != '{') i++;
                    i++;
                    break;
                }
                declarations.Text += content[i];
            }
            par = 1;
            for (; i < content.Length; i++)
            {
                if (content[i] == '{')
                    par++;
                else if (content[i] == '}')
                {
                    par--;
                    if (par == 0)
                    {
                        i++;
                        break;
                    }
                }
                executa.Text += content[i];
            }
            clean(usings);
            clean(start);
            clean(declarations);
            clean(executa);
            MessageBox.Show("Algoritmul a fost reluat cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void clean(TextBox tB)
        {
            while (tB.Text.Length > 0 && tB.Text.Substring(tB.Text.Length - 1, 1) == "\n" || tB.Text.Substring(tB.Text.Length - 1, 1) == "\t" || tB.Text.Substring(tB.Text.Length - 1, 1) == " ")
                tB.Text = tB.Text.Substring(0, tB.Text.Length - 1);
            while (tB.Text.Length > 0 && tB.Text.Substring(0, 1) == "\n")
                tB.Text = tB.Text.Substring(1, tB.Text.Length - 1);
        }
    }
}
