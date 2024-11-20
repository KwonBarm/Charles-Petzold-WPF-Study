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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Grid 생성
            Grid grid = new Grid();
            grid.Background = Brushes.SkyBlue;
            grid.ShowGridLines = true;
            Content = grid;

            // 행과 열의 정의
            for(int i =0; i < 3; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                //rowdef.Height = new GridLength(50);
                grid.RowDefinitions.Add(rowdef);
            }

            for(int i = 0; i < 2; i++)
            {
                ColumnDefinition coldef = new ColumnDefinition();
                coldef.Width = GridLength.Auto;
                //coldef.Width = new GridLength(50);
                grid.ColumnDefinitions.Add(coldef);
            }

            Label lbl = new Label();
            lbl.Content = "Begin Date: ";
            grid.Children.Add(lbl);
            Grid.SetColumn(lbl, 0);
            Grid.SetRow(lbl, 0);

            txtboxBegin = new TextBox();
            txtboxBegin.Text = new DateTime(1982, 11, 10).ToShortDateString();
            txtboxBegin.TextChanged += TextBoxOnChanged;
            txtboxBegin.VerticalContentAlignment = VerticalAlignment.Center;
            grid.Children.Add(txtboxBegin);
            Grid.SetRow(txtboxBegin, 0);
            Grid.SetColumn(txtboxBegin, 1);

            lbl = new Label();
            lbl.Content = "End Date: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 1);
            Grid.SetColumn(lbl, 0);

            txtboxEnd = new TextBox();
            txtboxEnd.TextChanged += TextBoxOnChanged;
            txtboxEnd.VerticalContentAlignment = VerticalAlignment.Center;
            grid.Children.Add(txtboxEnd);
            Grid.SetRow(txtboxEnd, 1);
            Grid.SetColumn(txtboxEnd, 1);

            lbl = new Label();
            lbl.Content = "Life Years: ";
            grid.Children.Add(lbl);
            Grid.SetRow(lbl, 2);
            Grid.SetColumn(lbl, 0);

            lblLifeYears = new Label();
            grid.Children.Add(lblLifeYears);
            Grid.SetRow(lblLifeYears, 2);
            Grid.SetColumn(lblLifeYears, 1);

            Thickness thick = new Thickness(5);
            grid.Margin = thick;

            foreach (Control ctrl in grid.Children)
                ctrl.Margin = thick;

            txtboxBegin.Focus();
            txtboxEnd.Text = DateTime.Now.ToShortDateString();
        }

        private void TextBoxOnChanged(object sender, TextChangedEventArgs e)
        {
            DateTime dtBeg, dtEnd;

            if(DateTime.TryParse(txtboxBegin.Text, out dtBeg) && DateTime.TryParse(txtboxEnd.Text, out dtEnd))
            {
                int iYears = dtEnd.Year - dtBeg.Year;
                int iMonth = dtEnd.Month - dtBeg.Month;
                int iDays = dtEnd.Day - dtBeg.Day;

                if (iDays < 0)
                {
                    iDays += DateTime.DaysInMonth(dtEnd.Year, 1+(dtEnd.Month + 10) % 12);
                    iMonth -= 1;
                }
                if(iMonth < 0)
                {
                    iMonth += 12;
                    iYears -= 1;
                }

                lblLifeYears.Content =
                    String.Format("{0} year{1}, {2} month{3}, {4} day{5}",
                    iYears, iYears == 1 ? "" : "s",
                    iMonth, iMonth == 1 ? "" : "s",
                    iDays, iDays == 1 ? "" : "s");
            }
            else
            {
                lblLifeYears.Content = "";
            }
        }
    }
}
