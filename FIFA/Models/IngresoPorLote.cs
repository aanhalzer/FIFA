using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FIFA {
    public class IngresoPorLote {

        public string id { get; set; }
        public int Semana { get; set; }
        public int Lote { get; set; }
        public bool LotePro { get; set; }
        public int Cantidad { get; set; }
        public bool CantidadPro { get; set; }
        public int Fecha { get; set; }
        public bool FechaPro { get; set; }

        public IngresoPorLote() {
            this.Semana = 0;
            this.Lote = 0;
            this.LotePro = true;
            this.Cantidad = 0;
            this.CantidadPro = true;
            this.Fecha = 0;
            this.FechaPro = true;
        }

        public static ObservableCollection<IngresoPorLote> InitialList () {
            return new ObservableCollection<IngresoPorLote> {
                new IngresoPorLote ()
            };
        }
    }
}
