using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ListColorNames
{
    class ListColorNames : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorNames());
        }

        public ListColorNames()
        {
            Title = "List Color Names";

            // 윈도우의 목록처럼 ListBox 생성
            ListBox lstBox = new ListBox();
            lstBox.Width = 150;
            //lstBox.Height = 150;
            lstBox.SelectionChanged += LstBox_SelectionChanged;
            Content = lstBox;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //Width = lstBox.Width + 50;
            //Height = lstBox.Height + 50;

            // 색상명으로 ListBox 채우기
            PropertyInfo[] props = typeof(Colors).GetProperties();
            foreach(PropertyInfo prop in props)
                lstBox.Items.Add(prop.Name);

            lstBox.SelectedItem = "Magenta";
            lstBox.ScrollIntoView(lstBox.SelectedItem);
            lstBox.Focus();
        }

        private void LstBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lstBox = sender as ListBox;
            string str = lstBox.SelectedItem as string;
            if(str != null)
            {
                Color clr = (Color)typeof(Colors).GetProperty(str).GetValue(null, null);
                Background = new SolidColorBrush(clr);
            }
        }
    }
}
