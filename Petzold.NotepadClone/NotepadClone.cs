using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
            // 속성에 접근하기 위해 현재 실행중인 어셈블리의 정보를 가져옴
            Assembly assembly = Assembly.GetExecutingAssembly();

            // assembly.GetCustomAttributes(...) Assembly에 붙은 커스텀 특성들 중 AssemblyTitleAttribute를 가져옴
            // false는 상속된 특성은 제외하라는 의미
            // 이 속성은 일반적으로 AssemblyInfo.cs 파일에 정의되어 있음
            // [assembly : AssemblyTitle("NotepadClone")]
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
            strAppTitle = title.Title;
        }

    }
}
