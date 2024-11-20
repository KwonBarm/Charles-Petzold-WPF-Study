using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleEllipse
{
    internal class RenderTheGraphic : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new RenderTheGraphic());
        }

        public RenderTheGraphic()
        {
            Title = "Render the Graphoc";
            SimpleEllipse simpleEllipse = new SimpleEllipse();
            Content = simpleEllipse;
        }
    }
}
