using System.IO.IsolatedStorage;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Petzold.ExploreDependencyProperties
{
    internal class DependencyPropertyListView : ListView
    {
        // Type에 대한 의존성 속성 정의
        public static readonly DependencyProperty TypeProperty;

        // 정적 생성자에 의존성 속성 초기화
        static DependencyPropertyListView()
        {
            TypeProperty = DependencyProperty.Register("Type", typeof(Type), typeof(DependencyPropertyListView), new PropertyMetadata(null, new PropertyChangedCallback(OnTypePropertyChanged)));
        }

        // TypeProperty가 변경될 때 호출되는 정적 메서드
        private static void OnTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // 관련된 ListView 객체를 구함
            // d는 해당 의존성 속성을 포함하는 객체
            DependencyPropertyListView lstvue = d as DependencyPropertyListView;

            // Type 프로퍼티의 새로운 값을 구함
            Type type = e.NewValue as Type;

            // ListView에 현재 저장되어 있는 모든 항목을 제거
            lstvue.ItemsSource = null;

            // Type 객체의 모든 의존성 속성을 구함
            if(type != null)
            {
                SortedList<string, DependencyProperty> list = new SortedList<string, DependencyProperty>();

                FieldInfo[] infos = type.GetFields();

                foreach (FieldInfo info in infos)
                    if (info.FieldType == typeof(DependencyProperty))
                        list.Add(info.Name, (DependencyProperty)info.GetValue(null));

                // ListView에 ItemsSource로 설정
                lstvue.ItemsSource = list.Values;
            }
        }

        // public Type 속성
        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // 생성자
        public DependencyPropertyListView()
        {
            // GridView를 생성해서 View 프로퍼티에 설정
            GridView grdvue = new GridView();
            View = grdvue;

            // 첫 번째 열에 의존성 속성의 Name 속성을 표시
            GridViewColumn col = new GridViewColumn();
            col.Header = "Name";
            col.Width = 150;
            col.DisplayMemberBinding = new Binding("Name");
            grdvue.Columns.Add(col);

            // 두 번째 열의 레이블은 'Owner'
            col = new GridViewColumn();
            col.Header = "Owner";
            col.Width = 100;
            grdvue.Columns.Add(col);

            // 두 번째 열에 의존성 속성은 'OwnerType' 출력
            // 이 열은 데이터 템플릿이 필요
            DataTemplate template = new DataTemplate();
            col.CellTemplate = template;

            // 텍스트 블록에 데이터를 출력
            FrameworkElementFactory elTextBolock = new FrameworkElementFactory(typeof(TextBlock));
            template.VisualTree = elTextBolock;

            // TypeToString Converter를 이용해서 의존성 속성의 OwnerType 속성을 TextBlock의 Text 속성과 바인딩
            Binding bind = new Binding("OwnerType");
            bind.Converter = new TypeToString();
            elTextBolock.SetBinding(TextBlock.TextProperty, bind);

            // 세 번째 열의 레이블은 'Type'
            col = new GridViewColumn();
            col.Header = "Type";
            col.Width = 100;
            grdvue.Columns.Add(col);

            // 'PropertyType' 과 바인딩하기 위해 유사한 템플릿이 필요함
            template = new DataTemplate();
            col.CellTemplate = template;
            elTextBolock = new FrameworkElementFactory(typeof(TextBlock));
            template.VisualTree = elTextBolock;
            bind = new Binding("PropertyType");
            bind.Converter = new TypeToString();
            elTextBolock.SetBinding(TextBlock.TextProperty, bind);

            // 네 번재 열의 레이블은 'Default'이고 DefaultMetadata.DefaultValue 속성을 출력
            col = new GridViewColumn();
            col.Header = "Default";
            col.Width = 75;
            col.DisplayMemberBinding = new Binding("DefaultMetadata.DefaultValue");
            grdvue.Columns.Add(col);

            // 다섯 번째 열도 유사함
            col = new GridViewColumn();
            col.Header = "Read-Only";
            col.Width = 75;
            col.DisplayMemberBinding = new Binding("DefaultMetadata.ReadOnly");
            grdvue.Columns.Add(col);

            // 여섯 번째 열도 유사함
            col = new GridViewColumn();
            col.Header = "Usage";
            col.Width = 75;
            col.DisplayMemberBinding = new Binding("DefaultMetadata.AttachedPropertyUsage");
            grdvue.Columns.Add(col);

            // 일곱 번째 열은 메타 데이터 플래그를 출력
            col = new GridViewColumn();
            col.Header = "Flags";
            col.Width = 200;
            grdvue.Columns.Add(col);

            // MetadataToFlags를 이용해 변환하기 위해서 템플릿 필요
            template = new DataTemplate();
            col.CellTemplate = template;
            elTextBolock = new FrameworkElementFactory(typeof(TextBlock));
            template.VisualTree = elTextBolock;
            bind = new Binding("DefaultMetadata");
            bind.Converter = new MetadataToFlags();
            elTextBolock.SetBinding(TextBlock.TextProperty, bind);
        }

    }
}
