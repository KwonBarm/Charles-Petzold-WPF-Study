using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UriDialog
{
    internal class UriDialog : Window
    {
        TextBox? txtBox;

        public UriDialog()
        {
            Title = "Enter a URI";
            ShowInTaskbar = false;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            txtBox = new TextBox();
            txtBox.Margin = new Thickness(48);
            txtBox.AcceptsReturn = true;
            Content = txtBox;

            txtBox.Focus();
        }

        public string Text
        {
            set
            {
                txtBox.Text = value;
                txtBox.SelectionStart = txtBox.Text.Length;
            }
            get { return txtBox.Text; }
        }

        //[STAThread]
        //public static void Main()
        //{
        //    Application app = new Application();
        //    app.Run(new UriDialog());
        //}

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Close();
        }
    }
}
