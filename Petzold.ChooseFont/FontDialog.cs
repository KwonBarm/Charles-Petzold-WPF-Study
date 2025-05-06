using System.Reflection.Emit;
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
        }
    }
}
