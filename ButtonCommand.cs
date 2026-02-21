using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace C_Sharp_WindowSize_Changer {
    class ButtonCommand {

        private readonly ViewModel mainViewModel_;

        public ICommand PlusCommand { get; private set; }
        public ICommand MinusCommand { get; private set; }

        public ButtonCommand(ViewModel mainViewModel) {
            mainViewModel_ = mainViewModel;
            PlusCommand = new BaseCommand(RunPlusCommand);
            MinusCommand = new BaseCommand(RunMinusCommand);
        }

        private void RunPlusCommand() {
            mainViewModel_.WindowWidth += 10;
        }

        private void RunMinusCommand() {
            mainViewModel_.WindowWidth -= 10;
        }
    }
}
