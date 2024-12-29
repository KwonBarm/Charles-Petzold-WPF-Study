using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.SelectColorFromGrid
{
    internal class ColorGridBox : ListBox
    {
        // 보여줄 색상
        string[] strColors =
        {
            "Black", "Brown", "DarkGreen", "MidnightBlue",
            "Navy", "DarkBlue", "Indigo", "DimGray",
            "DarkRed", "OrangeRed", "Olive", "Green",
            "Teal", "Blue", "SlateGray", "Gray",
            "Red", "Orange", "YellowGreen", "SeaGreen",
            "Aqua", "LightBlue", "Violet", "DarkGray",
            "Pink", "Gold", "Yellow", "Lime",
            "Turquoise", "SkyBlue", "Plum", "LightGray",
            "LightPink", "Tan", "LightYellow", "LightGreen",
            "LightCyan", "LightSkyBlue", "Lavender", "White"
        };

        public ColorGridBox()
        {
            // ItemsPanel 템플릿 정의
            FrameworkElementFactory factoryUnigrid = new FrameworkElementFactory(typeof(UniformGrid));
            factoryUnigrid.SetValue(UniformGrid.ColumnsProperty, 8);
            ItemsPanel = new ItemsPanelTemplate(factoryUnigrid);

            // ListBox에 항목 추가
            foreach (string strColor in strColors)
            {
                // Rectangle을 생성해 리스트 박스에 추가
                Rectangle rect = new Rectangle();
                rect.Width = 12;
                rect.Height = 12;
                rect.Margin = new Thickness(4);
                rect.Fill = (Brush)typeof(Brushes).GetProperty(strColor).GetValue(null, null);

                Items.Add(rect);

                // Rectangle을 위한 ToolTip을 생성
                ToolTip tip = new ToolTip();
                tip.Content = strColor;
                rect.ToolTip = tip;
            }

            // SelectedValue를 Rectangle 항목의 Fill 속성으로 설정
            SelectedValuePath = "Fill";
        }
    }
}
