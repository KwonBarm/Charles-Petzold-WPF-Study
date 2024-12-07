using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SpaceButton
{
    class SpaceButton : Button
    {
        string txt;

        public string Text
        {
            set
            {
                txt = value;
                Content = SpaceOutText(txt);
            }

            get { return txt; }
        }

        public static readonly DependencyProperty SpacePropety;

        public int Space
        {
            set
            {
                SetValue(SpacePropety, value);
            }
            get
            {
                return (int)GetValue(SpacePropety);
            }
        }

        static SpaceButton()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.DefaultValue = 1;
            metadata.AffectsMeasure = true;
            metadata.Inherits = true;
            metadata.PropertyChangedCallback += OnSpacePropetyChanged;

            SpacePropety = DependencyProperty.Register("Space", typeof(int), typeof(SpaceButton), metadata, ValidateSpaceValue);
        }

        private static bool ValidateSpaceValue(object value)
        {
            int i = (int)value;
            return i >= 0;
        }
        private static void OnSpacePropetyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SpaceButton btn = d as SpaceButton;
            btn.Content = btn.SpaceOutText(btn.txt);
        }

        private object SpaceOutText(string txt)
        {
            if (txt == null)
                return null;

            StringBuilder build = new StringBuilder();

            foreach (char ch in txt)
                build.Append(ch + new string(' ', Space));

            return build.ToString();

        }



        

    }
}

