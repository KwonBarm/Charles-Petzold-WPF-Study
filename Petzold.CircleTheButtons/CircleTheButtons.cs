﻿using System.Windows;
using System.Windows.Controls;

namespace Petzold.CircleTheButtons
{
    internal class CircleTheButtons : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new CircleTheButtons());
        }

        public CircleTheButtons()
        {
            Title = "Circle the Buttons";

            RadialPanel pnl = new RadialPanel();
            pnl.Orientation = RadialPanelOrientation.ByWidth;
            pnl.ShowPieLines = true;
            Content = pnl;

            Random rand = new Random();

            for (int i = 0; i < 9; i++)
            {
                Button btn = new Button();
                btn.Content = "Button Number " + (i + 1);
                btn.FontSize += rand.Next(10);
                pnl.Children.Add(btn);
            }
        }
    }
}
