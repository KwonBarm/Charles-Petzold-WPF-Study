using System.IO;

namespace Petzold.TemplateTheTree.트리뷰
{
    internal class DiskDirectory
    {
        DirectoryInfo dirinfo;

        // DirectoryInfo 객체를 받는 생성자
        public DiskDirectory(DirectoryInfo dirinfo)
        {
            this.dirinfo = dirinfo;
        }

        // 디렉토리 이름을 반환하는 Name 속성
        public string Name
        {
            get {  return dirinfo.Name; }
        }

        // DiskDirectory 객체에 대한 컬렉션을 구해주는 Subdirectories 속성
        public List<DiskDirectory> Subdirectories
        {
            get
            {
                List<DiskDirectory> dirs = new List<DiskDirectory>();
                DirectoryInfo[] subdirs;

                try
                {
                    subdirs = dirinfo.GetDirectories();
                }
                catch
                {
                    return dirs;
                }

                foreach (DirectoryInfo subdir in subdirs)
                    dirs.Add(new DiskDirectory(subdir));

                return dirs;
            }
        }
    }
}
