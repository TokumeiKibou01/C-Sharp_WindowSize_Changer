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

        public ICommand PlusCommand { get; private set; }
        public ICommand MinusCommand { get; private set; }
        public ICommand ApplyCommand { get; private set; }

        public ButtonCommand(ViewModel mainViewModel) {
            mainViewModel_ = mainViewModel;
            PlusCommand = new BaseCommand(RunPlusCommand);
            MinusCommand = new BaseCommand(RunMinusCommand);
            ApplyCommand = new BaseCommand(RunApplyCommand);
        }

        private void RunPlusCommand() {
            mainViewModel_.WindowWidth += 10;
        }

        private void RunMinusCommand() {
            mainViewModel_.WindowWidth -= 10;
        }

        private void RunApplyCommand() {
            var process = Process.GetProcessesByName(mainViewModel_.SelectedItem)[0];
            var hwnd = process.MainWindowHandle;
            if (GetWindowRect(hwnd, out RECT rect)) {
                MoveWindow(hwnd, rect.Left, rect.Top, mainViewModel_.WindowWidth, rect.Bottom - rect.Top, true);
            }
            
        }
    }
}
