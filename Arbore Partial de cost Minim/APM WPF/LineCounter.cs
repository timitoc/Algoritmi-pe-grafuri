using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace APM_WPF
{
    class LineCounter
    {
        private const double lineSize = 16.022385178320;
      
        private double currentHeight = 0;
        private int lastI = -1;
        public TextBlock textBlock;

        public LineCounter(double height)
        {
            Update(height);
            textBlock = new TextBlock();
        }

        private void addLine()
        {
            lastI++;
            currentHeight += lineSize;
            if (lastI <= 0) return;
            if (lastI == 1) textBlock.Text += lastI.ToString();
            else textBlock.Text += "\n" + lastI.ToString();
        }

        private void deleteLine()
        {
            currentHeight -= lineSize;
            int sz = 1 + lastI.ToString().Length;
            textBlock.Text = textBlock.Text.Remove(textBlock.Text.Length - sz);
            lastI--;
        }

        public void Update(double newHeight)
        {
            if (currentHeight == newHeight)
                return;
            if (currentHeight < newHeight)
            {
                int g = (int)Math.Round((newHeight - currentHeight) / lineSize);
                for (int i = 0; i < g; i++)
                    addLine();
            }
            else
            {
                int g = (int)Math.Round((currentHeight - newHeight) / lineSize);
                for (int i = 0; i < g+2; i++)
                    deleteLine();
            }
     
        }

    }
}
