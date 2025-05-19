using System.Reflection;
using System.Windows;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,            //where theme specific resource dictionaries are located
                                                //(used if a resource is not found in the page,
                                                // or application resource dictionaries)

    // /Themes/Generic.xaml�� �ִ� ResourceDictionary�� �ڵ����� ã�Ƽ� ���
    ResourceDictionaryLocation.SourceAssembly   //where the generic resource dictionary is located
                                                //(used if a resource is not found in the page,
                                                // app, or any theme specific resource dictionaries)
)]

// Visual Studio���� �ڵ� ������ AssemblyInfo.cs ������ �ּ� ó��
// [assembly: AssemblyTitle("NotepadClone")] 
// [assembly: AssemblyProduct("NotepadClone")]
// [assembly: AssemblyCompany("www.charlespetzold.com")]
// [assembly: AssemblyVersion("1.0.0.0")]
// [assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyDescription("A simple notepad clone application.")]
[assembly: AssemblyCopyright("\x00A9 2006 by Charles Petzold")]