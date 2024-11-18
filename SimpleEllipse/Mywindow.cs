using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleEllipse
{
    internal class Mywindow : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new Mywindow());
        }

        public Mywindow()
        {
            var elip = new SimpleEllipse();
            Content = elip;
            MaxWidth = 1200;
            MaxHeight = 700;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            elip.HorizontalAlignment = HorizontalAlignment.Center;
            elip.VerticalAlignment = VerticalAlignment.Center;
            elip.Width = 26;
            elip.Height = 26;
        }
    }
}
