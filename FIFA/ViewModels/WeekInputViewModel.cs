using FIFA.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private decimal precio_ = 0;
        public decimal Precio {
            get { return precio_; }
            set { precio_ = value; OnPropertyChanged("Precio"); }
        }

        private ObservableCollection<IngresoPorLote> lotes_ = new ObservableCollection<IngresoPorLote>();
        public ObservableCollection<IngresoPorLote> Lotes {
            get { return lotes_; }
            set { lotes_ = value; OnPropertyChanged("Lotes"); }
        }

        private ObservableCollection<IngresoPorIncubadora> incubadoras_ = new ObservableCollection<IngresoPorIncubadora>();
        public ObservableCollection<IngresoPorIncubadora> Incubadoras {
            get { return incubadoras_ ; }
            set { incubadoras_  = value; OnPropertyChanged("Incubadoras"); }
        }

        private ObservableCollection<Venta> ventas_ = new ObservableCollection<Venta>();
        public ObservableCollection<Venta> Ventas {
            get { return ventas_; }
            set { ventas_ = value; OnPropertyChanged("Venta"); }
        }

        private ICommand lotesAdd_;
        public ICommand LotesAdd {
            get { return lotesAdd_ = lotesAdd_ ?? new DelegateCommand(LotesAddExecute); }
        }
        private void LotesAddExecute() {
            Lotes.Add(new IngresoPorLote());
        }

        private ICommand lotesRemove_;
        public ICommand LotesRemove {
            get { return lotesRemove_ = lotesRemove_ ?? new DelegateCommand(LotesRemoveExecute); }
        }
        private void LotesRemoveExecute() {
            Lotes.RemoveAt(Lotes.Count - 1);
        }

        private ICommand incubadorasAdd_;
        public ICommand IncubadorasAdd {
            get { return incubadorasAdd_ = incubadorasAdd_ ?? new DelegateCommand(IncubadorasAddExecute); }
        }
        private void IncubadorasAddExecute() {
            Incubadoras.Add(new IngresoPorIncubadora());
        }

        private ICommand incubadorasRemove_;
        public ICommand IncubadorasRemove {
            get { return incubadorasRemove_ = incubadorasRemove_ ?? new DelegateCommand(IncubadorasRemoveExecute); }
        }
        private void IncubadorasRemoveExecute() {
            Incubadoras.RemoveAt(Incubadoras.Count - 1);
        }

        private ICommand ventasAdd_;
        public ICommand VentasAdd {
            get { return ventasAdd_ = ventasAdd_ ?? new DelegateCommand(VentasAddExecute); }
        }
        private void VentasAddExecute() {
            Ventas.Add(new Venta());
        }

        private ICommand ventasRemove_;
        public ICommand VentasRemove {
            get { return ventasRemove_ = ventasRemove_ ?? new DelegateCommand(VentasRemoveExecute); }
        }
        private void VentasRemoveExecute() {
            Ventas.RemoveAt(Ventas.Count - 1);
        }
    }
}
