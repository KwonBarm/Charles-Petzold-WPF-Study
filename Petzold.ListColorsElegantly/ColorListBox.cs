using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;

namespace Petzold.ListColorsElegantly
{
    internal class ColorListBox : ListBox
    {
        public ColorListBox()
        {
            PropertyInfo[] props = typeof(Colors).GetProperties();

            foreach (PropertyInfo prop in props)
            {
                ColorListBoxItem item = new ColorListBoxItem();
                item.Text = prop.Name;
                item.Color = (Color)prop.GetValue(null, null);
                Items.Add(item);
            }

            SelectedValuePath = "Color";
        }

        public Color SelectedColor
        {
            get { return (Color)SelectedValue; }
            set { SelectedValue = value; }
        }
    }
}
