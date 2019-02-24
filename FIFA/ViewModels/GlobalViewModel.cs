using FIFA.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FIFA.ViewModels
{
    class GlobalViewModel : MainViewModelBase
    {
        private ObservableCollection<Global> semanas_ = new ObservableCollection<Global>();
        public ObservableCollection<Global> Semanas {
            get { return semanas_; }
            set { semanas_ = value; OnPropertyChanged("Semanas"); }
        }

        private ICommand semanasUpdate_;
        public ICommand SemanasUpdate {
            get { return semanasUpdate_ = semanasUpdate_ ?? new DelegateCommand(SemanasUpdateExecute); }
        }
        private void SemanasUpdateExecute() {
            Semanas = new ObservableCollection<Global>();
            using (SqlConnection conn = new SqlConnection((App.Current as App).ConnectionString)) {
                using (SqlCommand cmd = new SqlCommand()) {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT * FROM Global";
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        Global obj = new Global();

                        // Getting from Global table
                        obj.Semana = reader.GetString(0);
                        obj.Ingreso = reader.GetInt32(1);
                        obj.IngresoEdit = reader.GetBoolean(2);
                        obj.Mortalidad = reader.GetByte(3);
                        obj.MortalidadEdit = reader.GetBoolean(4);
                        obj.Precio = (decimal) reader.GetSqlMoney(5);
                        obj.PrecioEdit = reader.GetBoolean(6);
                        obj.Venta = reader.GetInt32(7);
                        obj.VentaEdit = reader.GetBoolean(8);
                        obj.Liquidacion = reader.GetInt32(9);
                        obj.LiquidacionEdit = reader.GetBoolean(10);

                        // Getting from other tables
                        obj.VentasPorCliente = new ObservableCollection<Venta>();
                        obj.Descabece = new Dictionary<string, int>();
                        obj.SaleKFC = new Dictionary<string, int>();
                        obj.SalePie = new Dictionary<string, int>();
                        obj.IngresoPorIncubadoras = new Dictionary<string, int>();
                        using (SqlCommand cmd2 = new SqlCommand()) {
                            cmd2.Connection = conn;
                            cmd2.CommandType = CommandType.Text;
                            cmd2.CommandText = String.Format(
                                @"SELECT Cliente, Cantidad FROM Venta WHERE Semana = '{0}'", obj.Semana);
                            SqlDataReader reader2 = cmd2.ExecuteReader();
                            while (reader2.Read()) {
                                Venta venta = new Venta();
                                venta.Cliente = reader2.GetString(0);
                                venta.Cantidad = reader2.GetInt32(1);
                                obj.VentasPorCliente.Add(venta);
                            }
                            reader2.Close();
                            
                            string[] semanaSplit = obj.Semana.Split('-');
                            int semana = Int32.Parse(semanaSplit[0]), year = Int32.Parse(semanaSplit[1]);
                            cmd2.CommandText = String.Format(
                                @"SELECT Incubadora, Cantidad, Saldo, Dia, Descabece, SaleKFC, SalePie 
                                FROM IngresoPorLote WHERE Semana = '{0}'"
                                , obj.Semana);
                            reader2 = cmd2.ExecuteReader();
                            while (reader2.Read()) {

                                // Cantidades por Incubadora
                                string incubadora = reader2.GetString(0);
                                int cantidad = reader2.GetInt32(1);
                                if (obj.IngresoPorIncubadoras.ContainsKey(incubadora))
                                    obj.IngresoPorIncubadoras[incubadora] += cantidad;
                                else
                                    obj.IngresoPorIncubadoras.Add(incubadora, cantidad);

                                obj.Saldo += reader2.GetInt32(2);

                                // Salida de pollitos
                                int[] dias = {reader2.GetInt32(3),
                                              reader2.GetInt32(4),
                                              reader2.GetInt32(5),
                                              reader2.GetInt32(6)};
                                string[] salidas = SemanaSalidas(semana, year, dias);
                                if (obj.Descabece.ContainsKey(salidas[0]))
                                    obj.Descabece[salidas[0]]++;
                                else
                                    obj.Descabece.Add(salidas[0], 1);
                                if (obj.SaleKFC.ContainsKey(salidas[1]))
                                    obj.SaleKFC[salidas[1]]++;
                                else
                                    obj.SaleKFC.Add(salidas[1], 1);
                                if (obj.SalePie.ContainsKey(salidas[2]))
                                    obj.SalePie[salidas[2]]++;
                                else
                                    obj.SalePie.Add(salidas[2], 1);
                            }
                            reader2.Close();
                        }
                        Semanas.Add(obj);
                    }
                    reader.Close();
                    conn.Close();
                }
            }
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
