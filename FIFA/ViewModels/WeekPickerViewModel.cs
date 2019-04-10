using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FIFA.ViewModels
{
    public class WeekPickerViewModel : MainViewModelBase
    {
        public void PageLoad() => OnLoad();
        private void OnLoad() 
        {

        }

        private string nuevaSemana_ = string.Empty;

        private string semanaCrearText_ = string.Empty;
        public string SemanaCrearText {
            get { return semanaCrearText_; }
            set { semanaCrearText_ = value; OnPropertyChanged("SemanaCrearText"); }
        }

        private string editarSemana_ = "00-0000";
        public string EditarSemana {
            get { return editarSemana_; }
            set { editarSemana_ = value; OnPropertyChanged("EditarSemana"); }
        }
    }
}
