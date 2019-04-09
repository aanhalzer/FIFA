﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFA.Models
{
    public class IngresoPorLote
    {
        public string Semana { get; set; }
        public int Lote { get; set; }
        public int Cantidad { get; set; }
        public int Dia { get; set; }
        public int Descabece { get; set; }
        public int SaleKFC { get; set; }
        public int SalePie { get; set; }
        public string Incubadora { get; set; }
        public int Saldo { get; set; }

        public IngresoPorLote() {
            Descabece = 32;
            SaleKFC = 35;
            SalePie = 50;
            Incubadora = "N/A";
        }
    }
}
