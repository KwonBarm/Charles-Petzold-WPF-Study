using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedievalButton
{
    internal class TestWindow : Window
    {
        TextBlock txtblock;
        ScrollViewer scroll;
        StackPanel stackPanel;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new TestWindow());
        }

        public TestWindow()
        {
            Title = "Custom Medieval Button";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 300;
            Height = 1000;

            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());

            Content = grid;

            MedievalButton medievalButton = new MedievalButton();
            medievalButton.Text = "Click me!";
            medievalButton.Margin = new Thickness(20);
            medievalButton.Knock += MedievalButtonOnKnock;
            grid.Children.Add(medievalButton);
            Grid.SetRow(medievalButton, 0);

            stackPanel = new StackPanel();

            scroll = new ScrollViewer();
            scroll.Content = stackPanel;
            grid.Children.Add(scroll);
            Grid.SetRow(scroll, 1);



        }

        private void MedievalButtonOnKnock(object sender, RoutedEventArgs e)
        {
            txtblock = new TextBlock();
            txtblock.Margin = new Thickness(5);
            txtblock.Padding = new Thickness(5, 0, 0, 0);
            stackPanel.Children.Add(txtblock);
            txtblock.Text = e.Handled.ToString();
            scroll.ScrollToBottom();
        }
    }
}
