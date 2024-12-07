using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SetFontSizeProperty
{
    internal class SetFontSizeProperty : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SetFontSizeProperty());
        }

        public SetFontSizeProperty()
        {
            Title = "Set FontSize Property";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            FontSize = 16;
            double[] fontsizes = { 8, 16, 32 };

            Grid gird = new Grid();
            Content = gird;

            for (int i = 0; i < 2; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                gird.RowDefinitions.Add(row);
            }

            for (int i = 0; i < fontsizes.Length; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                gird.ColumnDefinitions.Add(col);
            }

            for (int i = 0; i < fontsizes.Length; i++)
            {
                Button btn = new Button();
                btn.Content = new TextBlock(new Run("Set window FontSize to " + fontsizes[i]));
                btn.Tag = fontsizes[i];
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Click += WindowFontSizeOnClick;
                gird.Children.Add(btn);
                Grid.SetRow(btn, 0);
                Grid.SetColumn(btn, i);

                btn = new Button();
                btn.Content = new TextBlock(new Run("Set button Fontsize to " + fontsizes[i]));
                btn.Tag = fontsizes[i];
                btn.HorizontalAlignment = HorizontalAlignment.Center;
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Click += ButtonFontSizeOnClick;
                gird.Children.Add(btn);
                Grid.SetRow(btn,1);
                Grid.SetColumn(btn,i);
            }
        }

        private void ButtonFontSizeOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.FontSize = (double)btn.Tag; 
        }

        private void WindowFontSizeOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            FontSize = (double)btn.Tag;

        }
    }
}
