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
        public DateTimeOffset Fecha { get; set; }
        public bool FechaPro { get; set; }

        public IngresoPorLote(int weekNumber) {
            this.Semana = weekNumber;
            this.Lote = 0;
            this.LotePro = true;
            this.Cantidad = 0;
            this.CantidadPro = true;
            this.Fecha = DateTime.Now;
            this.FechaPro = true;
        }

        public static ObservableCollection<IngresoPorLote> InitialList (int weekNumber) {
            return new ObservableCollection<IngresoPorLote> {
                new IngresoPorLote (weekNumber)
            };
        }
    }
}
