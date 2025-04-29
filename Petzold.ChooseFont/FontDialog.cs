using System.Reflection.Emit;
using System.Windows;
using System.Windows.Media;

namespace Petzold.ChooseFont
{
    internal class FontDialog : Window
    {
        TextBoxWithLister boxFamiliy, boxStyle, boxWeight, boxStretch, boxSize;
        Label lblDisplay;
        bool isUpdateSuppressed = true;

        public Typeface Typeface
        {
            set
            {
            }
        }
    }
}
