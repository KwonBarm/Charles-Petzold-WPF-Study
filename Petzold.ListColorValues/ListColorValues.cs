using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ListColorValues
{
    class ListColorValues : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorValues());
        }

        public ListColorValues()
        {
            Title = "List Color Values";

            // 윈도우 Content를 위한 ListBox 생성
            ListBox lstBox = new ListBox();
            lstBox.Width = 150;
            lstBox.Height = 150;
            lstBox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstBox;

            // 리스트 박스를 Color 값으로 채움
            PropertyInfo[] props = typeof(Colors).GetProperties();
            foreach (PropertyInfo prop in props)
                lstBox.Items.Add(prop.GetValue(null, null));

        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lstBox = sender as ListBox;
            if(lstBox.SelectedIndex != -1)
            {
                Color clr = (Color)lstBox.SelectedItem;
                Background = new SolidColorBrush(clr);
            }
        }
    }
}
