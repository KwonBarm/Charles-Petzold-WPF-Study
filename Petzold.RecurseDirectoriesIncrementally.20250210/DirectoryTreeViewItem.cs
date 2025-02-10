using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Petzold.RecurseDirectoriesIncrementally._20250210
{
    internal class DirectoryTreeViewItem : ImagedTreeViewItem
    {
        DirectoryInfo dir;

        // DirectoriyInfo 객체를 받는 생성자
        public DirectoryTreeViewItem(DirectoryInfo dir)
        {
            this.dir = dir;
            Text = dir.Name;

            SelectedImage = new BitmapImage(new Uri("pack://application:,,/Images/OPENFOLD.png"));
            UnSelectedImage = new BitmapImage(new Uri("pack://application:,,/Images/CLOSEFOLD.png"));
        }

        // DirectoryInfo 객체에 대한 Public 프로퍼티
        public DirectoryInfo DirectoryInfo
        {
            get { return dir; }
        }

        // 항목들과 값을 연동시켜주는 Public 메서드 
        public void Populate()
        {
            Items.Clear();
            DirectoryInfo[] dirs;

            try
            {
                dirs = dir.GetDirectories();
            }
            catch
            {
                return;
            }

            foreach (DirectoryInfo dirChild in dirs)
                Items.Add(new DirectoryTreeViewItem(dirChild));

            // populate 하위 항목에 대한 이벤트 오버라이딩
        }

        protected override void OnExpanded(RoutedEventArgs e)
        {
            base.OnExpanded(e);

            foreach (object obj in Items)
            {
                DirectoryTreeViewItem item = obj as DirectoryTreeViewItem;
                item.Populate();
            }
        }
    }
}
