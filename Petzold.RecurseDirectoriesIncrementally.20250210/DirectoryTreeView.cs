using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Petzold.RecurseDirectoriesIncrementally._20250210
{
    internal class DirectoryTreeView : TreeView
    {
        // 생성자는 디렉토리 트리 일부를 생성
        public DirectoryTreeView()
        {
            RefreshTree();
        }

        private void RefreshTree()
        {
            // BeginInit() - ISupportInitialize 인터페이스를 구현하는 클래스들이 초기화 프로세스를 시작할 때 호출하는 메서드
            // FrameworkElement는 ISupportInitialize 인터페이스를 구현하므로 BeginInit() 메서드를 호출할 수 있음
            BeginInit();
            Items.Clear();

            // 현재 시스템에 연결된 모드 드라이브(디스크)를 가져오는 코드
            DriveInfo[] drives = DriveInfo.GetDrives(); //  C:\, D:\, E:\

            foreach (DriveInfo drive in drives)
            {
                char chDrive = drive.Name.ToUpper()[0]; // C, D, E
                DirectoryTreeViewItem item = new DirectoryTreeViewItem(drive.RootDirectory); // C:\, D:\, E:\

                // 드라이브가 준비되면 볼륨 레이블을 보여주고,
                // 준비가 안 되면 DriveType을 보여줌
                if (chDrive != 'A' && chDrive != 'B' && drive.IsReady && drive.VolumeLabel.Length > 0)
                    item.Text = String.Format("{0} ({1})", drive.VolumeLabel, drive.Name);
                else
                    item.Text = String.Format("{0} ({1})", drive.DriveType, drive.Name);

                // 드라이브에 맞는 비트맵을 결정
                if (chDrive == 'A' || chDrive == 'B')
                    item.SelectedImage = item.UnSelectedImage = new BitmapImage(new Uri("pack://application:,,/Images/35FLOPPY.png"));
                else if (drive.DriveType == DriveType.CDRom)
                    item.SelectedImage = item.UnSelectedImage = new BitmapImage(new Uri("pack://application:,,/Images/CDDRIVE.png"));
                else
                    item.SelectedImage = item.UnSelectedImage = new BitmapImage(new Uri("pack://application:,,/Images/DRIVE.png"));

                Items.Add(item);

                // 드라이브에 맞는 디렉토리를 맵핑
                if (chDrive !='A' && chDrive != 'B' && drive.IsReady)
                    item.Populate();
            }

            EndInit();
        }
    }
}
