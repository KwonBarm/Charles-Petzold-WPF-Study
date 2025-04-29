using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Petzold.ChooseFont
{
    internal class Lister : ContentControl
    {
        ScrollViewer scroll;
        StackPanel stack;
        ArrayList list = new ArrayList();
        int indexSelected = -1;

        // Public 이벤트
        public event EventHandler SelectionChanged;

        // 생성자
        public Lister()
        {
            Focusable = false;

            // ContentControl의 Content를 Border로 설정
            Border bord = new Border();
            bord.BorderThickness = new Thickness(1);
            bord.BorderBrush = SystemColors.ActiveBorderBrush;
            bord.Background = SystemColors.WindowBrush;
            Content = bord;

            // Border의 자식으로 ScrollViewer를 설정
            scroll = new ScrollViewer();
            scroll.Focusable = false;
            scroll.Padding = new Thickness(2, 0, 0, 0);
            bord.Child = scroll;

            // ScrollViewer의 Content로 스택 패널을 생성
            stack = new StackPanel();
            scroll.Content = stack;

            // 마우스 왼쪽 버튼에 대한 핸들러 연결
            // Lister 컨트럴 전체에 TextBlock이 발생시키는 마우스 좌클릭 이벤트를 처리하라고 설정하고 그 이벤트가 발생하면 TextBlockOnMouseLeftButtonDown 메서드를 호출
            AddHandler(TextBlock.MouseLeftButtonDownEvent, new MouseButtonEventHandler(TextBlockOnMouseLeftButtonDown));

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Lister가 처음 보여질 때 뷰에 선택된 항목을 스크롤
            ScrollIntoView();
        }

        // Lister에 항목을 추가하고, 삽입하는 Public 메서드
        public void Add(object obj)
        {
            list.Add(obj);
            TextBlock txtblk = new TextBlock();
            txtblk.Text = obj.ToString();
            stack.Children.Add(txtblk);
        }

        public void Insert(int index, object obj)
        {
            list.Insert(index, obj);
            TextBlock txtblk = new TextBlock();
            txtblk.Text = obj.ToString();
            stack.Children.Insert(index, txtblk);
        }

        public void Clear()
        {
            SelectedIndex = -1;
            stack.Children.Clear();
            list.Clear();
        }

        public bool Contains(object obj)
        {
            return list.Contains(obj);
        }

        public int Count
        {
            get { return list.Count; }
        }

        // 입력한 문자에 따라 항목이 선택되게 하기 위해 호출되는 메서드
        public void GoToLetter(char ch)
        {
            int offset = SelectedIndex + 1;

            for (int i = 0; i < Count; i++)
            {
                // offset을 이용하여 선택된 항목 다음부터 검색
                // i가 증가하면서 Index가 증가하며 전체 항목을 순환함
                int index = (i + offset) % Count;

                // 입력한 문자와 list 항목의 첫 글자가 같으면 index 값을 SelectedIndex에 설정
                if (Char.ToUpper(ch) == Char.ToUpper(list[index].ToString()[0]))
                {
                    SelectedIndex = index;
                    break;
                }
            }
        }

        // 선택바를 출력하기 위한 SelectedIndex 프로퍼티
        public int SelectedIndex
        {
            set
            {
                if (value < -1 || value >= Count)
                    throw new ArgumentOutOfRangeException("SelectedIndex");

                if (value == indexSelected)
                    return;

                if (indexSelected != -1)
                {
                    TextBlock txtblk = stack.Children[indexSelected] as TextBlock;
                    txtblk.Background = SystemColors.WindowBrush;
                    txtblk.Foreground = SystemColors.WindowTextBrush;
                }

                indexSelected = value;

                if (indexSelected > -1)
                {
                    TextBlock txtblk = stack.Children[indexSelected] as TextBlock;
                    txtblk.Background = SystemColors.HighlightBrush;
                    txtblk.Foreground = SystemColors.HighlightTextBrush;
                }

                ScrollIntoView();

                // SelectionChanged 이벤트 트리거
                OnSelectionChanged(EventArgs.Empty);
            }
            get { return indexSelected; }
        }

        // SelectedItem 프로퍼티는 SeletectIndex를 이용
        public object SelectedItem
        {
            set
            {
                SelectedIndex = list.IndexOf(value);
            }
            get
            {
                if (SelectedIndex > -1)
                    return list[SelectedIndex];

                return null;
            }
        }

        // 리스트에서 페이지 업, 페이지 다운하는 Public 메서드
        // ExtentHeight : ScrollViewer의 전체 높이
        // ViewportHeight : ScrollViewer의 보이는 높이
        public void PageUp()
        {
            if (SelectedIndex == -1 || Count == 0)
                return;

            int index = SelectedIndex = (int)(Count * scroll.ViewportHeight / scroll.ExtentHeight);

            if (index < 0)
                index = 0;

            SelectedIndex = index;
        }

        public void PageDown()
        {
            if(SelectedIndex == -1 || Count == 0)
                return;

            int index = SelectedIndex + (int)(Count * scroll.ViewportHeight / scroll.ExtentHeight);

            if (index > Count - 1)
                index = Count - 1;

            SelectedIndex = index;
        }

        // ScrollViewer에서 선택 항목을 스크롤하는 Private 메서드
        private void ScrollIntoView()
        {
            // scroll.ViewportHeight > scroll.ExtentHeight 스크롤바가 필요 없을 때
            if (Count == 0 || SelectedIndex == -1 || scroll.ViewportHeight > scroll.ExtentHeight)
                return;

            double heightPerItem = scroll.ExtentHeight / Count;
            double offsetItemTop = SelectedIndex * heightPerItem;
            double offsetItemBot = (SelectedIndex + 1) * heightPerItem;

            // VerticalOffset : ScrollViewer의 현재 스크롤 위치
            if (offsetItemTop < scroll.VerticalOffset)
                scroll.ScrollToVerticalOffset(offsetItemTop);

            else if (offsetItemBot > scroll.VerticalOffset)
                scroll.ScrollToVerticalOffset(scroll.VerticalOffset + offsetItemBot - scroll.VerticalOffset - scroll.ViewportHeight);
        }

        // 이벤트 핸들러와 트리거
        private void TextBlockOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.Source is TextBlock)
                SelectedIndex = stack.Children.IndexOf(e.Source as TextBlock);
        }

        protected virtual void OnSelectionChanged(EventArgs empty)
        {
            if(SelectionChanged != null)
                SelectionChanged(this, empty);
        }
    }
}
