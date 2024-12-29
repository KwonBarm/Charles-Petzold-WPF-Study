using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.SelectColorFromGrid
{
    internal class SelectColorFromGrid : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SelectColorFromGrid());
        }

        public SelectColorFromGrid()
        {
            Title = "Select Color from Grid";
            SizeToContent = SizeToContent.WidthAndHeight;

            // 윈도우 Content를 위한 StackPanel 생성
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 탭 키를 검사하기 위한 do-nothing 버튼 생성
            Button btn = new Button();
            btn.Content = "Do-Nothing Button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);

            // ColorGridBox 컨트럴 생성
            ColorGridBox clrgrid = new ColorGridBox();
            clrgrid.Margin = new Thickness(24);
            clrgrid.HorizontalAlignment = HorizontalAlignment.Center;
            clrgrid.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(clrgrid);

            // 윈도우 배경색과 ColorGridBox에서 선택된 값을 바인딩
            clrgrid.SetBinding(ColorGridBox.SelectedValueProperty,"Background");
            clrgrid.DataContext = this;

            // 또 다른 do-nothing 버튼을 생성
            btn = new Button();
            btn.Content = "Do-Nothing Button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);

        }
    }
}
