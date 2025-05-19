using System.Reflection;
using System.Windows;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,            //where theme specific resource dictionaries are located
                                                //(used if a resource is not found in the page,
                                                // or application resource dictionaries)

    // /Themes/Generic.xaml에 있는 ResourceDictionary를 자동으로 찾아서 사용
    ResourceDictionaryLocation.SourceAssembly   //where the generic resource dictionary is located
                                                //(used if a resource is not found in the page,
                                                // app, or any theme specific resource dictionaries)
)]

// Visual Studio에서 자동 생성된 AssemblyInfo.cs 파일은 주석 처리
// [assembly: AssemblyTitle("NotepadClone")] 
// [assembly: AssemblyProduct("NotepadClone")]
// [assembly: AssemblyCompany("www.charlespetzold.com")]
// [assembly: AssemblyVersion("1.0.0.0")]
// [assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyDescription("A simple notepad clone application.")]
[assembly: AssemblyCopyright("\x00A9 2006 by Charles Petzold")]