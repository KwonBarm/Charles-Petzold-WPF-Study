using System.Windows;
using System.Windows.Controls;

namespace Petzold.CalculateIndex
{
    internal class RoundedButton : Control
    {
        // private 필드
        RoundedButtonDecorator decorator;

        // Public 정적 ClickEvent
        public static readonly RoutedEvent ClickEvent;

        // 정적 생성자
        static RoundedButton()
        {
            ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RoundedButton));
        }

        public RoundedButton()
        {
            decorator = new RoundedButtonDecorator();
            AddVisualChild(decorator);
            AddLogicalChild(decorator);
        }
    }
}
