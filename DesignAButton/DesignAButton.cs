﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesignAButton
{
    class DesignAButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application application = new Application();
            application.Run(new DesignAButton());
        }

        DesignAButton()
        {
            Title = "Design a Button";
            
            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;
            Content = btn;

            StackPanel stack = new StackPanel();
            btn.Content = stack;

            stack.Children.Add(ZigZag(10));

            Uri uri = new Uri("pack://application:,,/book.png");
            BitmapImage bitmap = new BitmapImage(uri);
            Image img = new Image();
            img.Margin = new Thickness(0,10,0,0);
            img.Source = bitmap;
            img.Stretch = Stretch.None;
            stack.Children.Add(img);

            Label lbl = new Label();
            lbl.Content = "_Read books!";
            lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
            stack.Children.Add(lbl);

            stack.Children.Add(ZigZag(0));
        }

        Polyline ZigZag(int offset)
        {
            Polyline poly = new Polyline();
            poly.Stroke = SystemColors.ControlTextBrush;
            poly.Points = new PointCollection();

            for (int x= 0; x < 100; x++)
                poly.Points.Add(new Point(x, (x+offset) % 20));

            return poly;
        }


        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The button has been clicked", Title);
        }
    }
}
