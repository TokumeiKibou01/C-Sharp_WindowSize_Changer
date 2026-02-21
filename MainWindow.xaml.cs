using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace C_Sharp_WindowSize_Changer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
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

        public MainWindow() {
            InitializeComponent();
            DataContext = new ViewModel();
            ViewModel viewModel = (ViewModel) DataContext;

            foreach (var process in Process.GetProcesses()) {
                if (process.MainWindowHandle != IntPtr.Zero) { // ウインドウハンドルがあるもののみ
                    viewModel.ProcessList.Add(process.ProcessName);
                    if (GetWindowRect(process.MainWindowHandle, out RECT rect)) {
                        viewModel.WindowWidth = rect.Right - rect.Left;
                        break;
                    }
                }
            }
        }
    }
}