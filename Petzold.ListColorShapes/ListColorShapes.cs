using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Petzold.ListColorShapes
{
    class ListColorShapes : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorShapes());
        }

        public ListColorShapes()
        {
            Title = "List Color Shapes";

            // 윈도우 Content를 위한 리스트 박스 생성
            ListBox lstBox = new ListBox();
            lstBox.Width = 150;
            lstBox.Height = 150;
            //lstBox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstBox;

            // 리스트 박스를 Ellipse 객체로 채움
            PropertyInfo[] props = typeof(Brushes).GetProperties();
            foreach(PropertyInfo prop in props)
            {
                Ellipse ellip = new Ellipse();
                ellip.Width = 100;
                ellip.Height = 20;
                ellip.Margin = new Thickness(10,5,0,5);
                ellip.Fill = (Brush)prop.GetValue(null, null);
                lstBox.Items.Add(ellip);
            }

            lstBox.SelectedValuePath = "Fill";
            lstBox.SetBinding(ListBox.SelectedValueProperty, "Background");
            lstBox.DataContext = this;
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lstBox = sender as ListBox;
            if(lstBox.SelectedIndex != 1)
            {
                Background = (lstBox.SelectedItem as Shape).Fill;
            }
        }
    }
}
