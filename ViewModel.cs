using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace C_Sharp_WindowSize_Changer {
    class ViewModel : INotifyPropertyChanged {
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

        public ViewModel() {
            ButtonCommand = new ButtonCommand(this);
        }

    }
}
