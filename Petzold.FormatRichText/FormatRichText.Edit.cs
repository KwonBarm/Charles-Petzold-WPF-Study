using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Petzold.FormatRichText
{
    internal partial class FormatRichText : Window
    {
        void AddEditToolbar(ToolBarTray tray, int band, int index)
        {
            // ToolBar 생성
            ToolBar toolBar = new ToolBar();
            toolBar.Band = band;
            toolBar.BandIndex = index;
            tray.ToolBars.Add(toolBar);

            RoutedUICommand[] comm =
            {
                ApplicationCommands.Cut,
                ApplicationCommands.Copy,
                ApplicationCommands.Paste,
                ApplicationCommands.Delete,
                ApplicationCommands.Undo,
                ApplicationCommands.Redo,
            };

            string[] strImages =
            {
                "CutHS.png",
                "CopyHS.png",
                "PasteHS.png",
                "DeleteHS.png",
                "UndoHS.png",
                "RedoHS.png",
            };

            for(int i=0; i < 6; i++)
            {
                if (i == 4)
                    toolBar.Items.Add(new Separator());

                Button btn = new Button();
                btn.Command = comm[i];
                toolBar.Items.Add(btn);

                Image img = new Image();
                img.Source = new BitmapImage(new Uri("pack://application:,,/Images/" + strImages[i]));
                img.Width = 32;
                img.Height = 32;
                btn.Content = img;

                ToolTip tip = new ToolTip();
                tip.Content = comm[i].Text;
                btn.ToolTip = tip;
            }

            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, OnDelete, CanDelete));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Redo));
        }

        private void CanDelete(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !txtbox.Selection.IsEmpty;
        }

        private void OnDelete(object sender, ExecutedRoutedEventArgs e)
        {
            txtbox.Selection.Text = "";
        }
    }
}
