using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFA.Models {
    public class TestGlobal {

        // Example: "40-2018" for week #40 in year 2018
        public string SemanaNacimiento { get; set; }
        public int IncAnhalzer { get; set; }
        public int Incubesa { get; set; }
        public int GenNacional { get; set; }
        public int TotalIngreso { get; set; }
        // =TotalIngreso-(TotalIngreso*Mortalidad)
        public float Mortalidad { get; set; }
        public string SemanaKFC { get; set; }
        public float ProyectadoKFC { get; set; }
        public int EntregaKFC { get; set; }
        public float Precio { get; set; }
        public string SemanaVentaPie { get; set; }
        public int VentaPereira { get; set; }
        public int VentaGustapollo { get; set; }
        public int VentaGranja { get; set; }
        public int VentaFaina { get; set; }
        public int Liquidacion { get; set; }
        public int Saldo { get; set; }

        public static ObservableCollection<TestGlobal> TestGlobalList() {
            ObservableCollection<TestGlobal> toReturn = new ObservableCollection<TestGlobal> {
                new TestGlobal {}
            };
            return toReturn;
        }
    }
}
