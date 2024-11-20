using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace ToggleBoldAndItalic
{
    internal class ToggleBoldAndItalic : Window
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application app = new Application();
            app.Run(new ToggleBoldAndItalic());
        }

        public ToggleBoldAndItalic()
        {
            Title = "Toggle Bold & Italic";
            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 32;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            Content = textBlock;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            string strQuote = "To be, or not to be, that is the question";
            string[] strwords = strQuote.Split();

            foreach (string str in strwords)
            {
                Run run = new Run(str);
                run.MouseDown += Run_MouseDown;
                textBlock.Inlines.Add(run);
                textBlock.Inlines.Add(" ");
            }
        }

        private void Run_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Run run = sender as Run;

            if ((e.ChangedButton == MouseButton.Left))
            {
                run.FontStyle = run.FontStyle == FontStyles.Normal 
                    ? FontStyles.Italic 
                    : FontStyles.Normal;
            }

            if (e.ChangedButton == MouseButton.Right) 
            { 
                run.FontWeight = run.FontWeight == FontWeights.Bold
                    ? FontWeights.Normal
                    : FontWeights.Bold;
            }
        }
    }
}
