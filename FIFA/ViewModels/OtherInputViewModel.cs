using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFA.ViewModels
{
    class OtherInputViewModel : MainViewModelBase
    {
        private int numeroLote_ = 0;
        public int NumeroLote {
            get { return numeroLote_; }
            set { numeroLote_ = value; OnPropertyChanged("NumeroLote"); }
        }

        private string granjaLote_ = "";
        public string GranjaLote {
            get { return granjaLote_; }
            set { granjaLote_ = value; OnPropertyChanged("GranjaLote"); }
        }

        private string incAbre_ = "";
        public string IncAbre {
            get { return incAbre_; }
            set { incAbre_ = value; OnPropertyChanged("IncAbre"); }
        }

        private string incNombre_ = "";
        public string IncNombre {
            get { return incNombre_; }
            set { incNombre_ = value; OnPropertyChanged("IncNombre"); }
        }

        private string clienteAbre_ = "";
        public string ClienteAbre {
            get { return clienteAbre_; }
            set { clienteAbre_ = value; OnPropertyChanged("ClienteAbre"); }
        }

        private string clienteNombre_ = "";
        public string ClienteNombre {
            get { return clienteNombre_; }
            set { clienteNombre_ = value; OnPropertyChanged("ClienteNombre"); }
        }

        private string granjaNombre_ = "";
        public string GranjaNombre {
            get { return granjaNombre_; }
            set { granjaNombre_ = value; OnPropertyChanged("GranjaNombre"); }
        }

    }
}
