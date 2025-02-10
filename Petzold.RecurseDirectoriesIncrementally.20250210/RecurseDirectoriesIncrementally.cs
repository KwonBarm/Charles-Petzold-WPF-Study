using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.RecurseDirectoriesIncrementally._20250210
{
    internal class RecurseDirectoriesIncrementally : Window
    {
        StackPanel stack;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new RecurseDirectoriesIncrementally());
        }

        public RecurseDirectoriesIncrementally()
        {
            Title = "Recurse Directories Incrementally";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 윈도우 Content를 위한 Grid 생성
            Grid grid = new Grid();
            Content = grid;

            // ColumnDefinition 객체 정의
            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = new GridLength(50, GridUnitType.Star);
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(50, GridUnitType.Star);
            grid.ColumnDefinitions.Add(coldef);

            // 왼쪽 면의 DirectoryTreeView를 추가
            DirectoryTreeView tree = new DirectoryTreeView();
            tree.SelectedItemChanged += TreeOnSelectedItemChanged;
            grid.Children.Add(tree);
            Grid.SetColumn(tree, 0);

            // 가운데 GridSplitter를 추가
            GridSplitter split = new GridSplitter();
            split.Width = 6;
            split.ResizeBehavior = GridResizeBehavior.PreviousAndNext; // 양쪽 열을 동시에 조정하는 속성
            grid.Children.Add(split);
            Grid.SetColumn(split, 1);

            // 오른쪽에 스크롤되는 스택 패널을 추가
            ScrollViewer scroll = new ScrollViewer();
            grid.Children.Add(scroll);
            Grid.SetColumn(scroll, 2);

            stack = new StackPanel();
            scroll.Content = stack;
        }

        private void TreeOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // 선택된 항목을 구함
            DirectoryTreeViewItem item = e.NewValue as DirectoryTreeViewItem;

            // StackPanel을 지움
            stack.Children.Clear();

            // 다시 채움
            FileInfo[] infos;

            try
            {
                infos = item.DirectoryInfo.GetFiles();
            }
            catch
            {
                return;
            }

            foreach (FileInfo info in infos)
            {
                TextBlock text= new TextBlock();
                text.Text = info.Name;
                stack.Children.Add(text);
            }
        }
    }
}
