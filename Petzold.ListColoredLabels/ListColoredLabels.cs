using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ListColoredLabels
{
    internal class ListColoredLabels : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColoredLabels());
        }

        public ListColoredLabels()
        {
            Title = "List Colored Labels";

            // 윈도우 Content를 위해 리스트 박스 생성
            ListBox lstBox = new ListBox();
            lstBox.Height = 150;
            lstBox.Width = 150;
            lstBox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstBox;

            // lable 컨트롤로 리스트 박스 채움
            PropertyInfo[] props = typeof(Colors).GetProperties();

            // 왜 0.222, 0.707, 0.071인가요?
            // 이 값들은 RGB(빨강, 초록, 파랑) 각각의 채널이 밝기에 미치는 상대적인 기여도를 반영합니다.
            // 인간의 눈은 서로 다른 색상에 대해 민감도가 다르며, 특히 초록색에 가장 민감하고, 빨강에 그다음으로 민감하며, 파랑에 가장 덜 민감합니다.
            // 이러한 민감도 차이를 고려하여 초록(G) 채널의 가중치가 가장 높게 설정되었습니다.
            foreach (PropertyInfo prop in props)
            {
                Color clr = (Color)prop.GetValue(null, null);
                bool isBlack = 0.222 * clr.R + 0.707 * clr.G + 0.071 * clr.B > 128;

                Label lbl = new Label();
                lbl.Content = prop.Name;
                lbl.Background = new SolidColorBrush(clr);
                lbl.Foreground = isBlack ? Brushes.Black : Brushes.White;
                lbl.Width = 100;
                lbl.Margin = new Thickness(15, 0, 0, 0);
                lbl.Tag = clr;
                lstBox.Items.Add(lbl);
            }
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lstBox = sender as ListBox;
            Label lbl = lstBox.SelectedItem as Label;

            if (lbl != null)
            {
                Color clr = (Color)lbl.Tag;
                Background = new SolidColorBrush(clr);
            }
        }
    }
}
