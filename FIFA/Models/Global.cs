using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFA.Models
{
    class Global
    {
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
    }
}
