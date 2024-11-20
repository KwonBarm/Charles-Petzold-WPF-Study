using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AnimationStudy
{
    public partial class MainWindow : Window
    {
        private Storyboard myStoryBoard;
        public MainWindow()
        {
            InitializeComponent();

            var stack2 = new StackPanel();
            stack2.Margin = new Thickness(10);
            stack2.Background = Brushes.White;
            Grid.SetColumn(stack2, 1);
            allGrid.Children.Add(stack2);

            var MyRectangle2 = new Rectangle();
            MyRectangle2.Name = "myRectangle2";
            MyRectangle2.Width = 100;
            MyRectangle2.Height = 100;
            MyRectangle2.Fill = Brushes.Blue;
            stack2.Children.Add(MyRectangle2);

            this.RegisterName(MyRectangle2.Name, MyRectangle2);

            var myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 1;
            myDoubleAnimation.To = 0;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(2));
            myDoubleAnimation.AutoReverse = true;
            myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            myStoryBoard = new Storyboard();
            myStoryBoard.Children.Add(myDoubleAnimation);
            Storyboard.SetTargetName(myDoubleAnimation, MyRectangle2.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation,new PropertyPath(Rectangle.OpacityProperty));

            MyRectangle2.Loaded += MyRectangle2_Loaded;

        }

        private void MyRectangle2_Loaded(object sender, RoutedEventArgs e)
        {
            //myStoryBoard.Begin(this);
            myStoryBoard.Begin(sender as Rectangle);
        }
    }
}