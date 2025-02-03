using System.Windows;
using System.Windows.Controls;

namespace Petzold.FormatRichText
{
    internal partial class FormatRichText : Window
    {
        RichTextBox txtbox;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new FormatRichText());
        }

        public FormatRichText()
        {
            Title = "Format Rich Text";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Height = 600;
            Width = 1000;

            // 윈도우 Content를 위한 DockPanel 생성
            DockPanel dock = new DockPanel();
            Content = dock;

            // 클라이언트 영역의 상단에 위치할 ToolBarTray 생성
            ToolBarTray tray = new ToolBarTray();
            dock.Children.Add(tray);
            DockPanel.SetDock(tray, Dock.Top);

            // RichTextBox 생성
            txtbox = new RichTextBox();
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            // 다른 파일의 메소드 호출
            AddFileToolbar(tray, 0, 0);
            AddEditToolbar(tray, 1, 0);
            AddCharToolbar(tray, 2, 0);
            AddParaToolbar(tray, 3, 0);
            //AddStatusBar(dock);

            // RichTextBox를 나머지 클라이언트 여역을 채우고 포커스를 위치시킨다.
            dock.Children.Add(txtbox);
            txtbox.Focus();

        }
    }
}
