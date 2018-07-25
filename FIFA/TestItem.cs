using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FIFA
{
    public class TestItem {
        public DateTimeOffset Fecha { get; set; }
        public int Lote { get; set; }
        public int Cantidad { get; set; }

        public static ObservableCollection<TestItem> TestList () {
            ObservableCollection<TestItem> toReturn = new ObservableCollection<TestItem> {
                new TestItem {
                    Fecha = new DateTime(1977, 1, 5),
                    Lote = 1,
                    Cantidad = 1
                },
                new TestItem {
                    Fecha = new DateTime(2018, 1, 1),
                    Lote = 2,
                    Cantidad = 2
                }
            };
            return toReturn;
        }
    }
}
