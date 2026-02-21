using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace C_Sharp_WindowSize_Changer {
    class ViewModel : INotifyPropertyChanged {
        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr hwnd, int x, int y, int width, int height, bool bRepaint);
        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect); //ウインドウの大きさを取得する関数

        /// <summary>
        /// windef.h にある構造体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT {
            public int Left; //ウインドウの左上X
            public int Top; //ウインドウの左上Y
            public int Right; //ウインドウの右下X
            public int Bottom; //ウインドウの右下Y
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public ButtonCommand ButtonCommand { get; private set; }

        private int _windowWidth;
        public int WindowWidth {
            get { return _windowWidth; }
            set {
                _windowWidth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WindowWidth)));
            }
        }

        private List<string> _processList;
        public List<string> ProcessList {
            get { return _processList; }
            set {
                _processList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProcessList)));
            }
        }

        private string _selectedItem;
        public string SelectedItem {
            get { return _selectedItem; }
            set {
                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
                var process = Process.GetProcessesByName(_selectedItem)[0];
                IntPtr hwnd = process.MainWindowHandle;
                if (GetWindowRect(hwnd, out RECT rect)) {
                    WindowWidth = rect.Right - rect.Left;  //ウインドウ幅を表示する
                }
            }
        }

        public ViewModel() {
            ButtonCommand = new ButtonCommand(this);
            ProcessList = new List<string>();
        }

    }
}
