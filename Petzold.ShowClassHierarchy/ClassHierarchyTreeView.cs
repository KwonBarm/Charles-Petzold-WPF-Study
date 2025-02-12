using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Petzold.ShowClassHierarchy
{
    internal class ClassHierarchyTreeView : TreeView
    {
        public ClassHierarchyTreeView(Type typeRoot)
        {
            // PresentationCore가 로딩되었는지 확인
            UIElement dummy = new UIElement();

            // Assembly 리스트 생성
            List<Assembly> assemblies = new List<Assembly>();

            // 참조하는 어셈블리를 모두 구함
            // Assembly.GetExecutingAssembly() : 현재 실행되고 있는 어셈블리를 반환
            // Assembly.GetReferencedAssemblies() : 현재 어셈블리가 참조하는 어셈블리를 반환
            AssemblyName[] anames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();

            // 어셈블리 목록에 추가
            foreach(AssemblyName aname in anames)
                assemblies.Add(Assembly.Load(aname));

            // sorted 리스트에 typeRoot의 하위 요소를 저장
            // SortedList<TKey, TValue> : 키와 값을 기준으로 자동 정렬되는 컬렉션
            SortedList<string, Type> classes = new SortedList<string, Type>();
            classes.Add(typeRoot.Name, typeRoot);

            // 어셈블리의 모든 타입을 구합
            // IsPublic : public으로 선언된 멤버인지 여부를 나타내는 값을 가져옴
            // IsSubclassOf(Type) : 현재 Type이 지정된 Type의 파생 클래스인지 여부를 나타내는 값을 가져옴
            // 결과적으로 public으로 선언된 typeRoot의 파생 클래스를 classes에 추가
            foreach (Assembly assembly in assemblies)
                foreach (Type type in assembly.GetTypes())
                    if (type.IsPublic && type.IsSubclassOf(typeRoot))
                        classes.Add(type.Name, type);

            // 루트 항목 생성
            TypeTreeViewItem item = new TypeTreeViewItem(typeRoot);
            Items.Add(item);

            // 재귀적 메소드 호출
            CreateLinkedItems(item, classes);
        }

        // ClassHierarchyTreeView을 생성할때 인자로 받은 Type 형식의 typeRoot를 기준으로 만든 TypeTreeViewItem와
        // typeRoot의 파생 클래스를 가지고 있는 SortedList<string, Type> list를 인자로 넘겨줌
        // KeyValuePair<TKey, TValue> : 키와 값을 나타내는 구조체
        // SortedList<string, Type> list의 모든 요소를 KeyValuePair로 받아서 key와 value 구조체 형식으로 kvp에 저장
        // kvp.Value.BaseType == itemBase.Type : kvp의 value의 BaseType가 itemBase의 Type과 같다면
        // BaseType 속성은 현재 Type의 기본 클래스를 가져옴
        private void CreateLinkedItems(TypeTreeViewItem itemBase, SortedList<string, Type> list)
        {
            foreach(KeyValuePair<string, Type> kvp in list)
            {
                if(kvp.Value.BaseType == itemBase.Type)
                {
                    TypeTreeViewItem item = new TypeTreeViewItem(kvp.Value);
                    itemBase.Items.Add(item);
                    CreateLinkedItems(item, list);
                }
            }
        }
    }
}
