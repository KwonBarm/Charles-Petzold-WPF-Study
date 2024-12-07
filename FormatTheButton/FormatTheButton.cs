using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace FormatTheButton
{
    class FormatTheButton : Window
    {
        Run? runButton = null;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new FormatTheButton());
        }

        public FormatTheButton()
        {
            Title = "Format the Button";

            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.MouseEnter += Btn_MouseEnter;
            btn.MouseLeave += Btn_MouseLeave;
            Content = btn;

            TextBlock txt = new TextBlock();
            txt.FontSize = 24;
            txt.TextAlignment = TextAlignment.Center;
            btn.Content = txt;

            txt.Inlines.Add(new Italic(new Run("Click")));
            txt.Inlines.Add(" The ");
            txt.Inlines.Add(runButton = new Run("button"));
            txt.Inlines.Add(new LineBreak());
            txt.Inlines.Add("to launch the ");
            txt.Inlines.Add(new Bold(new Run("rocket")));

        }

        private void Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            runButton.Foreground = SystemColors.ControlTextBrush;
            runButton.Text = "button";
        }

        private void Btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            runButton.Foreground = Brushes.Red;
            runButton.Text = "진입";
        }
    }
}
