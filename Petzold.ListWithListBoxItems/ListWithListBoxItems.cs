using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ListWithListBoxItems
{
    class ListWithListBoxItems : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListWithListBoxItems());
        }

        public ListWithListBoxItems()
        {
            Title = "List with ListBox Items";

            // 윈도우 Content를 위한 리스트 박스 생성
            ListBox lstBox = new ListBox();
            lstBox.Width = 150;
            lstBox.Height = 150;
            lstBox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstBox;

            // 리스트 박스를 ListBoxItem 객체로 채움
            PropertyInfo[] props = typeof(Colors).GetProperties();

            foreach (PropertyInfo prop in props)
            {
                Color clr = (Color)prop.GetValue(null, null);
                bool isBlack = 0.222 * clr.R + 0.707 * clr.G + 0.071 * clr.B > 128;

                ListBoxItem item = new ListBoxItem();
                item.Content = prop.Name;
                item.Background = new SolidColorBrush(clr);
                item.Foreground = isBlack ? Brushes.Black : Brushes.White;
                item.HorizontalContentAlignment = HorizontalAlignment.Center;
                item.Padding = new Thickness(2);
                lstBox.Items.Add(item);
            }
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lstBox = sender as ListBox;
            ListBoxItem item;

            if (e.AddedItems.Count > 0)
            {
                item = e.AddedItems[0] as ListBoxItem;
                string str = item.Content as string;
                item.Content = "[ " + str + " ]";
                item.FontWeight = FontWeights.Bold;
            }

            if(e.RemovedItems.Count > 0)
            {
                item = e.RemovedItems[0] as ListBoxItem;
                string str = item.Content as string;
                item.Content = str.Substring(2, str.Length - 4);
                item.FontWeight = FontWeights.Normal;
            }

            item = lstBox.SelectedItem as ListBoxItem;
            if(item !=null)
                Background = item.Background;
        }
    }
}
