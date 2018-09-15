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
        
        public string Semana { get; set; }
        public int Lote { get; set; }
        public int Cantidad { get; set; }
        public int Fecha { get; set; }

        public IngresoPorLote() {
            this.Semana = "";
            this.Lote = 0;
            this.Cantidad = 0;
            this.Fecha = 0;
        }

        public static ObservableCollection<IngresoPorLote> InitialList () {
            return new ObservableCollection<IngresoPorLote> {
                new IngresoPorLote ()
            };
        }
    }
}
