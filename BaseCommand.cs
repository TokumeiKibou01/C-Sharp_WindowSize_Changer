using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace C_Sharp_WindowSize_Changer {
    class BaseCommand : ICommand {
        public event EventHandler? CanExecuteChanged;
        private readonly Action _action;

        public BaseCommand(Action action) { 
            _action = action;
        }

        public bool CanExecute(object? parameter) {
            return true;
        }

        public void Execute(object? parameter) {
            _action();
        }
    }
}
