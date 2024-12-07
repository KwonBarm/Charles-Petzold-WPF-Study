using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommandTheButton
{
    class CommandTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CommandTheButton());
        }

        public CommandTheButton()
        {
            Title = "Command the Button";

            Button btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Command = ApplicationCommands.Paste;
            btn.Content = ApplicationCommands.Paste.Text;
            //btn.Command = new PasteCommand();
            //btn.Content = new PasteCommand().Text; 

            Content = btn;

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste,PasteOnExecute, PasteOnCanExecute));
        }

        private void PasteOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            Title = Clipboard.GetText();
        }

        private void PasteOnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsText();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Title = "Command the Button";
        }
    }
}
