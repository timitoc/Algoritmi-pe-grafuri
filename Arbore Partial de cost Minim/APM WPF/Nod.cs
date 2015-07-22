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
    /*<summary>
        Clasa nod.
     </summary>
    */



	public partial class Nod : Button
	{
        public List<Muchie> vec = new List<Muchie>();
        public Canvas myCanvas;
        public int val, info;
        public bool viz;

        public Nod()
        {
            
           // DefaultStyleKeyProperty.OverrideMetadata(typeof(Nod), new FrameworkPropertyMetadata(typeof(Nod)));
            Template = (ControlTemplate)FindResource("ButtonControlRound");
            Height = 40;
            Width = 40;
            AllowDrop = true;
            Cursor = Cursors.SizeAll;
            val = 5;
            
        }

        public Nod(Nod x)
        {
            Template = (ControlTemplate)FindResource("ButtonControlRound");

            Height = 40;
            Width = 40;
            AllowDrop = true;
            Cursor = Cursors.SizeAll;

            this.ApplyTemplate();
            vec = x.vec;
            myCanvas = x.myCanvas;
            val = x.val;
            Content = x.Content;

        }

        public void Delete(object sender = null, MouseEventArgs e = null)
        {
            while(vec.Count > 0)
                vec[0].Delete();

            myCanvas.Children.Remove(this);
        }

        public void Actual_Pos()
        {

            for (int i = 0; i < vec.Count; i++)
                vec[i].Actual_Pos();
        }

        public void Disable()
        {
            VisualStateManager.GoToState(this, "disabled", false);
        }

        public void Update()
        {
            if(this.IsEnabled  == false)
            {
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0, 0));
            }
        }

        public int num()
        {
            return (int)Content;
        }

        public string valstr
        {
            get { return val.ToString(); }
        }

        public string infostr
        {
            get { return info.ToString(); }
        }


	}

}
