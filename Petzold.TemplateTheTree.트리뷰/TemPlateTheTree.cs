using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Petzold.TemplateTheTree.트리뷰
{
    internal class TemplateTheTree : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new TemplateTheTree());
        }

        public TemplateTheTree()
        {
            Title = "Template the Tree";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 윈도우 Content인 TreeView 객체 생성
            TreeView tree = new TreeView();
            Content = tree;

            // DiskDirectory를 기반으로 HierarchicalDataTemplate 생성
            // HeirarchicalDataTemplate은 계층적(트리 구조) 데이터를 표현하는 데 사용하는 데이터 템플릿
            // 하이러키컬 데이터 템플릿
            HierarchicalDataTemplate template = new HierarchicalDataTemplate(typeof(DiskDirectory));

            // ItemSource로 Subdirectories 속성을 설정
            template.ItemsSource = new Binding("Subdirectories");

            // TextBlock에 대한 FrameworkElementFactory 생성
            FrameworkElementFactory factoryTextBlock = new FrameworkElementFactory(typeof(TextBlock));

            // DiskDirectory의 Name 속성과 Text 프로퍼티를 바인딩
            factoryTextBlock.SetBinding(TextBlock.TextProperty, new Binding("Name"));

            // 템플릿의 VisualTree로 이 TextBlock을 설정
            template.VisualTree = factoryTextBlock;

            // 시스템 드라이브에 대한 DiskDirectory 객체 생성
            // Environment.SystemDirectory => "C:\Windows\System32" 
            // Path.GetPathRoot(Environment.SystemDirectory) => "C:\"
            DiskDirectory dir = new DiskDirectory(new DirectoryInfo(Path.GetPathRoot(Environment.SystemDirectory)));

            // 루트 TreeViewItem을 생성하고 속성을 설정
            TreeViewItem item = new TreeViewItem();
            item.Header = dir.Name;
            item.ItemsSource = dir.Subdirectories;
            item.ItemTemplate = template;

            // TreeView에 TreeViewItem을 추가
            tree.Items.Add(item);
            item.IsExpanded = true;
        }
    }
}
