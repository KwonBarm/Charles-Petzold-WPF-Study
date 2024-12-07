using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Petzold.BuildButtonFactory
{
    internal class BuildButtonFactory : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new BuildButtonFactory());
        }

        public BuildButtonFactory()
        {
            Title = "Build a Button Factory";

            // Button 객체를 위한 ControlTemplate 생성
            ControlTemplate template = new ControlTemplate(typeof(Button));

            // Border 클래스에 대해 FrameworkElementFactory 객체 생성
            FrameworkElementFactory factoryBorder = new FrameworkElementFactory(typeof(Border));

            // 나중에 참조하기 위해 이름을 정함
            factoryBorder.Name = "border";

            // 기본 프로퍼티들을 설정
            factoryBorder.SetValue(Border.BorderBrushProperty, Brushes.Red);
            factoryBorder.SetValue(Border.BorderThicknessProperty, new Thickness(3));
            factoryBorder.SetValue(Border.BackgroundProperty, SystemColors.ControlLightBrush);

            // ContentPresenter 클래스에 대해 FrameworkElementFactory를 생성
            FrameworkElementFactory factoryContent = new FrameworkElementFactory(typeof(ContentPresenter));

            // 나중에 참조하기 위해 이름을 정함
            factoryContent.Name = "content";

            // 몇 가지 ContentPresenter 프로퍼티를 Button 프로퍼티에 바인딩
            factoryContent.SetValue(ContentPresenter.ContentProperty, new TemplateBindingExtension(Button.ContentProperty));

            // 버튼의 Padding은 컨텐트의 Margin임
            factoryContent.SetValue(ContentPresenter.MarginProperty, new TemplateBindingExtension(Button.PaddingProperty));

            // ContentPresenter를 Border의 자식으로 설정
            factoryBorder.AppendChild(factoryContent);

            // Border를 비주얼 트리의 루트 엘리먼트로 만듬
            template.VisualTree = factoryBorder;

            // IsMouseOver가 True가 될때의 Trigger를 정의
            Trigger trig = new Trigger();
            trig.Property = UIElement.IsMouseOverProperty;
            trig.Value = true;

            // 이 트리거와 연관된 Setter를 정의
            // "border" 엘리멘트의 CornerRadius 프로퍼티를 바꿈
            Setter set = new Setter();
            set.Property = Border.CornerRadiusProperty;
            set.Value = new CornerRadius(24);
            set.TargetName = "border";

            // Trigger의 Setters 컬렉션에 Setter를 추가
            trig.Setters.Add(set);

            // 비슷한 방법으로 FontStyle을 변경시키는 Setter를 추가
            set = new Setter();
            set.Property = Control.FontStyleProperty;
            set.Value = FontStyles.Italic;

            // 전과 같은 방법으로, 이 Setter를 Trigger의 Setters 컬렉션에 추가
            trig.Setters.Add(set);

            template.Triggers.Add(trig);

            // 비슷한 방법으로 IsPressed에 대한 트리거를 정의
            trig = new Trigger();
            trig.Property = ButtonBase.IsPressedProperty;
            trig.Value = true;
            set = new Setter();
            set.Property = Border.BackgroundProperty;
            set.Value = SystemColors.ControlDarkBrush;
            set.TargetName = "border";

            // Trigger의 Setters 컬렉션에 Setter를 추가
            trig.Setters.Add(set);

            // 트리거를 템플릿에 추가
            template.Triggers.Add(trig);

            // 마지막으로 Button을 생성
            Button btn = new Button();

            // 템플릿 지정
            btn.Template = template;

            // 기타 프로퍼티 정의
            btn.Content = "Button with Custom Template";
            btn.Padding = new Thickness(20);
            btn.FontSize = 48;
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Click += ButtonOnClick;
            Content = btn;
        }

        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The button has been clicked", Title);
        }
    }
}
