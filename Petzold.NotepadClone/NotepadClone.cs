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
        protected bool isFillDirty = false; // 파일 저장 확인을 위한 플래그

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
            DockPanel.SetDock(statLineCol, Dock.Right);

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
            // AddFileMenu(menu);
            // AddEditMenu(menu);
            // AddFormatMenu(menu);
            // AddViewMenu(menu);
            // AddHelpMenu(menu);

            // 이전에 실행되면서 저장해놓은 설정을 불러옴
            settings = (NotepadCloneSettings)LoadSettings();

            // 저장된 설정을 적용
            WindowState = settings.WindowState;

            if(settings.RestoreBound != Rect.Empty)
            {
                Left = settings.RestoreBound.Left;
                Top = settings.RestoreBound.Top;
                Width = settings.RestoreBound.Width;
                Height = settings.RestoreBound.Height;
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
            ApplicationCommands.New.Execute(null, this); // New 명령을 실행하여 새 문서 생성
        }

        private void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void TextBoxOnSelectionChanged(object sender, RoutedEventArgs e)
        {
        }
    }
}
