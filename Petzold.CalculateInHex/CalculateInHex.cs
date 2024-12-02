using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Petzold.CalculateInHex
{
    internal class CalculateInHex : Window
    {
        RoundedButton btnDisplay;
        ulong numDisplay;
        ulong numFirst;
        bool bNewNumber = true;
        char chOperation = '=';

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CalculateInHex());
        }

        // constructor
        public CalculateInHex()
        {
            Title = "Calculate in Hex";
            SizeToContent = SizeToContent.WidthAndHeight;
            ResizeMode = ResizeMode.CanMinimize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Grid를 생성하고 Content로 설정
            Grid grid = new Grid();
            grid.Margin = new Thickness(4);
            Content = grid;

            // 5개 열을 생성
            for (int i = 0; i < 5; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(col);
            }

            // 7개 행을 생성
            for (int i = 0; i < 7; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                grid.RowDefinitions.Add(row);
            }

            // 버튼에 보여질 텍스트
            string[] strButtons = { "0", "D", "E", "F", "+", "&", "A", "B", "C", "-", "|",
                "7", "8", "9", "*", "^", "4", "5", "6", "/", "<<", "1","2","3","%",">>","0","Back", "Equals",};

            int iRow = 0, iCol = 0;

            // 버튼 생성
            foreach (string str in strButtons)
            {
                RoundedButton btn = new RoundedButton();
                btn.Focusable = false;
                btn.Height = 32;
                btn.Margin = new Thickness(4);
                btn.Click += ButtonOnClick;

                // TextBlock을 생성하고 RoundedButton의 Child로 설정
                TextBlock txt = new TextBlock();
                txt.Text = str;
                btn.Child = txt;

                // RoundButton을 Grid에 추가
                grid.Children.Add(btn);
                Grid.SetRow(btn, iRow);
                Grid.SetColumn(btn, iCol);

                // 버튼 출력 때 예외 상항 처리
                if (iRow == 0 && iCol == 0)
                {
                    btnDisplay = btn;
                    btnDisplay.Margin = new Thickness(4, 4, 4, 6);
                    Grid.SetColumnSpan(btn, 5);
                    iRow = 1;
                }

                // Back과 EEquals 버튼 처리
                else if (iRow == 6 && iCol > 0)
                {
                    Grid.SetColumnSpan(btn, 2);
                    iCol += 2;
                }
                else 
                {
                    btn.Width = 32;
                    if (0 == (iCol = (iCol + 1) % 5))
                        iRow++;
                }
            }
        }

        // Click 이벤트 핸들러
        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            // 클릭된 버튼 구하기
            RoundedButton btn = e.Source as RoundedButton;

            if(btn == null)
                return;

            // 버튼 텍스트와 첫 글자 구하기
            string strButton = (btn.Child as TextBlock).Text;
            char chButton = strButton[0];

            // 몇 가지 특이한 경우
            if(strButton == "Equals")
                chButton = '=';

            if (btn == btnDisplay)
                numDisplay = 0;

            else if (strButton == "Back")
                numDisplay /= 16;

            // 16진수 글자
            else if (Char.IsLetterOrDigit(chButton))
            {
                if (bNewNumber)
                {
                    numFirst = numDisplay;
                    numDisplay = 0;
                    bNewNumber = false;
                }
                if(numDisplay <= ulong.MaxValue >> 4)
                    numDisplay = 16 * numDisplay + (ulong)(chButton - (Char.IsDigit(chButton) ? '0' : 'A' - 10));
            }

            // 연산
            else
            {
                if (!bNewNumber)
                {
                    switch (chOperation)
                    {
                        case '=': break;
                        case '+': numDisplay = numFirst + numDisplay; break;
                        case '-': numDisplay = numFirst - numDisplay; break;
                        case '*': numDisplay = numFirst * numDisplay; break;
                        case '&': numDisplay = numFirst & numDisplay; break;
                        case '|': numDisplay = numFirst | numDisplay; break;
                        case '^': numDisplay = numFirst ^ numDisplay; break;
                        case '<': numDisplay = numFirst << (int)numDisplay; break;
                        case '>': numDisplay = numFirst >> (int)numDisplay; break;
                        case '/': numDisplay = numDisplay != 0 ? numFirst / numDisplay : ulong.MaxValue; break;
                        case '%': numDisplay = numDisplay != 0 ? numFirst % numDisplay : ulong.MaxValue; break;
                        default: numDisplay = 0; break;

                    }
                }

                bNewNumber = true;
                chOperation = chButton;
            }

            // 출력 형식 지정
            TextBlock text = new TextBlock();
            text.Text = String.Format("{0:X}", numDisplay);
            btnDisplay.Child = text;
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            if (e.Text.Length == 0)
                return;

            // 문자 입력 구하기
            char chkey = Char.ToUpper(e.Text[0]);

            // 버튼을 통한 루프
            foreach(UIElement child in (Content as Grid).Children)
            {
                RoundedButton btn = child as RoundedButton;
                string strButton = (btn.Child as TextBlock).Text;

                // 일치하는 버튼을 확인하기 위한 로직
            }
        }
    }
}
