using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ExploreDirectiories
{
    class ExploreDirectiories : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ExploreDirectiories());
        }

        public ExploreDirectiories()
        {
            Title = "Explore Directories";
            ScrollViewer scroll = new ScrollViewer();
            Content = scroll;

            WrapPanel wrap = new WrapPanel();
            scroll.Content = wrap;
            wrap.Children.Add(new FileSystemInfoButton());
        }
    }
}
