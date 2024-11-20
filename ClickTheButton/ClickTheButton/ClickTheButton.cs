using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClickTheButton
{
    class ClickTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ClickTheButton());
        }

        public ClickTheButton()
        {
            Title = "Click the Button";

            TextBlock text = new TextBlock();
            text.Text = "Test";
            Button btn = new Button();
            //btn.Content = "_Click me, please!";
            btn.Content = text;
            btn.Click += Btn_Click;
            //btn.Focus();
            btn.IsDefault = true;
            btn.IsCancel = true;
            btn.Margin = new Thickness(96);
            btn.Padding = new Thickness(96);
            btn.FontSize = 96;
            btn.FontFamily = new FontFamily("Times New Roman");
            btn.Background = Brushes.AliceBlue;
            btn.Foreground = Brushes.DarkSalmon;
            btn.BorderBrush = Brushes.Magenta;
            SizeToContent = SizeToContent.WidthAndHeight;

            Content = btn;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The button has been Clicked and all is well.", "알림");
        }
    }
}
