using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFA.Models
{
    public class Global {

        public Global() {
            Saldo = 0;
            Venta = 0;
            Mortalidad = 0;
            Liquidacion = 0;
            Precio = 0;
            Ingreso = 0;
        }

        // From SQL Global table
        public string Semana { get; set; }
        public int Ingreso { get; set; }
        public bool IngresoEdit { get; set; }
        public int Mortalidad { get; set; }
        public bool MortalidadEdit { get; set; }
        public decimal Precio { get; set; }
        public bool PrecioEdit { get; set; }
        public int Venta { get; set; }
        public bool VentaEdit { get; set; }
        public int Liquidacion { get; set; }
        public bool LiquidacionEdit { get; set; }

        // From other tables
        public ObservableCollection<IngresoPorLote> IngresoPorLote { get; set; }
        public ObservableCollection<Venta> VentasPorCliente { get; set; }

        // Processed from other tables
        public Dictionary<string, int> IngresoPorIncubadoras { get; set; }
        public Dictionary<string, int> Descabece { get; set; }
        public Dictionary<string, int> SaleKFC { get; set; }
        public Dictionary<string, int> SalePie { get; set; }
        public int Saldo { get; set; }
    }
}
