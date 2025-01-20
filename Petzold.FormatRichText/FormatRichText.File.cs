using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Petzold.FormatRichText
{
    internal partial class FormatRichText : Window
    {
        string[] formats =
        {
            DataFormats.Xaml,
            DataFormats.XamlPackage,
            DataFormats.Rtf,
            DataFormats.Text,
            DataFormats.Text,
        };

        string strFilter =
            "XAML Document Files (*.xaml)|*.xaml|" +
            "XAML Package Files (*.zip)|*.zip|" +
            "Rich Text Format (*.rtf)|*.rtf|" +
            "Text Files (*.txt)|*.txt|" +
            "All Files (*.*)|*.*";

        void AddFileToolbar(ToolBarTray tray, int band, int index)
        {
            // ToolBar 생성
            ToolBar toolbar = new ToolBar();
            toolbar.Band = band;
            toolbar.BandIndex = index;
            tray.ToolBars.Add(toolbar);

            RoutedUICommand[] comm =
            {
                ApplicationCommands.New,
                ApplicationCommands.Open,
                ApplicationCommands.Save,
            };

            string[] strImages =
            {
                "NewDocumentHS.png",
                "OpenHS.png",
                "SaveHS.png",
            };

            // 툴바 버튼 생성
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button();
                btn.Command = comm[i];
                toolbar.Items.Add(btn);

                Image img = new Image();
                img.Source = new BitmapImage(new Uri("pack://Application:,,/Images/" + strImages[i]));
                //img.Stretch = Stretch.None;
                img.Width = 32;
                img.Height = 32;
                btn.Content = img;

                ToolTip tip = new ToolTip();
                tip.Content = comm[i].Text;
                btn.ToolTip = tip;
            }

            // 커맨드 바인딩 추가
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, OnNew));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OnOpen));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, OnSave));
        }

        // New: 빈 문자열을 설정
        private void OnNew(object sender, ExecutedRoutedEventArgs e)
        {
            FlowDocument flow = txtbox.Document;
            TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);

            range.Text = "";
        }

        // Open : 대화상자를 출력하고 파일을 로딩
        private void OnOpen(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dig = new OpenFileDialog();
            dig.CheckFileExists = true;
            dig.Filter = strFilter;

            if ((bool)dig.ShowDialog(this))
            {
                FlowDocument flow = txtbox.Document;
                TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);

                FileStream strm = null;

                try
                {
                    strm = new FileStream(dig.FileName, FileMode.Open);
                    range.Load(strm, formats[dig.FilterIndex - 1]);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, Title);
                }
                finally
                {
                    if (strm != null)
                        strm.Close();
                }
            }
        }

        // Save : 대화상자를 출력하고 파일을 저장
        private void OnSave(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dig = new SaveFileDialog();
            dig.Filter = strFilter;

            if ((bool)dig.ShowDialog(this))
            {
                FlowDocument flow = txtbox.Document;
                TextRange range = new TextRange(flow.ContentStart, flow.ContentEnd);

                FileStream strm = null;

                try
                {
                    strm = new FileStream(dig.FileName, FileMode.Create);
                    range.Save(strm, formats[dig.FilterIndex - 1]);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, Title);
                }
                finally
                {
                    if (strm != null)
                        strm.Close();
                }
            }
        }
    }
}
