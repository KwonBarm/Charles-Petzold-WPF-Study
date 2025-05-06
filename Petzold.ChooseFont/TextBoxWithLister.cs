using System.Windows.Controls;
using System.Windows.Input;

namespace Petzold.ChooseFont
{
    internal class TextBoxWithLister : ContentControl
    {
        TextBox txtbox;
        Lister lister;
        bool isReadOnly;

        // Public 이벤트
        // SelectionChanged, TextChanged 이벤트를 발생시키기 위한 이벤트 핸들러, 다른 클래스에서 이 이벤트를 구독하여 처리할 수 있도록 함
        public event EventHandler SelectionChanged;
        public event TextChangedEventHandler TextChanged;

        // 생성자
        public TextBoxWithLister()
        {
            // ContentControl의 Content를 위해 DockPanel을 생성
            DockPanel dock = new DockPanel();
            Content = dock;

            // 상단에 위치할 TextBox
            txtbox = new TextBox();
            txtbox.TextChanged += TextBoxOnTextChanged;
            dock.Children.Add(txtbox);
            DockPanel.SetDock(txtbox, Dock.Top);

            // DockPanel의 나머지에 Lister를 추가
            lister = new Lister();
            lister.SelectionChanged += ListerOnSelectionChanged;
            dock.Children.Add(lister);
        }

        // TextBox 항목과 관련된 Public 속성
        public string Text
        {
            get { return txtbox.Text; }
            set { txtbox.Text = value; }
        }

        public bool IsReadOnly
        {
            set { isReadOnly = value; }
            get { return isReadOnly; }
        }

        // Lister 인스턴스의 SelectedItem과 TextBoxWithLister의 SelectedItem을 연결
        // SeletedItem이 null이 아니면 TextBox에 선택된 항목을 문자열로 표시, null이면 TextBox를 비움
        public object SelectedItem
        {
            set
            {
                lister.SelectedItem = value;

                if (lister.SelectedItem != null)
                {
                    txtbox.Text = lister.SelectedItem.ToString();
                }
                else
                    txtbox.Text = "";
            }
            get { return lister.SelectedItem; }
        }

        // Lister 인스턴스의 SelectedIndex와 TextBoxWithLister의 SelectedIndex를 연결
        // SeletedIndex가 -1이면 TextBox를 비우고, 그렇지 않으면 TextBox에 선택된 항목을 문자열로 표시
        public int SelectedIndex
        {
            set
            {
                lister.SelectedIndex = value;

                if (lister.SelectedIndex == -1)
                    txtbox.Text = "";
                else
                    txtbox.Text = lister.SelectedItem.ToString();
            }
            get
            {
                return lister.SelectedIndex;
            }
        }

        public void Add(object obj)
        {
            lister.Add(obj);
        }

        public void Insert(int index, object obj)
        {
            lister.Insert(index, obj);
        }

        public void Clear()
        {
            lister.Clear();
        }

        public bool Contains(object obj)
        {
            return lister.Contains(obj);
        }

        // 마우스를 클릭하면 TextBoxWithLister에 키보드 포커스를 설정
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
        }

        // 키보드가 포커스를 갖게 되면 TextBox에 포커스를 설정
        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);

            // 포커스가 TextBoxWithLister에 들어오면 TextBox에 포커스를 설정
            if (e.NewFocus == this)
            {
                txtbox.Focus();
                if (SelectedIndex == -1 && lister.Count > 0)
                    SelectedIndex = 0;
            }
        }

        // 첫 문자를 입력하면 이 값을 GoToLetter 메소드로 넘김
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);

            if (IsReadOnly)
            {
                lister.GoToLetter(e.Text[0]);
                e.Handled = true;
            }
        }

        // 선택 항목을 변경하기 위해 커서 이동키를 처리
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (SelectedIndex == -1)
                return;

            switch (e.Key)
            {
                case Key.Home:
                    if (lister.Count > 0)
                        SelectedIndex = 0;
                    break;
                case Key.End:
                    if (lister.Count > 0)
                        SelectedIndex = lister.Count - 1;
                    break;
                case Key.Up:
                    if (SelectedIndex > 0)
                        SelectedIndex--;
                    break;
                case Key.Down:
                    if (SelectedIndex < lister.Count - 1)
                        SelectedIndex++;
                    break;
                case Key.PageUp:
                    lister.PageUp();
                    break;
                case Key.PageDown:
                    lister.PageDown();
                    break;
            }

            e.Handled = true;
        }

        // Lister의 SelectedChanged 이벤트를 처리
        private void ListerOnSelectionChanged(object? sender, EventArgs e)
        {
            if (SelectedIndex == -1)
                txtbox.Text = "";
            else
                txtbox.Text = lister.SelectedItem.ToString();

            OnSelectionChanged(e);
        }

        // TextBox의 Text가 변경되면 TextChanged 이벤트를 발생시킴
        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }

        //
        private void OnSelectionChanged(EventArgs e)
        {
            if(SelectionChanged != null)
                SelectionChanged(this, e);
        }

    }
}