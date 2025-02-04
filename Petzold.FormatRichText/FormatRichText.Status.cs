using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace Petzold.FormatRichText
{
    internal partial class FormatRichText : Window
    {
        StatusBarItem itemDateTime;

        void AddStatusBar(DockPanel dock)
        {
            StatusBar status = new StatusBar();
            dock.Children.Add(status);
            DockPanel.SetDock(status, Dock.Bottom);

            // StatusBarItem 생성
            itemDateTime = new StatusBarItem();
            itemDateTime.HorizontalAlignment = HorizontalAlignment.Right;
            status.Items.Add(itemDateTime);

            // StatusBarItem을 갱신하기 위해 타이머 생성
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromSeconds(1);
            tmr.Tick += TimerOnTick;
            tmr.Start();
        }

        private void TimerOnTick(object? sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            itemDateTime.Content = dt.ToLongDateString() + " " + dt.ToLongTimeString();
        }
    }
}
