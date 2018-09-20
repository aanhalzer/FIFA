using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFA.ViewModels
{
    class WeekInputViewModel : MainViewModelBase
    {
        private string semana_ = "00-0000";
        public string Semana {
            get { return semana_; }
            set { semana_ = value; OnPropertyChanged("Semana"); }
        }

        private int mortalidad_ = 6;
        public int Mortalidad {
            get { return mortalidad_; }
            set { mortalidad_ = value; OnPropertyChanged("Mortalidad"); }
        }

        private int liquidacion_ = 0;
        public int Liquidacion {
            get { return liquidacion_; }
            set { liquidacion_ = value; OnPropertyChanged("Liquidacion"); }
        }

    }
}
