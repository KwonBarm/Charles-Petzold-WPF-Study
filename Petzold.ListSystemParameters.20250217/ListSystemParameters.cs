using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Petzold.ListSystemParameters._20250217
{
    internal class ListSystemParameters : Window
    {
        [STAThread]
        public static void Main() 
        {
            Application app = new Application();
            app.Run(new ListSystemParameters());
        }

        public ListSystemParameters()
        {
            Title = "List System Parameters";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 윈도우 Content를 위한 ListView 생성
            ListView lstvue = new ListView();
            Content = lstvue;

            // ListView의 View로 사용하기 위해 GridView 생성
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

            // 배열에서 모든 시스템 파라미터를 구함
            PropertyInfo[] props = typeof(SystemParameters).GetProperties();

            // ListView에 항목 추가
            foreach (PropertyInfo prop in props)
            {
                if (prop.PropertyType != typeof(ResourceKey))
                {
                    SystemParam sysparma= new SystemParam();
                    sysparma.Name = prop.Name;
                    sysparma.Value = prop.GetValue(null, null);
                    lstvue.Items.Add(sysparma);
                }
            }
        }
    }
}
