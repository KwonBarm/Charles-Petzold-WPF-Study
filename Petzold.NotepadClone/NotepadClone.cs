using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Petzold.NotepadClone
{
    public partial class NotepadClone : Window
    {
        protected string strAppTitle; // 프로그램의 타이틀바 이름
        protected string strAppData;  // 설정 파일의 전체 파일 이름
        protected NotepadCloneSettings settings; // 설정
        protected bool isFileDirty = false; // 파일 저장 확인을 위한 플래그

        // 메인 윈도우에서 사용되는 컨트롤
        protected Menu menu;
        protected TextBox txtbox;
        protected StatusBar status;

        string strLoadedFile; // 불러올 파일의 전체 이름
        StatusBarItem statLineCol; // 줄과 열을 표시하는 상태바 아이템

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.ShutdownMode = ShutdownMode.OnMainWindowClose; // 메인 윈도우가 닫힐 때 애플리케이션 종료
            app.Run(new NotepadClone());
        }

        public NotepadClone()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // 속성에 접근하기 위해 현재 실행중인 어셈블리의 정보를 가져옴
            Assembly assembly = Assembly.GetExecutingAssembly();

            // assembly.GetCustomAttributes(...) Assembly에 붙은 커스텀 특성들 중 AssemblyTitleAttribute를 가져옴
            // false는 상속된 특성은 제외하라는 의미
            // 이 속성은 일반적으로 AssemblyInfo.cs 파일에 정의되어 있음
            // [assembly : AssemblyTitle("NotepadClone")]
            // AssemblyTitleAttribute는 하나의 속성만을 가지므로 [0]으로 첫 번째 요소를 가져옴
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
            strAppTitle = title.Title;

            // strAppData 파일 이름을 설정하기 위해 AssemblyProduct 속성을 구함
            // Environment.SpecialFolder.ApplicationData는 C:\Users\사용자\AppData\Roaming 폴더를 가리킴
            AssemblyProductAttribute product = (AssemblyProductAttribute)assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0];
            strAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Petzold\\" + product.Product + "\\" + product.Product+".Settings.xml");

            // Window Content를 위해 DockPanel을 생성
            DockPanel dock = new DockPanel();
            Content = dock;

            // 상단에 위치할 Menu를 생성
            menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);

            // 하단에 위치할 상태바 설정
            status = new StatusBar();
            dock.Children.Add(status);
            DockPanel.SetDock(status, Dock.Bottom);

            // 선과 열을 보여주기 위해 StatusBarItem을 생성
            statLineCol = new StatusBarItem();
            statLineCol.HorizontalAlignment = HorizontalAlignment.Right;
            status.Items.Add(statLineCol);
            //DockPanel.SetDock(statLineCol, Dock.Right);

            // 클라이언트 영역의 남은 부분을 채울 TextBox를 생성
            txtbox = new TextBox();
            txtbox.AcceptsReturn = true; // Enter키를 눌렀을 때 줄바꿈이 가능하도록 설정
            txtbox.AcceptsTab = true; // Tab키를 눌렀을 때 탭이 가능하도록 설정
            txtbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto; // 세로 스크롤바 자동 표시
            txtbox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto; // 가로 스크롤바 자동 표시
            txtbox.TextChanged += TextBoxOnTextChanged;
            txtbox.SelectionChanged += TextBoxOnSelectionChanged;
            dock.Children.Add(txtbox);

            // 모든 탑 레벨 메뉴 항목 생성
             AddFileMenu(menu);
            // AddEditMenu(menu);
            // AddFormatMenu(menu);
            // AddViewMenu(menu);
            // AddHelpMenu(menu);

            // 이전에 실행되면서 저장해놓은 설정을 불러옴
            settings = (NotepadCloneSettings)LoadSettings();

            // 저장된 설정을 적용
            WindowState = settings.WindowState;

            if(settings.RestoreBounds != Rect.Empty)
            {
                Left = settings.RestoreBounds.Left;
                Top = settings.RestoreBounds.Top;
                Width = settings.RestoreBounds.Width;
                Height = settings.RestoreBounds.Height;
            }

            txtbox.TextWrapping = settings.TextWrapping;
            txtbox.FontFamily = new FontFamily(settings.FontFamily);
            txtbox.FontStyle = (FontStyle)new FontStyleConverter().ConvertFromString(settings.FontStyle);
            txtbox.FontWeight = (FontWeight)new FontWeightConverter().ConvertFromString(settings.FontWeight);
            txtbox.FontStretch = (FontStretch)new FontStretchConverter().ConvertFromString(settings.FontStretch);
            txtbox.FontSize = settings.FontSize;

            // Loaded 이벤트 핸들러를 설정
            Loaded += WindowOnLoaded;

            // TextBox에 Focus 설정
            txtbox.Focus();
        }

        // 생성자를 호출했을 때 설정을 불러들이는 메서드를 오버라이딩
        protected virtual object LoadSettings()
        {
            return NotepadCloneSettings.Load(typeof(NotepadCloneSettings),strAppData);
        }


        private void WindowOnLoaded(object sender, RoutedEventArgs e)
        {
            //ApplicationCommands.New(WPF가 제공하는 표준 명령 RoutedCommand)에 연결된 이벤트 핸들러를 실행
            ApplicationCommands.New.Execute(null, this);

            // 현재 실행 중인 프로세스의 명령줄 인수를 가져옴 - MyApp.exe file1.txt -v 라고 cmd에서 실행하면 "C:\경로\MyApp.exe"와 "file1.txt", "-v"가 strArgs에 저장됨
            string[] strArgs = Environment.GetCommandLineArgs();

            if(strArgs.Length > 1)
            {
                // 첫번째 인수는 실행 파일 이름이므로 두 번째 인수부터 시작
                if (File.Exists(strArgs[1]))
                {
                    LoadFile(strArgs[1]);
                }
                else
                {
                    MessageBoxResult result =
                        MessageBox.Show("Cannot find the "+
                        Path.GetFileName(strArgs[1])+ " file.\r\n\r\n"+
                        "Do you want to create a new fiel?",
                        strAppTitle, MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question);

                    // 사용자가 "Cancel"을 클릭하면 윈도우 종료
                    if(result == MessageBoxResult.Cancel)
                        Close();

                    // "Yes"를 클릭하면 새 파일 생성
                    else if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            File.Create(strLoadedFile = strArgs[1]).Close();
                        }
                        catch(Exception exc)
                        {
                            MessageBox.Show("Error on File Creation: " +
                                exc.Message, strAppTitle,
                                MessageBoxButton.OK,
                                MessageBoxImage.Asterisk);

                            return;
                        }

                        UpdataTitle();
                    }

                    // "No"면 아무 작업도 안함
                }
            }
        }

        // OnCloseing 메서드 - Window 창이 닫히기 직전에 호출됨
        // e.Cancel = true로 설정하면 창 닫기 취소
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = !OkToTrash();
            settings.RestoreBounds = RestoreBounds;
        }

        // OnClosed 메서드 - Window 창이 닫힌 후에 호출됨
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            settings.WindowState = WindowState;
            settings.TextWrapping = txtbox.TextWrapping;

            settings.FontFamily = txtbox.FontFamily.ToString();
            settings.FontStyle = new FontStyleConverter().ConvertToString(txtbox.FontStyle);
            settings.FontWeight = new FontWeightConverter().ConvertToString(txtbox.FontWeight);
            settings.FontStretch = new FontStretchConverter().ConvertToString(txtbox.FontStretch);
            settings.FontSize = txtbox.FontSize;

            SaveSettings();
        }

        // 'settings' 객체의 Save를 호출하기 위해 메서드를 오버라이딩
        protected virtual void SaveSettings()
        {
            settings.Save(strAppData);
        }

        // 파일이 존재 여부를 확인하고, 파일이 존재하면 창의 제목을 업데이트
        private void UpdataTitle()
        {
            if (strLoadedFile == null)
                Title = "Untitled - " + strAppTitle;
            else
                Title = Path.GetFileName(strLoadedFile) + "-" + strAppTitle;
        }

        // TextBox의 Text가 변경되면 isFileDirty를 설정
        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            isFileDirty = true;
        }

        // TextBox 선택이 변경되면 상태바를 갱신
        private void TextBoxOnSelectionChanged(object sender, RoutedEventArgs e)
        {
            int iChar = txtbox.SelectionStart; // TextBox 내에서 커서가 위치한 문자 인덱스를 가져옴
            int iLine = txtbox.GetLineIndexFromCharacterIndex(iChar); // iChar이 몇 번째 줄에 있는지를 구함

            // iLine이 -1이면 줄을 찾지 못한 것
            if (iLine == -1)
            {
                statLineCol.Content = "";
                return;
            }

            int iCol = iChar - txtbox.GetCharacterIndexFromLineIndex(iLine); // iChar에서 iLine의 시작 인덱스를 빼면 열 번호를 구할 수 있음
            string str = String.Format("Line {0}, Col {1}", iLine + 1, iCol + 1); // 줄과 열 번호를 문자열로 포맷팅

            if(txtbox.SelectionLength > 0)
            {
                iChar += txtbox.SelectionLength; // 선택된 문자의 끝 위치
                iLine = txtbox.GetLineIndexFromCharacterIndex(iChar); // 선택된 문자의 끝 위치에서 줄 번호를 구함
                iCol = iChar - txtbox.GetCharacterIndexFromLineIndex(iLine); // 선택된 문자의 끝 위치에서 열 번호를 구함
                str += String.Format(" - Line {0}, Col {1}", iLine + 1, iCol + 1); // 선택된 문자의 줄과 열 번호를 문자열로 포맷팅
            }

            statLineCol.Content = str; // 상태바에 줄과 열 번호를 표시
        }

    }
}
