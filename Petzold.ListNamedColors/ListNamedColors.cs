using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ListNamedColors
{
    internal class ListNamedColors : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListNamedColors());
        }

        public ListNamedColors()
        {
            Title = "List Named Colors";

            // 윈도우 Content를 위한 리스트 박스 생성
            ListBox lstBox = new ListBox();
            lstBox.Width = 150;
            lstBox.Height = 150;
            lstBox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstBox;

            // 항목과 프로퍼티 패스 설정
            lstBox.ItemsSource = NamedColor.All;
            lstBox.DisplayMemberPath = "Name";
            lstBox.SelectedValuePath = "Color";
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lstBox = sender as ListBox;

            if(lstBox.SelectedValue != null)
            {
                Color clr = (Color)lstBox.SelectedValue;
                Background = new SolidColorBrush(clr);
            }
        }
    }
}
