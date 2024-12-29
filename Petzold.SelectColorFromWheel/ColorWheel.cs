using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using Petzold.CircleTheButtons;

namespace Petzold.SelectColorFromWheel
{
    internal class ColorWheel : ListBox
    {
        public ColorWheel()
        {
            // ItemsPanel 템플릿 정의
            FrameworkElementFactory factoryRadialPanel = new FrameworkElementFactory(typeof(RadialPanel));
            ItemsPanel = new ItemsPanelTemplate(factoryRadialPanel);

            // 항목에 대한 DataTemplate 생성
            DataTemplate template = new DataTemplate(typeof(Brush));
            ItemTemplate = template;

            // Rectangle 기반의 FrameworkElementFactory 생성
            FrameworkElementFactory elRectangle = new FrameworkElementFactory(typeof(Rectangle));
            elRectangle.SetValue(Rectangle.WidthProperty, 4.0);
            elRectangle.SetValue(Rectangle.HeightProperty, 12.0);
            elRectangle.SetValue(Rectangle.MarginProperty, new Thickness(1, 8, 1, 8));
            elRectangle.SetValue(Rectangle.FillProperty, new Binding(""));

            // 비주얼 트리를 위해 factory를 사용
            template.VisualTree = elRectangle;

            // 리스트 박스에 항목 설정
            PropertyInfo[] props = typeof(Brushes).GetProperties();

            foreach(PropertyInfo prop in props)
                Items.Add((Brush)prop.GetValue(null, null));
        }
    }
}