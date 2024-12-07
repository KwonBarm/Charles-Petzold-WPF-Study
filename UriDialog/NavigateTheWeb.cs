using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UriDialog
{
    class NavigateTheWeb : Window
    {
        Frame frm;

        [STAThread]
        public static void Main()
        {
            Application application = new Application();
            application.Run(new NavigateTheWeb());
        }

        public NavigateTheWeb()
        {
            Title = "Navigate the Web";

            frm = new Frame();
            Content = frm;

            Loaded += NavigateTheWeb_Loaded;
        }

        private void NavigateTheWeb_Loaded(object sender, RoutedEventArgs e)
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
