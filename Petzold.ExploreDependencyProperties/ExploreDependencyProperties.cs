using Petzold.ShowClassHierarchy;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.ExploreDependencyProperties
{
    internal class ExploreDependencyProperties : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ExploreDependencyProperties());
        }

        public ExploreDependencyProperties()
        {
            Title = "Explore Dependency Properties";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 윈도우 Content를 위한 Grid 생성
            Grid grid = new Grid();
            Content = grid;

            // Grid에 열을 3개 정의
            ColumnDefinition col = new ColumnDefinition();
            col.Width = new GridLength(1, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col);

            col = new ColumnDefinition();
            col.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(col);

            col = new ColumnDefinition();
            col.Width = new GridLength(3, GridUnitType.Star);
            grid.ColumnDefinitions.Add(col);

            // ClassHierarchyTreeView를 왼쪽에 배치
            ClassHierarchyTreeView treevue = new ClassHierarchyTreeView(typeof(DependencyObject));
            grid.Children.Add(treevue);
            Grid.SetColumn(treevue, 0);

            // GridSplitter를 가운데에 배치
            GridSplitter split = new GridSplitter();
            split.HorizontalAlignment = HorizontalAlignment.Center;
            split.VerticalAlignment = VerticalAlignment.Stretch;
            split.Width = 6;
            grid.Children.Add(split);
            Grid.SetColumn(split, 1);

            // DependencyProPertyListView를 오른쪽에 배치
            DependencyPropertyListView lstvue = new DependencyPropertyListView();
            grid.Children.Add(lstvue);
            Grid.SetColumn(lstvue, 2);

            // TreeView와 ListView를 연결
            lstvue.SetBinding(DependencyPropertyListView.TypeProperty, "SelectedItem.Type");
            lstvue.DataContext = treevue;

        }
    }
}
