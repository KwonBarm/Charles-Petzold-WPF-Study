using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ChooseFont
{
    internal class FontDialog : Window
    {
        TextBoxWithLister boxFamily, boxStyle, boxWeight, boxStretch, boxSize;
        Label lblDisplay;
        bool isUpdateSuppressed = true;

        public Typeface Typeface
        {
            set
            {
                if (boxFamily.Contains(value.FontFamily))
                    boxFamily.SelectedItem = value.FontFamily;
                else
                    boxFamily.SelectedIndex = 0;

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
                return new Typeface((FontFamily)boxFamily.SelectedItem, (FontStyle)boxStyle.SelectedItem, (FontWeight)boxWeight.SelectedItem, (FontStretch)boxStretch.SelectedItem);
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
            //ShowInTaskbar = false;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.NoResize;

            // 윈도우 Content를 위해 3개 행을 가진 Grid 생성
            Grid gridMain = new Grid();
            Content = gridMain;
            //gridMain.ShowGridLines = true;

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
            //gridBoxes.ShowGridLines = true;
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

            boxFamily = new TextBoxWithLister();
            boxFamily.IsReadOnly = true;
            boxFamily.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxFamily);
            Grid.SetRow(boxFamily, 1);
            Grid.SetColumn(boxFamily, 0);

            // TextBoxWithLister Control과 FontStyle Label을 생성
            lbl = new Label();
            lbl.Content = "Font Style";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 1);

            boxStyle = new TextBoxWithLister();
            boxStyle.IsReadOnly = true;
            boxStyle.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxStyle);
            Grid.SetRow(boxStyle, 1);
            Grid.SetColumn(boxStyle, 1);

            // TextBoxWithLister Control과 FontWeight Label을 생성
            lbl = new Label();
            lbl.Content = "Font Weight";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 2);

            boxWeight = new TextBoxWithLister();
            boxWeight.IsReadOnly = true;
            boxWeight.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxWeight);
            Grid.SetRow(boxWeight, 1);
            Grid.SetColumn(boxWeight, 2);

            // TextBoxWithLister Control과 FontStretch Label을 생성
            lbl = new Label();
            lbl.Content = "Font Stretch";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 3);

            boxStretch = new TextBoxWithLister();
            boxStretch.IsReadOnly = true;
            boxStretch.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxStretch);
            Grid.SetRow(boxStretch, 1);
            Grid.SetColumn(boxStretch, 3);

            // TextBoxWithLister Control과 FontSize Label을 생성
            lbl = new Label();
            lbl.Content = "Font Size";
            lbl.Margin = new Thickness(12, 12, 12, 0);
            gridBoxes.Children.Add(lbl);
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 4);

            boxSize = new TextBoxWithLister();
            boxSize.Margin = new Thickness(12, 0, 12, 12);
            gridBoxes.Children.Add(boxSize);
            Grid.SetRow(boxSize, 1);
            Grid.SetColumn(boxSize, 4);

            // 샘플 Text를 보여주기 위한 Label 생성
            lblDisplay = new Label();
            lblDisplay.Content = "AaBbCc XxYyZz 012345";
            lblDisplay.HorizontalContentAlignment = HorizontalAlignment.Center;
            lblDisplay.VerticalContentAlignment = VerticalAlignment.Center;
            gridMain.Children.Add(lblDisplay);
            Grid.SetRow(lblDisplay, 1);

            // Button을 위해 5개의 열을 가진 Grid를 생성
            Grid gridButtons = new Grid();
            gridMain.Children.Add(gridButtons);
            Grid.SetRow(gridButtons, 2);

            for (int i = 0; i < 5; i++)
                gridButtons.ColumnDefinitions.Add(new ColumnDefinition());

            // Ok 버튼
            Button btn = new Button();
            btn.Content = "OK";
            btn.IsDefault = true;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.MinWidth = 60;
            btn.Margin = new Thickness(12);
            btn.Click += OkOnClick;
            gridButtons.Children.Add(btn);
            Grid.SetColumn(btn, 1);

            // Cancel 버튼
            btn = new Button();
            btn.Content = "Cancel";
            btn.IsCancel = true;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.MinWidth = 60;
            btn.Margin = new Thickness(12);
            gridButtons.Children.Add(btn);
            Grid.SetColumn(btn, 3);

            // 시스템 FontFamily로 FontFamily Box를 초기화
            foreach (FontFamily fam in Fonts.SystemFontFamilies)
                boxFamily.Add(fam);

            // FontSize Box를 초기화
            double[] ptsizes = new double[]
            {
                8, 9.0, 10.0, 11.0, 12.0, 14.0, 16.0, 18.0,
                20.0, 22.0, 24.0, 26.0, 28.0, 36.0, 48.0,
                72.0
            };

            foreach(double ptsize in ptsizes)
                boxSize.Add(ptsize);

            // EventHandler 연결
            boxFamily.SelectionChanged += FamilyOnSelectionChanged;
            boxStyle.SelectionChanged += StyleOnSelectionChanged;
            boxWeight.SelectionChanged += StyleOnSelectionChanged;
            boxStretch.SelectionChanged += StyleOnSelectionChanged;
            boxSize.TextChanged += SizeOnTextChanged;

            // 현재 Windows의 FontFamily, FontStyle, FontWeight, FontStretch, FontSize를 Typeface, FaceSize 속성에 할당
            Typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);
            FaceSize = FontSize;

            // boxFamily를 키보드 포커스가 가능하게 하고 포커스를 줌
            boxFamily.Focus();

            // 샘플 텍스트를 수정할 수 있게 함
            isUpdateSuppressed = false;
            UpdateSample();
        }

        // FontFamily Box에서 FontFamily를 선택하면 발생하는 SelectionChanged 이벤트 핸들러
        private void FamilyOnSelectionChanged(object? sender, EventArgs e)
        {
            // 선택한 FontFamily를 구함
            FontFamily fontFamily = (FontFamily)boxFamily.SelectedItem;

            // 이전 스타일, 웨이트, 스트레치를 저장
            // 이 값을 이 메서드가 처음 불릴 때는 null
            FontStyle? fontStylePrevious = (FontStyle?)boxStyle.SelectedItem;
            FontWeight? fontWeightPrevious = (FontWeight?)boxWeight.SelectedItem;
            FontStretch? fontStretchPrevious = (FontStretch?)boxStretch.SelectedItem;

            // 샘플이 보이지 않게 함
            isUpdateSuppressed = true;

            // FontStyle, FontWeight, FontStretch Box를 지움
            boxStyle.Clear();
            boxWeight.Clear();
            boxStretch.Clear();

            // 선택된 FontFamily의 typefaces에 대해서 루프를 수행
            // FontFamily의 FamilyTypefaces 속성은 FontFamily의 모든 스타일을 포함, 예를 들어 FontFamily안에 FontStyle, FontWeight, FontStretch가 다 포함되어 있음
            foreach (FamilyTypeface familyTypeface in fontFamily.FamilyTypefaces)
            {
                // boxStyle에 스타일을 추가(Normal이 가장 상위에 위치)
                if (!boxStyle.Contains(familyTypeface.Style))
                {
                    if (familyTypeface.Style == FontStyles.Normal)
                        boxStyle.Insert(0, familyTypeface.Style);
                    else
                        boxStyle.Add(familyTypeface.Style);
                }

                // boxWeight에 웨이트를 추가(Normal이 가장 상위에 위치)
                if (!boxWeight.Contains(familyTypeface.Weight))
                {
                    if(familyTypeface.Weight == FontWeights.Normal)
                        boxWeight.Insert(0, familyTypeface.Weight);
                    else
                        boxWeight.Add(familyTypeface.Weight);
                }

                // boxStretch에 스트레치를 추가(Normal이 가장 상위에 위치)
                if (!boxStretch.Contains(familyTypeface.Stretch))
                {
                    if(familyTypeface.Stretch == FontStretches.Normal)
                        boxStretch.Insert(0, familyTypeface.Stretch);
                    else
                        boxStretch.Add(familyTypeface.Stretch);
                }
            }

            // boxStyle에 선택 항목을 설정
            if (boxStyle.Contains(fontStylePrevious))
                boxStyle.SelectedItem = fontStylePrevious;
            else
                boxStyle.SelectedIndex = 0;

            // boxWeight에 선택 항목을 설정
            if (boxWeight.Contains(fontWeightPrevious))
                boxWeight.SelectedItem = fontWeightPrevious;
            else
                boxWeight.SelectedIndex = 0;

            // boxStretch에 선택 항목을 설정
            if (boxStretch.Contains(fontStretchPrevious))
                boxStretch.SelectedItem = fontStretchPrevious;
            else
                boxStretch.SelectedIndex = 0;

            // 샘플 수정이 가능하게 하고 샘플을 갱신
            isUpdateSuppressed = false;
            UpdateSample();
        }

        // FontStyle, FontWeight, FontStretch Box에 대한 SelectionChanged 이벤트 핸들러
        private void StyleOnSelectionChanged(object? sender, EventArgs e)
        {
            UpdateSample();
        }

        // 크기 박스에 대한 TextChanged 이벤트 핸들러
        private void SizeOnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSample();
        }

        // 샘플 Text를 선택한 FontFamily, FontStyle, FontWeight, FontStretch, FontSize에 맞게 수정
        private void UpdateSample()
        {
            if(isUpdateSuppressed)
                return;

            lblDisplay.FontFamily = (FontFamily)boxFamily.SelectedItem;
            lblDisplay.FontStyle = (FontStyle)boxStyle.SelectedItem;
            lblDisplay.FontWeight = (FontWeight)boxWeight.SelectedItem;
            lblDisplay.FontStretch = (FontStretch)boxStretch.SelectedItem;

            double size;

            if (double.TryParse(boxSize.Text, out size))
                lblDisplay.FontSize = size;
            else
                lblDisplay.FontSize = 8.25;
        }
        
        // OK 버튼을 누르면 대화상자를 종료
        private void OkOnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

    }
}
