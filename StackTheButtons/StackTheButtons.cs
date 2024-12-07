using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StackTheButtons
{
    class StackTheButtons : Window
    {
        StackPanel stack;

        [STAThread]
        public static void Main()
        {
            Application application = new Application();
            application.Run(new StackTheButtons());
        }

        public StackTheButtons()
        {
            Title = "Stack Ten Buttons";

            stack = new StackPanel();
            Content = stack;

            Random random = new Random();

            for(int i =0; i < 10; i++)
            {
                Button btn = new Button();

                btn.Name = ((char)('A'+i)).ToString();
                btn.FontSize += random.Next(10);
                btn.Content = "Button " + btn.Name + " says 'Click me'";
                btn.Click += ButtonOnClick;
                //btn.HorizontalAlignment = HorizontalAlignment.Center;

                stack.Children.Add(btn);
                stack.Background = Brushes.Aquamarine;
                //stack.HorizontalAlignment = HorizontalAlignment.Center;
                //stack.Orientation = Orientation.Horizontal;

                SizeToContent = SizeToContent.WidthAndHeight;
            }
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;

            //MessageBox.Show("Button " + btn.Name + "has been clicked", "Button click");

            foreach(var button in stack.Children)
            {
                if(button is Button getButton)
                {
                    if(getButton.Name == btn.Name)
                    {
                        MessageBox.Show(stack.Children.IndexOf(getButton).ToString());
                    }
                }
            }
        }
    }
}
