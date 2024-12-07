using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SpaceButton
{
    class SpaceButtonWindow : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new SpaceButtonWindow());
        }

        public SpaceButtonWindow()
        {
            Title = "Space Button Window";

            SpaceButton spaceButton = new SpaceButton();
            Content = spaceButton;
            spaceButton.Text = "abcdefg";
            spaceButton.Space = 5;
            spaceButton.VerticalAlignment = VerticalAlignment.Center;
            spaceButton.HorizontalAlignment = HorizontalAlignment.Center;
            spaceButton.Click += SpaceButton_Click;
        }

        private void SpaceButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            StringBuilder text = new StringBuilder();

            foreach(char ch in button.Content.ToString())
            {
                if(ch != ' ')
                    text.Append(ch);
            }

            MessageBox.Show(text.ToString());
        }
    }
}
