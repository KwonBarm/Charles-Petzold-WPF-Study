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
    class ImageTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ImageTheButton());
        }

        public ImageTheButton()
        {
            Title = "Image the button";

            Uri uri = new Uri("pack://application:,,/Image/munch.jpg");

            BitmapImage bitmap = new BitmapImage(uri);

            Image img = new Image();
            img.Source = bitmap;
            img.Stretch = Stretch.None;

            Button btn = new Button();
            btn.Content = img;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += Btn_Click;
            btn.IsCancel = true;
            btn.IsDefault = true;

            Content = btn;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("꺄악", Title);
        }
    }
}
