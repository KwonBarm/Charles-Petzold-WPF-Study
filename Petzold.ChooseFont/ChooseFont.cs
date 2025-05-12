using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ChooseFont
{
    internal class ChooseFont : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ChooseFont());
        }

        public ChooseFont()
        {
            Title = "Choose Font";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 300;
            Height = 200;

            Button btn = new Button();
            btn.Content = Title;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;
            Content = btn;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            FontDialog dig = new FontDialog();
            dig.Owner = this;

            // Window의 FontDialog 속성을 설정
            dig.Typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);

            dig.FaceSize = FontSize;

            if (dig.ShowDialog().GetValueOrDefault())
            {
                // FontDialog에서 윈도우 속성을 설정
                FontFamily = dig.Typeface.FontFamily;
                FontStyle = dig.Typeface.Style;
                FontWeight = dig.Typeface.Weight;
                FontStretch = dig.Typeface.Stretch;
                FontSize = dig.FaceSize;
            }
        }
    }
}
