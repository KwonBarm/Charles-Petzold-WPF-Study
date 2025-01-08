using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Petzold.CommandTheMenu
{
    internal class CommandTheMenu : Window
    {
        TextBlock text;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CommandTheMenu());
        }

        public CommandTheMenu()
        {
            Title = "Command the Menu";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // DockPanel 생성
            DockPanel dock = new DockPanel();
            Content = dock;

            // 탑 메뉴용 Menu 생성
            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // 나머지를 채울 TextBlock 생성
            text = new TextBlock();
            text.Text = "Sample clipboard text";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.FontSize = 32;
            text.TextWrapping = TextWrapping.Wrap;
            dock.Children.Add(text);

            // Edit 메뉴 생성
            MenuItem itemEdit = new MenuItem();
            itemEdit.Header = "_Edit";
            menu.Items.Add(itemEdit);

            // Edit 메뉴의 항목을 생성
            MenuItem itemCut = new MenuItem();
            itemCut.Header = "Cu_t";
            itemCut.Command = ApplicationCommands.Cut;
            itemEdit.Items.Add(itemCut);

            MenuItem itemCopy = new MenuItem();
            itemCopy.Header = "_Copy";
            itemCopy.Command = ApplicationCommands.Copy;
            itemEdit.Items.Add(itemCopy);

            MenuItem itemPaste = new MenuItem();
            itemPaste.Header = "_Paste";
            itemPaste.Command = ApplicationCommands.Paste;
            itemEdit.Items.Add(itemPaste);

            MenuItem itemDelete = new MenuItem();
            itemDelete.Header = "_Delete";
            itemDelete.Command = ApplicationCommands.Delete;
            itemEdit.Items.Add(itemDelete);

            // Window 컬렌셔에 바인딩될 커맨드를 추가
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, CutOnExecute, CutCanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, CopyOnExecute, CutCanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, PasteOnExecute, PasteCanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, DeleteOnExecute, CutCanExecute));

            InputGestureCollection collGesture = new InputGestureCollection();
            collGesture.Add(new KeyGesture(Key.R, ModifierKeys.Control));
            RoutedUICommand commRestore = new RoutedUICommand("_Restore", "Restore", GetType(), collGesture);

            MenuItem itemReStore = new MenuItem();
            //itemReStore.Header = "_Restore";
            itemReStore.Command = commRestore;
            itemEdit.Items.Add(itemReStore);
            CommandBindings.Add(new CommandBinding(commRestore, RestoreOnExecute));
        }

        void CutCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = text.Text != null && text.Text.Length > 0;
        }

        void PasteCanExecute(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = Clipboard.ContainsText();
        }

        void CutOnExecute(object sener, ExecutedRoutedEventArgs args)
        {
            ApplicationCommands.Copy.Execute(null, this);
            ApplicationCommands.Delete.Execute(null, this);
        }

        void CopyOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            Clipboard.SetText(text.Text);
        }

        void PasteOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            text.Text = Clipboard.GetText();
        }

        void DeleteOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            text.Text = null;
        }

        void RestoreOnExecute(object sender, ExecutedRoutedEventArgs args)
        {
            text.Text = "Sample clipboard text";
        }
    }
}
