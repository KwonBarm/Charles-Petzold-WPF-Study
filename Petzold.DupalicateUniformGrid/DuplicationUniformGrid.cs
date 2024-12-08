using System.Windows;
using System.Windows.Controls;

namespace Petzold.DupalicateUniformGrid
{
    internal class DuplicationUniformGrid : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new DuplicationUniformGrid());
        }

        public DuplicationUniformGrid()
        {
            Title = "Duplication Uniform Grid";

            // UniformGridAlmost를 생성해서 Window의 Content로 설정
            UniformGridAlmost unigrid = new UniformGridAlmost();
            unigrid.Columns = 5;
            Content = unigrid;

            // UniformGridAlmost에 임의의 크기를 가진 버튼을 채움
            Random rand = new Random();

            for(int index=0; index<46; index++)
            {
                Button btn = new Button();
                btn.Name = "Button" + index;
                btn.Content = btn.Name;
                btn.FontSize += rand.Next(10);
                unigrid.Children.Add(btn);
            }

            AddHandler(Button.ClickEvent,new RoutedEventHandler(ButtonOnClick));
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            MessageBox.Show(btn.Name + "has been clicked",Title);
        }
    }
}
