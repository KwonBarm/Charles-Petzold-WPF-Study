using System.Windows;
using System.Windows.Controls;

namespace Petzold.MoveTheToolbar
{
    class MoveTheToolbar : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new MoveTheToolbar());
        }

        public MoveTheToolbar()
        {
            Title = "Move the Toolbar";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Window Content인 DockPanel 생성
            DockPanel dock = new DockPanel();
            Content = dock;

            // 윈도우 왼쪽과 상단에 위치할 ToolBarTray 생성
            ToolBarTray trayTop = new ToolBarTray();
            dock.Children.Add(trayTop);
            DockPanel.SetDock(trayTop, Dock.Top);

            ToolBarTray trayLeft = new ToolBarTray();
            trayLeft.Orientation = Orientation.Vertical;
            dock.Children.Add(trayLeft);
            DockPanel.SetDock(trayLeft, Dock.Left);

            // 나머지 클라이언트 영역을 채울 텍스트 박스 생성
            TextBox txtBox = new TextBox();
            dock.Children.Add(txtBox);

            // 6개의 툴바 생성
            for(int i=0; i < 6; i++)
            {
                ToolBar toolbar = new ToolBar();
                toolbar.Header = "Toolbar " + (i + 1);

                if(i < 3)
                    trayTop.ToolBars.Add(toolbar);
                else
                    trayLeft.ToolBars.Add(toolbar);

                // 각각 6개의 버튼 생성
                for(int j = 0; j < 6; j++)
                {
                    Button btn = new Button();
                    btn.FontSize = 16;
                    btn.Content = (char)('A' + j);
                    toolbar.Items.Add(btn);
                }
            }
        }
    }
}
