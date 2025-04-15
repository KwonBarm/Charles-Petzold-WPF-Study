using System.Windows;
using System.Windows.Controls;

namespace Petzold.PrintWithMargins
{
    class PrintWithMargins : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PrintWithMargins());
        }

        public PrintWithMargins()
        {


            Button btnok = new Button();
            btnok.Content = "OK";
            btnok.Click += btnOnClick;
            btnok.Margin = new Thickness(12);

            Content = btnok;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 300;
            Height = 200;
        }

        private void btnOnClick(object sender, RoutedEventArgs e)
        {
            Window win = new PageMarginsDialog();
            win.Owner = this;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            win.ShowDialog();
        }
    }
}
