using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Petzold.NotepadClone
{
    public class NotepadCloneSettings
    {
        // 기본 설정
        public WindowState WindowState = WindowState.Normal;
        public Rect RestoreBound = Rect.Empty;
        public TextWrapping TextWrapping = TextWrapping.NoWrap;
        public string FontFamily = "";
        public string FontStyle = new FontStyleConverter().ConvertToString(FontStyles.Normal);
        public string FontWeight = new FontWeightConverter().ConvertToString(FontWeights.Normal);
        public string FontStretch = new FontStretchConverter().ConvertToString(FontStretches.Normal);
        public double FontSize = 11;

        // 현재 객체를 XML로 직렬화하여 지정된 파일에 저장
        public virtual bool Save(string strAppData)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(strAppData)); // strAppData = "C:\\Temp\\MyData\\NotepadCloneSettings.xml" → "C:\\Temp\\MyData" 폴더가 없으면 생성
                StreamWriter write = new StreamWriter(strAppData); // strAppData 경로로 Stream을 쓰기 모드로 염, 파일이 있으면 덮어씀
                XmlSerializer xml = new XmlSerializer(GetType()); // GetType()은 현재 객체의 타입을 반환 
                xml.Serialize(write, this); // this 객체를 XML형식으로 직렬화하여 write에 저장
                write.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        // 설정을 파일에서 불러들임
        public static object Load(Type type, string strAppData)
        {
            StreamReader reader;
            object settings;
            XmlSerializer xml = new XmlSerializer(type);

            try
            {
                reader = new StreamReader(strAppData); // strAppData 경로로 Stream을 읽기 모드로 염, 파일이 없으면 예외 발생
                settings = xml.Deserialize(reader); // reader에서 XML형식으로 직렬화된 객체를 역직렬화하여 settings에 저장
                reader.Close();
            }
            catch
            {
                settings = type.GetConstructor(System.Type.EmptyTypes).Invoke(null); // 역직렬화에 실패하면 기본 생성자를 호출하여 새로운 객체를 생성
            }

            return settings; // 역직렬화된 객체를 반환
        }
    }
}
