using FIFA.Models;
using FIFA.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFA.Services {
    public class GlobalService {

        public GlobalService() {
            globalRepo = new GlobalRepo();
            ventaRepo = new VentaRepo();
            ingresoPorLoteRepo = new IngresoPorLoteRepo();
        }

        private GlobalRepo globalRepo;
        private VentaRepo ventaRepo;
        private IngresoPorLoteRepo ingresoPorLoteRepo;

        public ObservableCollection<Global> GetAllWeeks() {
            ObservableCollection<Global> allWeeks = globalRepo.GetAllWeeks();
            foreach (Global week in allWeeks) {
                week.VentasPorCliente = ventaRepo.GetAllSalesForWeek(week.Semana);
                week.IngresoPorLote = ingresoPorLoteRepo.GetAllEntriesForWeek(week.Semana); 
                week.Descabece = new Dictionary<string, int>();
                week.SaleKFC = new Dictionary<string, int>();
                week.SalePie = new Dictionary<string, int>();
                week.IngresoPorIncubadoras = new Dictionary<string, int>();

                foreach (IngresoPorLote ingreso in week.IngresoPorLote) {
                    week.Saldo += ingreso.Saldo;

                    if (week.IngresoPorIncubadoras.ContainsKey(ingreso.Incubadora))
                        week.IngresoPorIncubadoras[ingreso.Incubadora] += ingreso.Cantidad;
                    else
                        week.IngresoPorIncubadoras.Add(ingreso.Incubadora, ingreso.Cantidad);

                    int[] dias = { ingreso.Dia, ingreso.Descabece, ingreso.SaleKFC, ingreso.SalePie };
                    string[] semanaSplit = week.Semana.Split('-');
                    int semana = Int32.Parse(semanaSplit[0]), year = Int32.Parse(semanaSplit[1]);
                    string[] salidas = SemanaSalidas(semana, year, dias);

                    if (week.Descabece.ContainsKey(salidas[0]))
                        week.Descabece[salidas[0]]++;
                    else
                        week.Descabece.Add(salidas[0], 1);

                    if (week.SaleKFC.ContainsKey(salidas[1]))
                        week.SaleKFC[salidas[1]]++;
                    else
                        week.SaleKFC.Add(salidas[1], 1);

                    if (week.SalePie.ContainsKey(salidas[2]))
                        week.SalePie[salidas[2]]++;
                    else
                        week.SalePie.Add(salidas[2], 1);
                }
            }
            return allWeeks;
        }

        private string[] SemanaSalidas(int week, int year, int[] dias) {
            int entra = dias[0], descabece = dias[1], saleKFC = dias[2], salePie = dias[3];
            Calendar c = new CultureInfo("en-US").Calendar;
            CalendarWeekRule cwr = CalendarWeekRule.FirstDay;
            DayOfWeek dw = DayOfWeek.Monday;
            DateTime enterDate = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Monday - enterDate.DayOfWeek;
            enterDate = c.AddDays(enterDate, daysOffset);
            if (enterDate.Year != year) enterDate = c.AddWeeks(enterDate, 1);
            int num = c.GetWeekOfYear(enterDate, cwr, dw);
            num = week - num;
            enterDate = c.AddWeeks(enterDate, num);
            enterDate = c.AddDays(enterDate, entra - 1);
            string partial = "-" + year.ToString();
            return new string[]
                { c.GetWeekOfYear(c.AddDays(enterDate, descabece), cwr, dw).ToString().PadLeft(2, '0') + partial,
                  c.GetWeekOfYear(c.AddDays(enterDate, saleKFC), cwr, dw).ToString().PadLeft(2, '0') + partial,
                  c.GetWeekOfYear(c.AddDays(enterDate, salePie), cwr, dw).ToString().PadLeft(2, '0') + partial};
        }
    }
}
