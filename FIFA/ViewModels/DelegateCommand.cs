using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FIFA.ViewModels {
    public class DelegateCommand : ICommand {

        public event EventHandler CanExecuteChanged;
        private readonly Action _execute;

        public DelegateCommand(Action execute) {
            _execute = execute;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            _execute();
        }
    }
}
