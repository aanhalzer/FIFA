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
    public class GlobalRepo {
        
        public ObservableCollection<Global> GetAllWeeks() {
            ObservableCollection<Global> allWeeks = new ObservableCollection<Global>();
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
                        obj.Precio = (decimal)reader.GetSqlMoney(5);
                        obj.PrecioEdit = reader.GetBoolean(6);
                        obj.Venta = reader.GetInt32(7);
                        obj.VentaEdit = reader.GetBoolean(8);
                        obj.Liquidacion = reader.GetInt32(9);
                        obj.LiquidacionEdit = reader.GetBoolean(10);
                        allWeeks.Add(obj);
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            return allWeeks;
        }
    }
}
