using System.Windows;
using System.Windows.Controls;

namespace UriDialog
{
    internal class NavigateTheWeb : Window
    {
        Frame frm;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new NavigateTheWeb());
        }

        public NavigateTheWeb()
        {
            Title = "Navigate the Web";

            frm = new Frame();
            Content = frm;

            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            UriDialog dig = new UriDialog();
            dig.Owner = this;
            dig.Text = "http://";
            dig.ShowDialog();

            try
            {
                frm.Source = new Uri(dig.Text);
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, Title);
            }
        }
    }
}
