using Petzold.ListNamedBrushes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;

namespace Petzold.ListColorsEvenElegantlier
{
    internal class ListColorsEvenElegantlier : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorsEvenElegantlier());
        }

        public ListColorsEvenElegantlier()
        {
            Title = "List Colors Even Elegantlier";

            // 항목의 DataTemplate 생성
            DataTemplate template = new DataTemplate(typeof(NamedBrush));

            // StackPanel 기반의 FrameworkElementFactory 생성
            FrameworkElementFactory factoryStack = new FrameworkElementFactory(typeof(StackPanel));
            factoryStack.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            // DataTemplate 비주얼 트리의 루트 생성
            template.VisualTree = factoryStack;

            // Rectangle 기반의 FrameworkElementFactory 생성
            FrameworkElementFactory factoryRectangle = new FrameworkElementFactory(typeof(Rectangle));
            factoryRectangle.SetValue(Rectangle.WidthProperty, 16.0);
            factoryRectangle.SetValue(Rectangle.HeightProperty, 16.0);
            factoryRectangle.SetValue(Rectangle.MarginProperty, new Thickness(2));
            factoryRectangle.SetValue(Rectangle.StrokeProperty, SystemColors.WindowTextBrush);
            factoryRectangle.SetValue(Rectangle.FillProperty, new Binding("Brush"));

            // StackPanel에 factoryRectangle 객체 추가
            factoryStack.AppendChild(factoryRectangle);

            // TextBlock 기반의 FrameworkElementFactory 생성
            FrameworkElementFactory factoryTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            factoryTextBlock.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            factoryTextBlock.SetValue(TextBlock.TextProperty, new Binding("Name"));

            // StackPanel에 factoryTextBlock 객체 추가
            factoryStack.AppendChild(factoryTextBlock);

            // 윈도우 Content를 위한 리스트 박스 생성
            ListBox lstbox = new ListBox();
            lstbox.Width = 200;
            lstbox.Height = 200;
            Content = lstbox;

            // ItemTemplate 속성을 위해 생성한 DataTemplate 할당
            lstbox.ItemTemplate = template;

            // ItemsSource 속성을 NamedBrush.All로 할당
            lstbox.ItemsSource = NamedBrush.All;

            // SelectedValue와 윈도우 배경색을 바인딩
            lstbox.SelectedValuePath = "Brush";
            lstbox.SetBinding(ListBox.SelectedValueProperty, "Background");
            lstbox.DataContext = this;
        }
    }
}
