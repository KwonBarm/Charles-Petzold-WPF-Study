using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ExamineKeystrokes
{
    class ExamineKeystrokes : Window
    {
        StackPanel stack;
        ScrollViewer scroll;
        string strHeader = "Event    key                 Sys-Key     Text      "+"Ctrl-Text Sys-Text   Ime       KeyStates     "+"IsDown   IsUp   IsToggled IsRepeat";
        string strFormatKey = "{0,-10}{1,-20}{2,-10}                           "+ "    {3,-10}{4,-15}{5,-8}{6,-7}   {7,-8}{8,-10}";
        string strFormatText = "{0,-10}" + "{1,-10}{2,-10}{3,-10}";

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ExamineKeystrokes());
        }

        public ExamineKeystrokes()
        {
            Title = "Examine Keystrokes";
            FontFamily = new FontFamily("Courier New");

            Grid grid = new Grid();
            Content = grid;
            grid.ShowGridLines = true;

            RowDefinition rowdef = new RowDefinition();
            rowdef.Height = GridLength.Auto;
            grid.RowDefinitions.Add(rowdef);
            grid.RowDefinitions.Add(new RowDefinition());

            // 제목 텍스트를 출력
            TextBlock textHeader = new TextBlock();
            textHeader.FontWeight = FontWeights.Bold;
            textHeader.Text = strHeader;
            grid.Children.Add(textHeader);

            // 이벤트 출력을 위한 ScrollViewer를 생성
            // StackPanel을 생성이벤트 출력을 위한 ScrollViewer를 생성
            scroll = new ScrollViewer();
            grid.Children.Add(scroll);
            Grid.SetRow(scroll, 1);

            stack = new StackPanel();
            scroll.Content = stack;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            DisplayKeyInfo(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            DisplayKeyInfo(e);
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            string str =
                String.Format(strFormatText, e.RoutedEvent.Name, e.Text, e.ControlText, e.SystemText);
            DisplayInfro(str);
        }

        private void DisplayKeyInfo(KeyEventArgs e)
        {
            string str =
                String.Format(strFormatKey, e.RoutedEvent.Name, e.Key, e.SystemKey, e.ImeProcessedKey, e.KeyStates, e.IsDown, e.IsUp, e.IsToggled, e.IsRepeat);
            DisplayInfro(str);
        }

        private void DisplayInfro(string str)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = str;
            stack.Children.Add(textBlock);
            scroll.ScrollToBottom();
        }
    }
}
