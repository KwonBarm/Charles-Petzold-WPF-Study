using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShowMyFace
{
    class ShowMyFace : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ShowMyFace());
        }

        public ShowMyFace()
        {
            Title = "Show My Face";

            Uri uri = new Uri("https://letsenhance.io/static/8f5e523ee6b2479e26ecc91b9c25261e/1015f/MainAfter.jpg");
            //Uri uri = new Uri(Path.Combine(Environment.GetEnvironmentVariable("windir"), "test.png"));
            BitmapImage bitmap = new BitmapImage(uri);

            Image img = new Image();
            img.Source = bitmap;
            //img.Stretch = Stretch.Fill;
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.Margin = new Thickness(10);
            //img.Opacity = 0.5;
            img.LayoutTransform = new RotateTransform(45);

            Content = img;
            SizeToContent = SizeToContent.WidthAndHeight;
            Background = new LinearGradientBrush(Colors.Red, Colors.Blue, new Point(0, 0), new Point(1, 1));
        }
    }
}
