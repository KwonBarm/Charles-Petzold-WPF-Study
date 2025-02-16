using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Petzold.ListSystemParameters._20250217;

namespace Petzold.ListSortedSystemParameters._20250217
{
    internal class ListSortedSystemParameters : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ListSortedSystemParameters());
        }

        public ListSortedSystemParameters()
        {
            Title = "List Sorted System Parameters";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 윈도우 Content를 위한 ListView 생성
            ListView lstvue = new ListView();
            Content = lstvue;

            // ListView의 View로 사용하기 위한 GridView 생성
            GridView grdvue = new GridView();
            lstvue.View = grdvue;

            // GridView 열 2개 생성
            GridViewColumn col = new GridViewColumn();
            col.Header = "Property Name";
            col.Width = 200;
            col.DisplayMemberBinding = new Binding("Name");
            grdvue.Columns.Add(col);

            col = new GridViewColumn();
            col.Header = "Value";
            col.Width = 200;
            col.DisplayMemberBinding = new Binding("Value");
            grdvue.Columns.Add(col);

            // 두 번째 열을 위한 데이터 템플릿 생성
            DataTemplate template = new DataTemplate(typeof(string));
            FrameworkElementFactory factoryTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            factoryTextBlock.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right);
            factoryTextBlock.SetBinding(TextBlock.TextProperty, new Binding("Value"));
            template.VisualTree = factoryTextBlock;
            col.CellTemplate = template;

            // 배열의 모든 시스템 파라미터를 구함
            PropertyInfo[] props = typeof(SystemParameters).GetProperties();

            // SystemParam 객체가 들어갈 SortedList 생성
            SortedList<string, SystemParam> sortlist = new SortedList<string, SystemParam>();

            // PropertyInfo 배열로 SortedList를 채움
            foreach(PropertyInfo prop in props)
            {
                SystemParam sysparam = new SystemParam();
                sysparam.Name = prop.Name;
                sysparam.Value = prop.GetValue(null, null);
                sortlist.Add(sysparam.Name, sysparam);
            }

            // ListView의 ItemsSource 속성을 설정
            lstvue.ItemsSource = sortlist.Values;
        }
    }
}
