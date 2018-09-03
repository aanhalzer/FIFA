using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FIFA {
    public class TestItem {

        public DateTimeOffset Fecha { get; set; }
        public int Lote { get; set; }
        public int Cantidad { get; set; }

        public TestItem() {
            this.Fecha = DateTime.Now;
            this.Lote = 0;
            this.Cantidad = 0;
        }

        public static ObservableCollection<TestItem> TestList () {
            return new ObservableCollection<TestItem> {
                new TestItem ()
            };
        }
    }
}
