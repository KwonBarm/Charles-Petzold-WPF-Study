using Microsoft.Win32;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace EditSomeRichText
{
    class EditSomeRichText : Window
    {
        RichTextBox textBox;

        string strFilter = "Document Files (*.xaml)|*.xaml|All files (*.*)|*.*";


        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new EditSomeRichText());
        }

        public EditSomeRichText()
        {
            Title = "Edit Some Rich Text";

            textBox = new RichTextBox();
            textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            Content = textBox;

            textBox.Focus();
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            // 파일 불러오기
            if (e.ControlText.Length > 0 && e.ControlText[0] == '\x0F')
            {
                OpenFileDialog dig = new OpenFileDialog();
                dig.CheckFileExists = true;
                dig.Filter = strFilter;


                if ((bool)dig.ShowDialog(this))
                {
                    FlowDocument flow = textBox.Document;
                    TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);

                    Stream strm = null;

                    try
                    {
                        strm = new FileStream(dig.FileName, FileMode.Open);
                        range.Load(strm, DataFormats.Xaml);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Title);
                    }
                    finally
                    {
                        if (strm != null)
                            strm.Close();
                    }

                    e.Handled = true;
                }
            }

            // 파일 저장하기
            if (e.ControlText.Length > 0 && e.ControlText[0] == '\x13')
            {
                SaveFileDialog dig = new SaveFileDialog();
                dig.Filter = strFilter;

                if ((bool)dig.ShowDialog(this))
                {
                    FlowDocument flow = textBox.Document;
                    TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);

                    Stream strm = null;

                    try
                    {
                        strm = new FileStream(dig.FileName, FileMode.Create);
                        range.Save(strm, DataFormats.Xaml);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Title);
                    }
                    finally
                    {
                        if (strm != null)
                            strm.Close();
                    }

                    e.Handled = true;
                }
            }

            base.OnPreviewTextInput(e);
        }
    }
}
