﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace BindTheButton
{
    class BindTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new BindTheButton());
        }

        public BindTheButton()
        {
            Title = "Bind theButton";
            
            ToggleButton btn = new ToggleButton();
            btn.Content = "Make _Topmost";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.SetBinding(ToggleButton.IsCheckedProperty, "Topmost");
            btn.DataContext = this;

            Content = btn;

            ToolTip tip = new ToolTip();
            tip.Content = "Toggle the button on to make" + "the window topmost on the desktop";

            btn.ToolTip = tip;
        }

    }
}
