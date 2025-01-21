using Petzold.SelectColorFromGrid;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.FormatRichText
{
    internal partial class FormatRichText : Window
    {
        ComboBox comboFamily, comboSize;
        ToggleButton btnBold, btnItalic;
        ColorGridBox clrboxBackground, clrboxForeground;

        void AddCharToolbar(ToolBarTray tray, int band, int index)
        {
            // 툴바를 생성해서 ToolBarTray에 추가
            ToolBar toolBar = new ToolBar();
            toolBar.Band = band;
            toolBar.BandIndex = index;
            tray.ToolBars.Add(toolBar);

            // 폰트 패밀리를 위한 콤보 박스 생성
            comboFamily = new ComboBox();
            comboFamily.Width = 144;
            comboFamily.ItemsSource = Fonts.SystemFontFamilies;
            comboFamily.SelectedItem = txtbox.FontFamily;
            comboFamily.SelectionChanged += FamilyComboOnSelection;
            toolBar.Items.Add(comboFamily);

            ToolTip tip = new ToolTip();
            tip.Content = "Font Family";
            comboFamily.ToolTip = tip;

            // 폰트 크기를 위한 콤보 박스 생성
            comboSize = new ComboBox();
            comboSize.Width = 48;
            comboSize.IsEditable = true;
            comboSize.Text = (0.75 * txtbox.FontSize).ToString();
            comboSize.ItemsSource = new double[]
            {
                8, 9, 10, 11, 12, 14, 16, 18, 20, 24, 28, 36, 48, 72
            };
            comboSize.SelectionChanged += SizeComboOnSelection;
            comboSize.GotKeyboardFocus += SizeComboOnGotFocus;
            comboSize.LostKeyboardFocus += SizeComboOnLostFocus;
            comboSize.PreviewKeyDown += SizeComboOnKeyDown;
            toolBar.Items.Add(comboSize);

            tip = new ToolTip();
            tip.Content = "Font Size";
            comboSize.ToolTip = tip;

            // 굵게 버튼 생성
            btnBold = new ToggleButton();
            btnBold.Checked += BoldButtonOnChecked;
            btnBold.Unchecked += BoldButtonOnUnchecked;
            btnBold.BorderBrush = Brushes.Black;
            btnBold.BorderThickness = new Thickness(1);
            toolBar.Items.Add(btnBold);

            Image img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,/Images/boldhs.png"));
            img.Width = 16;
            img.Height = 16;
            btnBold.Content = img;

            tip = new ToolTip();
            tip.Content = "Bold";
            btnBold.ToolTip = tip;

            // Italic 버튼 생성
            btnItalic = new ToggleButton();
            btnItalic.Checked += ItalicButtonOnChecked;
            btnItalic.Unchecked += ItalicButtonOnUnchecked;
            btnItalic.BorderBrush = Brushes.Black;
            btnItalic.BorderThickness = new Thickness(1);
            toolBar.Items.Add(btnItalic);

            img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,/Images/ItalicHS.png"));
            img.Width = 16;
            img.Height = 16;
            btnItalic.Content = img;

            tip = new ToolTip();
            tip.Content = "Italic";
            btnItalic.ToolTip = tip;

            toolBar.Items.Add(new Separator());

            // 배경색과 전경색 메뉴 생성
            Menu menu = new Menu();
            toolBar.Items.Add(menu);

            // 배경색 메뉴 항목 생성
            MenuItem item = new MenuItem();
            menu.Items.Add(item);

            img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,/Images/ColorHS.png"));
            img.Width = 16;
            img.Height = 16;
            item.Header = img;

            clrboxBackground = new ColorGridBox();
            clrboxBackground.SelectionChanged += BackgroundColorOnSelection;
            item.Items.Add(clrboxBackground);

            tip = new ToolTip();
            tip.Content = "Background Color";
            item.ToolTip = tip;

            // 전경색 메뉴 항목 생성
            item = new MenuItem();
            menu.Items.Add(item);

            img = new Image();
            img.Source = new BitmapImage(new Uri("pack://application:,,/Images/fontHS.png"));
            img.Width = 16;
            img.Height = 16;
            item.Header = img;

            clrboxForeground = new ColorGridBox();
            clrboxForeground.SelectionChanged += ForegroundColorOnSelection;
            item.Items.Add(clrboxForeground);

            tip = new ToolTip();
            tip.Content = "Foreground Color";
            item.ToolTip = tip;

            // RichTextBox의 SelectionChanged 이벤트에 대한 핸들러 연결
            txtbox.SelectionChanged += TextBoxOnSelectionChanged;

        }

        // RichTextBox의 SelectionChanged 이벤트 핸들러
        private void TextBoxOnSelectionChanged(object sender, RoutedEventArgs e)
        {
            // 현재 선택된 텍스트의 폰트 패밀리를 구하고...
            object obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontFamilyProperty);

            // ... 콤보 박스에 구한 것을 설정하고
            if (obj is FontFamily)
                comboFamily.SelectedItem = (FontFamily)obj;
            else
                comboFamily.SelectedIndex = -1;

            // 현재 선택된 텍스트의 폰트 크기를 구하고...
            obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontSizeProperty);

            // ... 콤보 박스에 구한 것을 설정하고
            if (obj is double)
                comboSize.Text = (0.72 * (double)obj).ToString();
            else
                comboSize.SelectedIndex = -1;

            // 현재 선택된 텍스트의 폰트웨이트를 구하고
            obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontWeightProperty);

            // ... 토클 버튼을 설정하고
            if (obj is FontWeight)
                btnBold.IsChecked = (FontWeight)obj == FontWeights.Bold;

            // 현재 선택된 텍스트의 폰트 스타일을 구하고...
            obj = txtbox.Selection.GetPropertyValue(FlowDocument.FontStyleProperty);

            // ... 토글 버튼을 설정하고
            if(obj is FontStyle)
                btnItalic.IsChecked = (FontStyle)obj == FontStyles.Italic;

            // 배경색과 전경색을 구해서 ColorGridBox 컨트롤에 설정
            obj = txtbox.Selection.GetPropertyValue(FlowDocument.BackgroundProperty);

            if (obj != null && obj is Brush)
                clrboxBackground.SelectedValue = (Brush)obj;

            obj = txtbox.Selection.GetPropertyValue(FlowDocument.ForegroundProperty);

            if (obj != null && obj is Brush)
                clrboxForeground.SelectedValue = (Brush)obj;
        }

        // 폰트 패밀리 콤보 박스의 SelectionChanged 이벤트 핸들러
        private void FamilyComboOnSelection(object sender, SelectionChangedEventArgs e)
        {
            // 선택된 폰트 패밀리를 구함
            ComboBox combo = e.Source as ComboBox;
            FontFamily family = combo.SelectedItem as FontFamily;

            // 선택된 텍스트를 설정
            if (family != null)
                txtbox.Selection.ApplyPropertyValue(FlowDocument.FontFamilyProperty, family);

            // 텍스트 박스에 포커스를 줌
            txtbox.Focus();
        }

        private void ForegroundColorOnSelection(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BackgroundColorOnSelection(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ItalicButtonOnUnchecked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ItalicButtonOnChecked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BoldButtonOnUnchecked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BoldButtonOnChecked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SizeComboOnKeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SizeComboOnLostFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SizeComboOnGotFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SizeComboOnSelection(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        
    }
}
