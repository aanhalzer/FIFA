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
    public class IngresoPorLoteRepo {

        public ObservableCollection<IngresoPorLote> GetAllEntriesForWeek(string week) {
            ObservableCollection<IngresoPorLote> allEntries = new ObservableCollection<IngresoPorLote>();
            using (SqlConnection conn = new SqlConnection((App.Current as App).ConnectionString)) {
                using (SqlCommand cmd = new SqlCommand()) {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $@"SELECT Incubadora, Cantidad, Saldo, Dia, Descabece, SaleKFC, SalePie 
                                FROM IngresoPorLote WHERE Semana = '{week}'";
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        IngresoPorLote ingreso = new IngresoPorLote();
                        ingreso.Incubadora = reader.GetString(0);
                        ingreso.Cantidad = reader.GetInt32(1);
                        ingreso.Saldo = reader.GetInt32(2);
                        ingreso.Dia = reader.GetInt32(3);
                        ingreso.Descabece = reader.GetInt32(4);
                        ingreso.SaleKFC = reader.GetInt32(5);
                        ingreso.SalePie = reader.GetInt32(6);
                        allEntries.Add(ingreso);
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            return allEntries;
        }
    }
}
