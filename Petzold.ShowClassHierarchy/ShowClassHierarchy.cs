using System.Windows;
using System.Windows.Threading;

namespace Petzold.ShowClassHierarchy
{
    internal class ShowClassHierarchy : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ShowClassHierarchy());
        }

        public ShowClassHierarchy()
        {
            Title = "Show Class Hierarchy";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // ClassHierarchyTreeView 생성
            ClassHierarchyTreeView tree = new ClassHierarchyTreeView(typeof(DispatcherObject));

            Content = tree;
        }
    }
}
