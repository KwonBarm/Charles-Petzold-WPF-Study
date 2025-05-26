using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Petzold.NotepadClone
{
    public partial class NotepadClone : Window
    {
        // 대화상자에서 파일 열기와 저장을 위한 필터
        protected string strFilter = "Text Documents(*.txt)|*.txt|All Files (*.*)|*.*";

        void AddFileMenu(Menu menu)
        {
            // 탑 레벨 File 항목 생성
            MenuItem itemFile = new MenuItem();
            itemFile.Header = "_File";
            menu.Items.Add(itemFile);

            // New 메뉴 항목
            MenuItem itemNew = new MenuItem();
            itemNew.Header = "_New";
            itemNew.Command = ApplicationCommands.New;
            itemFile.Items.Add(itemNew);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, NewOnExecute));

            // Open 메뉴 항목
            MenuItem itemOpen = new MenuItem();
            itemOpen.Header = "_Open...";
            itemOpen.Command = ApplicationCommands.Open;
            itemFile.Items.Add(itemOpen);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, OpenOnExecute));

            // Save 메뉴 항목
            MenuItem itemSave = new MenuItem();
            itemSave.Header = "_Save";
            itemSave.Command = ApplicationCommands.Save;
            itemFile.Items.Add(itemSave);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, SaveOnExecute));

            // Save As 메뉴 항목
            MenuItem itemSaveAs = new MenuItem();
            itemSaveAs.Header = "Save _As...";
            itemSaveAs.Command = ApplicationCommands.SaveAs;
            itemFile.Items.Add(itemSaveAs);
            CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs, SaveAsOnExecute));

            // 구분자와 인쇄 항목
            itemFile.Items.Add(new Separator());
            //AddPrintMenuItems(itemFile);
            itemFile.Items.Add(new Separator());

            // Exit 메뉴 항목
            MenuItem itemExit = new MenuItem();
            itemExit.Header = "E_xit";
            itemExit.Click += ExitOnClick;
            itemFile.Items.Add(itemExit);
        }

        // File New 커맨드 : 빈 텍스트 박스로 시작
        protected virtual void NewOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (!OkToTrash())
                return;

            txtbox.Text = "";
            strLoadedFile = null;
            isFileDirty = false;
            UpdataTitle();
        }

        // File Open 커맨드 : 대화상자를 출력하고 파일을 로딩
        private void OpenOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if(!OkToTrash())
                return;

            OpenFileDialog dig = new OpenFileDialog();
            dig.Filter = strFilter;

            if(dig.ShowDialog(this) == true)
            {
                LoadFile(dig.FileName); // FileName : OpenFileDialog에서 선택한 파일의 전체 경로
            }
        }

        // File Save 커맨드 : SaveAsExecute 실행
        private void SaveOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (strLoadedFile == null || strLoadedFile.Length == 0)
                DisplaySaveDialog("");
            else
                SaveFile(strLoadedFile);
        }

        private void SaveAsOnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            DisplaySaveDialog(strLoadedFile);
        }

        // 파일 저장 대화상자를 보여주고 파일이 저장되면 true를 반환
        private bool DisplaySaveDialog(string strFileName)
        {
            SaveFileDialog dig = new SaveFileDialog();
            dig.Filter = strFilter;
            dig.FileName = strFileName;

            if ((bool)dig.ShowDialog(this))
            {
                SaveFile(dig.FileName);
                return true;
            }

            return false;
        }

        // File Exit 커맨드 : 윈도우 종료
        private void ExitOnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // 텍스트 박스 내용이 저장될 필요가 없으면 OkToTrash를 true를 반환
        bool OkToTrash()
        {
            if (!isFileDirty)
                return true;

            // 마지막 인수 MessageBoxResult.Yes는 기본값으로, 사용자가 Enter를 누르면 Yes가 선택됨
            MessageBoxResult result =
                MessageBox.Show("The text in the file " + strLoadedFile + " has changed\n\n" +
                "Do you want to save the changes?",
                strAppTitle,
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question,
                MessageBoxResult.Yes);

            if (result == MessageBoxResult.Cancel)
                return false;
            else if (result == MessageBoxResult.No)
                return true;
            else // result == MessageBoxResult.Yes
            {
                if (strLoadedFile != null && strLoadedFile.Length > 0)
                    return SaveFile(strLoadedFile);

                return DisplaySaveDialog("");
            }
        }

        // 에러가 발생하면 메세지 박스를 출력
        void LoadFile(string strFileName)
        {
            try
            {
                txtbox.Text = File.ReadAllText(strFileName);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error on File Open: " +
                    exc.Message, strAppTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            strLoadedFile = strFileName;
            UpdataTitle();
            txtbox.SelectionStart = 0;
            txtbox.SelectionLength = 0;
            isFileDirty = false;
        }

        // 에러가 발생하면 메세지 박스를 출력
        bool SaveFile(string strFileName)
        {
            try
            {
                File.WriteAllText(strFileName, txtbox.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error On File Save: " + exc.Message,
                    strAppTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk);

                return false;
            }

            strLoadedFile = strFileName;
            UpdataTitle();
            isFileDirty = false;
            return true;
        }

    }
}
