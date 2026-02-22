using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Input;
using static C_Sharp_WindowSize_Changer.ViewModel;

namespace C_Sharp_WindowSize_Changer {
    class ButtonCommand {

        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr hwnd, int x, int y, int width, int height, bool bRepaint);
        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect); //ウインドウの大きさを取得する関数

        private readonly ViewModel mainViewModel_;

        public ICommand WidthPlusCommand { get; private set; }
        public ICommand WidthMinusCommand { get; private set; }
        public ICommand HeightPlusCommand { get; private set; }
        public ICommand HeightMinusCommand { get; private set; }
        public ICommand ApplyCommand { get; private set; }

        public ButtonCommand(ViewModel mainViewModel) {
            mainViewModel_ = mainViewModel;
            WidthPlusCommand = new BaseCommand(RunWidthPlusCommand);
            WidthMinusCommand = new BaseCommand(RunWidthMinusCommand);
            HeightPlusCommand = new BaseCommand(RunHeightPlusCommand);
            HeightMinusCommand = new BaseCommand(RunHeightMinusCommand);
            ApplyCommand = new BaseCommand(RunApplyCommand);
        }

        private void RunWidthPlusCommand() {
            mainViewModel_.WindowWidth += 10;
        }

        private void RunWidthMinusCommand() {
            mainViewModel_.WindowWidth -= 10;
        }

        private void RunHeightPlusCommand() {
            mainViewModel_.WindowHeight += 10;
        }

        private void RunHeightMinusCommand() {
            mainViewModel_.WindowHeight -= 10;
        }

        private void RunApplyCommand() {
            var process = Process.GetProcessesByName(mainViewModel_.SelectedItem)[0];
            var hwnd = process.MainWindowHandle;
            if (GetWindowRect(hwnd, out RECT rect)) {
                MoveWindow(hwnd, rect.Left, rect.Top, mainViewModel_.WindowWidth, mainViewModel_.WindowHeight, true);
            }
            
        }
    }
}
