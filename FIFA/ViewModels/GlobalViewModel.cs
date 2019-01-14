using FIFA.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
            using (SqlConnection conn = new SqlConnection((App.Current as App).ConnectionString)) {
                using (SqlCommand cmd = new SqlCommand()) {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT * FROM Global";
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        Global obj = new Global();
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
                        obj.SemanaPie = "TEST";
                        obj.SemanaKFC = "TEST";
                        obj.EntregaKFC = 10;
                        obj.IngresoPorIncubadoras = new ObservableCollection<IngresoPorIncubadora>();
                        obj.VentasPorCliente = new ObservableCollection<Venta>();
                        using (SqlCommand cmd2 = new SqlCommand()) {
                            cmd2.Connection = conn;
                            cmd2.CommandType = CommandType.Text;
                            cmd2.CommandText = String.Format(@"SELECT Incubadora, Cantidad FROM IngresoPorIncubadora WHERE Semana = '{0}'",obj.Semana);
                            SqlDataReader reader2 = cmd2.ExecuteReader();
                            while (reader2.Read()) {
                                IngresoPorIncubadora ingreso = new IngresoPorIncubadora();
                                ingreso.Incubadora = reader2.GetString(0);
                                ingreso.Cantidad = reader2.GetInt32(1);
                                obj.IngresoPorIncubadoras.Add(ingreso);
                            }
                            reader2.Close();
                            cmd2.CommandText = String.Format(@"SELECT Cliente, Cantidad FROM Venta WHERE Semana = '{0}'", obj.Semana);
                            reader2 = cmd2.ExecuteReader();
                            while (reader2.Read()) {
                                Venta venta = new Venta();
                                venta.Cliente = reader2.GetString(0);
                                venta.Cantidad = reader2.GetInt32(1);
                                obj.VentasPorCliente.Add(venta);
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
    }
}
