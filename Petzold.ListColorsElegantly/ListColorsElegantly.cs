using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ListColorsElegantly
{
    internal class ListColorsElegantly : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListColorsElegantly());
        }

        public ListColorsElegantly()
        {
            Title = "List Colors Elegantly";

            ColorListBox lstbox = new ColorListBox();
            lstbox.Width = 150;
            lstbox.Height = 150;
            lstbox.SelectionChanged += ListBoxOnSelectionChanged;
            Content = lstbox;

            // SelectedColor 초기화
            lstbox.SelectedColor = SystemColors.ControlDarkDarkColor;
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ColorListBox lstbox = sender as ColorListBox;
            Background = new SolidColorBrush(lstbox.SelectedColor);

        }
    }
}
