using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Petzole.RecurseDirectoriesInefficiently._20250210
{
    internal class RecurseDirectoriesInefficiently : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new RecurseDirectoriesInefficiently());
        }

        public RecurseDirectoriesInefficiently()
        {
            Title = "Recurse Directories Inefficiently";

            TreeView tree = new TreeView();
            Content = tree;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 시스템 드라이브를 기반으로 TreeViewItem 생성
            TreeViewItem item = new TreeViewItem();
            item.Header = Path.GetPathRoot(Environment.SystemDirectory);
            item.Tag = new DirectoryInfo(item.Header as string);
            tree.Items.Add(item);

            // 재귀호출로 내용을 채움
            GetSubdirectories(item);
        }

        private void GetSubdirectories(TreeViewItem item)
        {
            DirectoryInfo dir = item.Tag as DirectoryInfo;
            DirectoryInfo[] subdirs;

            try
            {
                // 하위 디렉토리를 구함
                subdirs = dir.GetDirectories();
            }
            catch
            {
                return;
            }

            // 각 하위 디렉토리에 대해 루프를 수행
            foreach (DirectoryInfo subdir in subdirs)
            {
                // 각 디렉토리에 대해 새로운 TreeViewItem 생성
                TreeViewItem subitem = new TreeViewItem();
                subitem.Header = subdir.Name;
                subitem.Tag = subdir;
                item.Items.Add(subitem);

                // 재귀적으로 하위 디렉토리를 구함
                GetSubdirectories(subitem);
            }
        }
    }
}
