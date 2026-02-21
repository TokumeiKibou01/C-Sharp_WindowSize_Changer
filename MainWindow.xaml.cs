using System.Diagnostics;
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
        public MainWindow() {
            InitializeComponent();
            DataContext = new ViewModel();
            ViewModel viewModel = (ViewModel) DataContext;

            foreach (var process in Process.GetProcesses()) {
                if (process.MainWindowHandle != IntPtr.Zero) { // ウインドウハンドルがあるもののみ
                    viewModel.ProcessList.Add(process.ProcessName);
                }
            }
        }
    }
}