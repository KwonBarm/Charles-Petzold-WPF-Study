using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ToggleTheButton
{
    class ToggleTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();   
            app.Run(new ToggleTheButton());
        }

        public ToggleTheButton()
        {
            Title = "Toggle the Button";

            //ToggleButton btn = new ToggleButton();
            CheckBox btn = new CheckBox();
            btn.Content = "Can _Resize";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.IsChecked = true;//(ResizeMode == ResizeMode.CanResize);
            btn.Checked += ButtonOnChecked;
            btn.Unchecked += ButtonOnChecked;

            Content = btn;
        }

        private void ButtonOnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox btn = sender as CheckBox;
            ResizeMode = (bool)btn.IsChecked ? ResizeMode.CanResize
                : ResizeMode.NoResize;
        }
    }
}
