using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GetMedieval
{
    internal class GetMedieval : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new GetMedieval());
        }

        public GetMedieval()
        {
            Title = "Get Medieval";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 500;
            Height = 300;

            MedievalButton btn = new MedievalButton();
            btn.Text = "Click this button";
            btn.FontSize = 24;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Padding = new Thickness(5, 20, 5, 20);
            btn.Knock += ButtonKnock;
            //btn.Width = 50;

            Content = btn;


        }

        private void ButtonKnock(object sender, RoutedEventArgs e)
        {
            MedievalButton btn = e.Source as MedievalButton;
            MessageBox.Show("The button labeled \"" + btn.Text + "\" has been knocked.",Title);
        }
    }
}
