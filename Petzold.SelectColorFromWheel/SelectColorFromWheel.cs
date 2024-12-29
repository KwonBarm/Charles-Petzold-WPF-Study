using System.Windows;
using System.Windows.Controls;

namespace Petzold.SelectColorFromWheel
{
    class SelectColorFromWheel : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SelectColorFromWheel());
        }

        public SelectColorFromWheel()
        {
            Title = "Select Color from Wheel";
            SizeToContent = SizeToContent.WidthAndHeight;

            // 윈도우 Content를 위한 StackPanel 생성
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Content = stack;

            // 탭 키를 검사하기 위한 do-nothing 버튼 생성
            Button btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);

            // ColorWheel 컨트럴 생성
            ColorWheel clrwheel = new ColorWheel();
            clrwheel.Margin = new Thickness(24);
            clrwheel.HorizontalAlignment = HorizontalAlignment.Center;
            clrwheel.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(clrwheel);

            // 윈도우 배경색과 ColorWhell의 선택 값을 바인딩
            clrwheel.SetBinding(ColorWheel.SelectedValueProperty, "Background");
            clrwheel.DataContext = this;

            // 또 다른 do-nothing 버튼 생성
            btn = new Button();
            btn.Content = "Do-nothing button\nto test tabbing";
            btn.Margin = new Thickness(24);
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(btn);
        }
    }
}
