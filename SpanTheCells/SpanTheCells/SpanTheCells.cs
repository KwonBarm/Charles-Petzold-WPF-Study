using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shell;

namespace SpanTheCells
{
    class SpanTheCells : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SpanTheCells());

        }

        public SpanTheCells()
        {
            Title = "Span the Cells";
            SizeToContent = SizeToContent.WidthAndHeight;

            Grid grid = new Grid();
            grid.Margin = new Thickness(5);
            grid.Background = Brushes.Yellow;
            grid.ShowGridLines = true;
            Content = grid;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //WindowStyle = WindowStyle.None;
            //WindowChrome windowChrome = new WindowChrome();
            //windowChrome.CaptionHeight = 30;

            for (int i = 0; i < 6; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowdef);
            }

            for (int i = 0; i < 4; i++)
            {
                ColumnDefinition coldef = new ColumnDefinition();

                if (i == 1)
                    coldef.Width = new GridLength(100, GridUnitType.Star);
                else
                    coldef.Width = GridLength.Auto;

                grid.ColumnDefinitions.Add(coldef);
            }

            string[] astrLabel =
            {
                "_First name:",
                "_Last name:",
                "_Social security number:",
                "_Credit card number:",
                "_Other personal stuff:",
            };

            for(int i = 0; i < astrLabel.Length; i++)
            {
                Label lbl = new Label();
                lbl.Content = astrLabel[i];
                lbl.VerticalContentAlignment = VerticalAlignment.Center;

                grid.Children.Add(lbl);
                Grid.SetRow(lbl, i);
                Grid.SetColumn(lbl, 0);

                TextBox txtbox = new TextBox();
                txtbox.Margin = new Thickness(5);

                grid.Children.Add(txtbox);
                Grid.SetRow(txtbox, i);
                Grid.SetColumn(txtbox, 1);
                Grid.SetColumnSpan(txtbox, 3);
            }

            Button btn = new Button();
            btn.Content = "Submit";
            btn.Margin = new Thickness(5);
            btn.IsDefault = true;
            btn.Click += delegate (object sender, RoutedEventArgs e) { this.Close(); MessageBox.Show("Submit"); };
            grid.Children.Add(btn);
            Grid.SetRow(btn, 5);
            Grid.SetColumn(btn, 2);

            btn = new Button();
            btn.Content = "Cancel";
            btn.Margin = new Thickness(5);
            btn.IsCancel = true;
            btn.Click += delegate (object sender, RoutedEventArgs e) { this.Close(); MessageBox.Show("Cancel"); };
            grid.Children.Add(btn);
            Grid.SetRow(btn, 5);
            Grid.SetColumn(btn, 3);

            if (grid.Children[1] is TextBox)
                grid.Children[1].Focus();

            GridSplitter split = new GridSplitter();
            split.Width = 6;
            split.Background = Brushes.Red;
            grid.Children.Add(split);
            Grid.SetRow(split, 3);
            Grid.SetColumn(split, 2);   
        }

      
    }
}
