using Petzold.RenderTheBetterEllips;
using System.Windows;
using System.Windows.Media;

namespace EllipseWithChild
{
    class TestWindows : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new TestWindows());
        }

        public TestWindows()
        {
            Title = "Test";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 500;
            Height = 500;
            //SizeToContent = SizeToContent.WidthAndHeight;

            BetterEllipse elip = new BetterEllipse();
            elip.Width = 700;
            elip.Height = 200;
            //elip.Fill = Brushes.Red;
            elip.Stroke = new Pen(Brushes.Blue, 20);

            Content = elip;
        }
    }
}
