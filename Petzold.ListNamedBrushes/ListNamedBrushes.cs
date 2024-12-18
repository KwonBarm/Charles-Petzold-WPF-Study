using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.ListNamedBrushes
{
    internal class ListNamedBrushes : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListNamedBrushes());
        }

        public ListNamedBrushes()
        {
            Title = "List Named Brushes";

            // 윈도우 Content를 위한 리스트 박스 생성
            ListBox lstBox = new ListBox();
            lstBox.Width = 150;
            lstBox.Height = 150;
            Content = lstBox;

            // 항목과 속성 패스를 설정
            lstBox.ItemsSource = NamedBrush.All;
            lstBox.DisplayMemberPath = "Name";
            lstBox.SelectedValuePath = "Brush";

            // SelectedValue와 윈도우 배경색을 바인딩
            lstBox.SetBinding(ListBox.SelectedValueProperty,"Background");
            lstBox.DataContext = this;
        }
    }
}
