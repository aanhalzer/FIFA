using FIFA.Models;
using FIFA.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FIFA.ViewModels
{
    public class GlobalViewModel : MainViewModelBase
    {
        public GlobalViewModel() {
            globalService = new GlobalService();
        }

        private GlobalService globalService; 

        private ObservableCollection<Global> semanas_ = new ObservableCollection<Global>();
        public ObservableCollection<Global> Semanas {
            get { return semanas_; }
            set { semanas_ = value; OnPropertyChanged("Semanas"); }
        }

        private ICommand semanasUpdate_;
        public ICommand SemanasUpdate {
            get { return semanasUpdate_ = semanasUpdate_ ?? new DelegateCommand(SemanasUpdateExecute); }
        }
        private void SemanasUpdateExecute() {
            Semanas = globalService.GetAllWeeks();
        }
    }
}
