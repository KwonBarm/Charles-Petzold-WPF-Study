using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Petzold.CraftTheToolbar
{
    internal class CraftTheToolbar : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CraftTheToolbar());
        }

        public CraftTheToolbar()
        {
            Title = "Craft the Toolbar";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            RoutedUICommand[] comm =
            {
                ApplicationCommands.New,
                ApplicationCommands.Open,
                ApplicationCommands.Save,
                ApplicationCommands.Print,
                ApplicationCommands.Cut,
                ApplicationCommands.Copy,
                ApplicationCommands.Paste,
                ApplicationCommands.Delete,
            };

            string[] strImage =
            {
                "New.png",
                "Open.png",
                "Save.png",
                "Print.png",
                "Cut.png",
                "Copy.png",
                "Paste.png",
                "Delete.png",
            };

            // 윈도우 Content를 위한 DockPanel 생성
            DockPanel dock = new DockPanel();
            dock.LastChildFill = false;
            Content = dock;

            // 윈도우 상단에 위치할 툴바 생성
            ToolBar toolBar = new ToolBar();
            dock.Children.Add(toolBar);
            DockPanel.SetDock(toolBar, Dock.Top);

            //  툴바에 버튼 추가
            for (int i = 0; i < comm.Length; i++)
            {
                if (i == 4)
                    toolBar.Items.Add(new Separator());

                // Button 생성
                Button btn = new Button();
                btn.Command = comm[i];
                toolBar.Items.Add(btn);

                // 버튼의 내용에 해당하는 이미지 생성
                Image img = new Image();
                img.Source = new BitmapImage(new Uri("pack://application:,,/Images/" + strImage[i]));
                img.Stretch = Stretch.Fill;
                img.Width = 60;
                img.Height = 60;
                btn.Content = img;

                // UICommand 텍스트 기반의 툴팁 생성
                ToolTip tip = new ToolTip();
                tip.Content = comm[i].Text;
                btn.ToolTip = tip;

                // 윈도우 CommandBinding에 UICommand 추가
                // 커맨드 바인딩 전에는 ToolTip이 표시되지 않음
                CommandBindings.Add(new CommandBinding(comm[i],ToolbarButtonOnClick));
            }
        }

        private void ToolbarButtonOnClick(object sender, ExecutedRoutedEventArgs e)
        {
            RoutedUICommand comm = e.Command as RoutedUICommand;
            MessageBox.Show(comm.Name + " command not yet implemented", Title);
        }
    }
}
