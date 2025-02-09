using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.RecurseDirectoriesIncrementally._20250210
{
    class ImagedTreeViewItem : TreeViewItem
    {
        TextBlock text;
        Image img;
        ImageSource srcSelected, srcUnselected;

        // 생성자는 이미지와 텍스트를 스택패널에 추가
        public ImagedTreeViewItem()
        {
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            Header = stack;

            img = new Image();
            img.VerticalAlignment = VerticalAlignment.Center;
            img.Margin = new Thickness(0, 0, 2, 0);
            stack.Children.Add(img);

            text = new TextBlock();
            text.VerticalAlignment = VerticalAlignment.Center;
            stack.Children.Add(text);
        }

        // 텍스트와 이미지에 대한 Public 속성
        public string Text
        {
            set { text.Text = value; }
            get { return text.Text; }
        }

        public ImageSource SelectedImage
        {
            set
            {
                srcSelected = value;

                if (IsSelected)
                    img.Source = srcSelected;
            }
            get { return srcSelected; }
        }

        public ImageSource UnSelectedImage
        {
            set
            {
                srcUnselected = value;

                if (!IsSelected)
                    img.Source = srcUnselected;
            }
            get { return srcUnselected; }
        }

        // 이미지를 설정하는 이벤트 오버라이드
        protected override void OnSelected(RoutedEventArgs e)
        {
            base.OnSelected(e);
            img.Source = srcSelected;
        }
        protected override void OnUnselected(RoutedEventArgs e)
        {
            base.OnUnselected(e);
            img.Source = srcUnselected;
        }

    }
}
