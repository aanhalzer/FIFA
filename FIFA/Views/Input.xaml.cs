using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace FIFA.Views {
    public sealed partial class Input : Page {

        public ObservableCollection<IngresoPorLote> InitialList { get; set; }

        public Input() {
            this.InitializeComponent();
            InitialList = IngresoPorLote.InitialList(1);
            this.DataContext = InitialList;
        }

        private void AddNewRow_Click(object sender, RoutedEventArgs e) {
            InitialList.Add(new IngresoPorLote (1));
        }

        private void PushData_Click(object sender, RoutedEventArgs e) {
            string cnString = "lol u thought";
            string cmdText = "insert into IngresoPorLote values (@Semana,@Lote,@LotePro,@Cantidad,@CantidadPro,@Fecha,@FechaPro)";
            using (SqlConnection con = new SqlConnection(cnString))
            using (SqlCommand cmd = new SqlCommand(cmdText, con)) {
                con.Open();
                foreach (IngresoPorLote i in InitialList) {
                    cmd.Parameters.AddWithValue("@Semana", weekNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@Lote", i.Lote);
                    cmd.Parameters.AddWithValue("@LotePro", i.LotePro);
                    cmd.Parameters.AddWithValue("@Cantidad", i.Cantidad);
                    cmd.Parameters.AddWithValue("@CantidadPro", i.CantidadPro);
                    cmd.Parameters.AddWithValue("@Fecha", i.Fecha);
                    cmd.Parameters.AddWithValue("@FechaPro", i.FechaPro);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                con.Close();
            }
        }

        private void DeleteLastRow_Click(object sender, RoutedEventArgs e) {
            InitialList.RemoveAt(InitialList.Count - 1);
        }
    }
}
