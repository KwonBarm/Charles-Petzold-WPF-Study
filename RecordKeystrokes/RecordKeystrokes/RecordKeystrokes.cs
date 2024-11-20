using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RecordKeystrokes
{
    class RecordKeystrokes : Window
    {
        string str = "";
        StringBuilder build = new StringBuilder("text");

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new RecordKeystrokes());
        }

        public RecordKeystrokes()
        {
            
            Title = "Record Keystrokes";
            Content = null;
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);

            if(e.Text == "\b")
            {
                if (build.Length > 0)
                    build.Remove(build.Length - 1, 1);
            }
            else
            {
                build.Append(e.Text);
            }

            Content = build;
        }
    }
}
