using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFA.Models
{
    public class Venta
    {
        public Venta() {
            Cliente = "N/A";
        }
        
        public string Semana { get; set; }
        public string Cliente { get; set; }
        public int Cantidad { get; set; }
    }
}
