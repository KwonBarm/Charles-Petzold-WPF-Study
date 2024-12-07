using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintTheButton
{
    class PaintTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new PaintTheButton());
        }

        public PaintTheButton()
        {
            Title = "Paint the Button";

            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.BorderThickness = new Thickness(1);
            Content = btn;

            Canvas canv = new Canvas();
            canv.Width = 144;
            canv.Height = 144;
            btn.Content = canv;

            Rectangle rect = new Rectangle();
            rect.Width = canv.Width;
            rect.Height = canv.Height;
            //rect.Width = 100;
            //rect.Height = 100;
            rect.RadiusX = 24;
            rect.RadiusY = 24;
            rect.Fill = Brushes.Blue;
            canv.Children.Add(rect);
            Canvas.SetLeft(rect,0);
            Canvas.SetTop(rect,0);

            Polygon polygon = new Polygon();
            polygon.Fill = Brushes.Yellow;
            polygon.Points = new PointCollection();

            for(int i = 0; i<5; i++)
            {
                double angle = i * 4 * Math.PI / 5;
                Point pt = new Point(48 * Math.Sin(angle), -48 * Math.Cos(angle));
                polygon.Points.Add(pt);
            }

            canv.Children.Add(polygon);
            Canvas.SetLeft(polygon,canv.Width/2);
            Canvas.SetTop(polygon,canv.Height/2);
        }
    }
}
