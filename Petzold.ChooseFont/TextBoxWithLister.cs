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

        // Lister 요소의 다른 public 속성 인터페이스
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

        // 마우스를 클릭하면 키보드 포커스를 설정
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();
        }

        // 키보드가 포커스를 갖게 되면 TextBox에 포커스를 설정
        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);

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


        private void ListerOnSelectionChanged(object? sender, EventArgs e)
        {
            if (SelectedIndex == -1)
                txtbox.Text = "";
            else
                txtbox.Text = lister.SelectedItem.ToString();

            OnSelectionChanged(e);
        }

        
        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }

        private void OnSelectionChanged(EventArgs e)
        {
            if(SelectionChanged != null)
                SelectionChanged(this, e);
        }

    }
}