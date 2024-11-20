using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageTheButton
{
    internal class ImageTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ImageTheButton());
        }

        public ImageTheButton()
        {
            Title = "Image the Button";

            Uri uri = new Uri("pack://application:,,/Image/Munch.jpg");
            BitmapImage bitImage = new BitmapImage(uri);

            Image image = new Image();
            image.Source = bitImage;
            image.Stretch = Stretch.Fill;
            //image.Source = ImagSource

            Button btn = new Button();
            btn.Height = 200;
            btn.Width = 200;
            btn.Content = image;
            btn.Click += Btn_Click;
            btn.IsCancel = true;
            Content = btn;
            
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("text");
        }
    }
}
