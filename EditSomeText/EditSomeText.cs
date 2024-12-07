using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EditSomeText
{
    class EditSomeText : Window
    {
        static string strFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Barem\\EditSomeText\\EditSomeText.txt");

        TextBox txtbox;

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new EditSomeText());
        }

        public EditSomeText()
        {
            Title = "Edit Some Text";

            txtbox = new TextBox();
            txtbox.AcceptsReturn = true;
            txtbox.TextWrapping = TextWrapping.Wrap;
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            txtbox.KeyDown += TextBoxOnKeyDown;

            Content = txtbox;

            try
            {
                txtbox.Text = File.ReadAllText(strFileName);
            }
            catch
            {

            }

            txtbox.CaretIndex = txtbox.Text.Length;
            txtbox.Focus();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            try
            {
                //MessageBoxResult result = MessageBox.Show("Close program ?", Title, MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                //e.Cancel = (result == MessageBoxResult.No);

                Directory.CreateDirectory(Path.GetDirectoryName(strFileName));
                File.WriteAllText(strFileName, txtbox.Text);
            }
            catch (Exception ex)
            {
                MessageBoxResult result=
                    MessageBox.Show("File coult not be saved: " + ex.Message
                    + "\nClose program anyway?", Title,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Exclamation);

                e.Cancel = (result == MessageBoxResult.No);
            }
        }
        private void TextBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                txtbox.SelectedText = DateTime.Now.ToString();
                txtbox.CaretIndex = txtbox.SelectionStart + txtbox.SelectionLength;
            }
        }
    }
}
