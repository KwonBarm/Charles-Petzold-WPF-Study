using System.Windows;
using System.Windows.Controls;

namespace ClickTheButton
{
    class ClickTheButton : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ClickTheButton());
        }

        public ClickTheButton()
        {
            Title = "Click the Button";

            Button btn = new Button();
            btn.Content = "_Click me, please";
            btn.Click += Btn_Click;
            //btn.Focus();
            //btn.IsDefault = true;
            //btn.IsCancel = true;
            btn.Margin = new Thickness(96);
            //btn.HorizontalContentAlignment = HorizontalAlignment.Center;
            //btn.VerticalAlignment = VerticalAlignment.Center;
            //btn.Padding = new Thickness(96);

            SizeToContent = SizeToContent.WidthAndHeight;
            Content = btn;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The Button has been clicked all is well.", Title);
        }
    }
}
