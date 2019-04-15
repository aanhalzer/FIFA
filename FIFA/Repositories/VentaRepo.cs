using FIFA.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIFA.Repositories {
    public class VentaRepo {

        public ObservableCollection<Venta> GetAllSalesForWeek(string week) {
            ObservableCollection<Venta> allSales = new ObservableCollection<Venta>();
            using (SqlConnection conn = new SqlConnection((App.Current as App).ConnectionString)) {
                using (SqlCommand cmd = new SqlCommand()) {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"SELECT Cliente, Cantidad FROM Venta WHERE Semana = '{week}'";
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        Venta venta = new Venta();
                        venta.Cliente = reader.GetString(0);
                        venta.Cantidad = reader.GetInt32(1);
                        allSales.Add(venta);
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            return allSales;
        }
    }
}
