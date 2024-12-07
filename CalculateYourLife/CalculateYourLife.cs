using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CalculateYourLife
{
    class CalculateYourLife : Window
    {
        TextBox txtboxBegin, txtboxEnd;
        Label lblLifeYears;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CalculateYourLife());
        }

        public CalculateYourLife()
        {
            Title = "Calculate Your Life";
            SizeToContent = SizeToContent.WidthAndHeight;
            //ResizeMode = ResizeMode.CanMinimize;

            Grid grid = new Grid();
            Content = grid;
            grid.Background = Brushes.AliceBlue;
            grid.ShowGridLines = true;

            for (int i = 0; i < 3; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowdef);
            }

            for (int i = 0; i < 2; i++)
            {
                ColumnDefinition coldef = new ColumnDefinition();
                coldef.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(coldef);
            }

            Label lbl = new Label();
            lbl.Content = "Begin Date: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl,0);
            Grid.SetColumn(lbl,0);

            txtboxBegin = new TextBox();
            txtboxBegin.Text = new DateTime(1982, 11, 10).ToShortDateString();
            txtboxBegin.TextChanged += TextBoxOnTextChanged;
            //txtboxBegin.VerticalContentAlignment = VerticalAlignment.Center;
            grid.Children.Add(txtboxBegin);
            Grid.SetRow(txtboxBegin,0);
            Grid.SetColumn(txtboxBegin,1);

            lbl = new Label();
            lbl.Content = "End Date: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl,1);
            Grid.SetColumn(lbl,0);

            txtboxEnd = new TextBox();
            txtboxEnd.TextChanged += TextBoxOnTextChanged;
            grid.Children.Add(txtboxEnd);
            Grid.SetRow(txtboxEnd,1);
            Grid.SetColumn(txtboxEnd, 1);

            lbl = new Label();
            lbl.Content = "Life Years: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl,2);
            
            lblLifeYears = new Label();
            grid.Children.Add(lblLifeYears);
            Grid.SetRow(lblLifeYears, 2);
            Grid.SetColumn(lblLifeYears, 1);

            Thickness thickness = new Thickness(5);
            grid.Margin = thickness;

            foreach(Control ctrl in grid.Children)
                ctrl.Margin = thickness;

            txtboxBegin.Focus();
            txtboxEnd.Text = DateTime.Now.ToShortDateString();
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime dtBeg, dtEnd;

            if(DateTime.TryParse(txtboxBegin.Text, out dtBeg) && DateTime.TryParse(txtboxEnd.Text, out dtEnd))
            {
                int iYear = dtEnd.Year = dtBeg.Year;
                int iMonths = dtEnd.Month = dtBeg.Month;
                int iDays = dtEnd.Day = dtBeg.Day;

                if(iDays < 0)
                {
                    iDays += Date
                }
            }
        }
    }
}
