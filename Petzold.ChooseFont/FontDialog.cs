using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ChooseFont
{
    internal class FontDialog : Window
    {
        TextBoxWithLister boxFamiliy, boxStyle, boxWeight, boxStretch, boxSize;
        Label lblDisplay;
        bool isUpdateSuppressed = true;

        public Typeface Typeface
        {
            set
            {
                if (boxFamiliy.Contains(value.FontFamily))
                    boxFamiliy.SelectedItem = value.FontFamily;
                else
                    boxFamiliy.SelectedIndex = 0;

                if (boxStyle.Contains(value.Style))
                    boxStyle.SelectedItem = value.Style;
                else
                    boxStyle.SelectedIndex = 0;

                if (boxWeight.Contains(value.Weight))
                    boxWeight.SelectedItem = value.Weight;
                else
                    boxWeight.SelectedIndex = 0;

                if (boxStretch.Contains(value.Stretch))
                    boxStretch.SelectedItem = value.Stretch;
                else
                    boxStretch.SelectedIndex = 0;
            }

            get
            {
                return new Typeface((FontFamily)boxFamiliy.SelectedItem, (FontStyle)boxStyle.SelectedItem, (FontWeight)boxWeight.SelectedItem, (FontStretch)boxStretch.SelectedItem);
            }
        }

        public double FaceSize
        {
            set
            {
                double size = 0.75 * value;
                boxSize.Text = size.ToString();

                if(!boxSize.Contains(size))
                    boxSize.Insert(0, size);

                boxSize.SelectedItem = size;
            }
            get
            {
                double size;

                if (!double.TryParse(boxSize.Text, out size))
                    size = 8.25;

                return size / 0.75;
            }
        }

        // FontDialog 생성자
        public FontDialog()
        {
            Title = "Font";
            ShowInTaskbar = false;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.NoResize;

            // 윈도우 Content를 위해 3개 행을 가진 Grid 생성
            Grid gridMain = new Grid();
            Content = gridMain;
            gridMain.ShowGridLines = true;

            // TextBoxWithLister Control을 위한 행
            RowDefinition rowdef = new RowDefinition();
            rowdef.Height = new GridLength(200, GridUnitType.Pixel);
            gridMain.RowDefinitions.Add(rowdef);

            // 샘플 Text를 위한 행
            rowdef = new RowDefinition();
            rowdef.Height = new GridLength(150, GridUnitType.Pixel);
            gridMain.RowDefinitions.Add(rowdef);

            // Button을 위한 행
            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            gridMain.RowDefinitions.Add(rowdef);

            // gridMain을 위한 열
            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = new GridLength(650, GridUnitType.Pixel);
            gridMain.ColumnDefinitions.Add(coldef);

            // TextBoxWithLister Control을 위해 2개 행과 5개 열을 가진 Grid 생성
            Grid gridBoxes = new Grid();
            gridBoxes.ShowGridLines = true;
            gridMain.Children.Add(gridBoxes);

            // Label을 위한 행
            rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            gridBoxes.RowDefinitions.Add(rowdef);

            // EditBoxWithLister Control을 위한 행
            rowdef = new RowDefinition();
            rowdef.Height = new GridLength(100, GridUnitType.Star);
            gridBoxes.RowDefinitions.Add(rowdef);

            // FontFamily를 위한 첫번째 열
            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(175, GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            // FontStyle을 위한 두번째 열
            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            // FontWeight을 위한 세번째 열
            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            // FontStretch을 위한 네번째 열
            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100,GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            // FontSize을 위한 다섯번째 열
            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(75,GridUnitType.Star);
            gridBoxes.ColumnDefinitions.Add(coldef);

            // TextBoxWithLister Control과 FontFamily Label을 생성
            Label lbl = new Label();
            lbl.Content = "Font Family";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 0);

            boxFamiliy = new TextBoxWithLister();
            boxFamiliy.IsReadOnly = true;
            boxFamiliy.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxFamiliy);
            Grid.SetRow(boxFamiliy, 1);
            Grid.SetColumn(boxFamiliy, 0);
        }



        private void OkOnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new FontDialog());
        }
    }
}
