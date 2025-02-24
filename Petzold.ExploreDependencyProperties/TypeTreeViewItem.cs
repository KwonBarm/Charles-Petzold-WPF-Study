using System.Windows.Controls;

namespace Petzold.ShowClassHierarchy
{
    internal class TypeTreeViewItem : TreeViewItem
    {
        Type type;

        // 두 개의 생성자
        public TypeTreeViewItem() { }
        public TypeTreeViewItem(Type type)
        {
            Type = type;
        }

        // Type 타입의 Public Type 속성
        public Type Type
        {
            set
            {
                type = value;

                if(type.IsAbstract)
                    Header = type.Name + " (abstract)";
                else
                    Header = type.Name;
            }
            get => type;
        }
    }
}
