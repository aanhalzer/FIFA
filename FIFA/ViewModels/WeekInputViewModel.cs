using FIFA.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace FIFA.ViewModels
{
    class WeekInputViewModel : MainViewModelBase
    {
        private string semana_ = "00-0000";
        public string Semana {
            get { return semana_; }
            set { semana_ = value; OnPropertyChanged("Semana"); }
        }

        private int mortalidad_ = 6;
        public int Mortalidad {
            get { return mortalidad_; }
            set { mortalidad_ = value; OnPropertyChanged("Mortalidad"); }
        }

        private int liquidacion_ = 0;
        public int Liquidacion {
            get { return liquidacion_; }
            set { liquidacion_ = value; OnPropertyChanged("Liquidacion"); }
        }

        private decimal precio_ = 0;
        public decimal Precio {
            get { return precio_; }
            set { precio_ = value; OnPropertyChanged("Precio"); }
        }

        private ObservableCollection<IngresoPorLote> lotes_ = 
            new ObservableCollection<IngresoPorLote>();
        public ObservableCollection<IngresoPorLote> Lotes {
            get { return lotes_; }
            set { lotes_ = value; OnPropertyChanged("Lotes"); }
        }

        private ObservableCollection<IngresoPorIncubadora> incubadoras_ = 
            new ObservableCollection<IngresoPorIncubadora>();
        public ObservableCollection<IngresoPorIncubadora> Incubadoras {
            get { return incubadoras_ ; }
            set { incubadoras_  = value; OnPropertyChanged("Incubadoras"); }
        }

        private ObservableCollection<Venta> ventas_ = 
            new ObservableCollection<Venta>();
        public ObservableCollection<Venta> Ventas {
            get { return ventas_; }
            set { ventas_ = value; OnPropertyChanged("Venta"); }
        }

        private ICommand lotesAdd_;
        public ICommand LotesAdd {
            get { return lotesAdd_ = lotesAdd_ ?? new DelegateCommand(LotesAddExecute); }
        }
        private void LotesAddExecute() {
            Lotes.Add(new IngresoPorLote());
        }

        private ICommand lotesRemove_;
        public ICommand LotesRemove {
            get { return lotesRemove_ = lotesRemove_ ?? new DelegateCommand(LotesRemoveExecute); }
        }
        private void LotesRemoveExecute() {
            Lotes.RemoveAt(Lotes.Count - 1);
        }

        private ICommand incubadorasAdd_;
        public ICommand IncubadorasAdd {
            get { return incubadorasAdd_ = incubadorasAdd_ ?? new DelegateCommand(IncubadorasAddExecute); }
        }
        private void IncubadorasAddExecute() {
            Incubadoras.Add(new IngresoPorIncubadora());
        }

        private ICommand incubadorasRemove_;
        public ICommand IncubadorasRemove {
            get { return incubadorasRemove_ = incubadorasRemove_ ?? new DelegateCommand(IncubadorasRemoveExecute); }
        }
        private void IncubadorasRemoveExecute() {
            Incubadoras.RemoveAt(Incubadoras.Count - 1);
        }

        private ICommand ventasAdd_;
        public ICommand VentasAdd {
            get { return ventasAdd_ = ventasAdd_ ?? new DelegateCommand(VentasAddExecute); }
        }
        private void VentasAddExecute() {
            Ventas.Add(new Venta());
        }

        private ICommand ventasRemove_;
        public ICommand VentasRemove {
            get { return ventasRemove_ = ventasRemove_ ?? new DelegateCommand(VentasRemoveExecute); }
        }
        private void VentasRemoveExecute() {
            Ventas.RemoveAt(Ventas.Count - 1);
        }

        private ICommand guardar_;
        public ICommand Guardar {
            get { return guardar_ = guardar_ ?? new DelegateCommand(GuardarExecuteAsync); }
        }
        private async void GuardarExecuteAsync() {
            using (SqlConnection conn = new SqlConnection((App.Current as App).ConnectionString)) {
                using (SqlCommand cmd = new SqlCommand()) {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    
                    // Ingreso por lote
                    cmd.CommandText = @"INSERT INTO IngresoPorLote VALUES (@Semana, @Lote, @Cantidad, @Dia)";
                    conn.Open();
                    foreach (IngresoPorLote i in Lotes) {
                        cmd.Parameters.AddWithValue("@Semana", Semana);
                        cmd.Parameters.AddWithValue("@Lote", i.Lote);
                        cmd.Parameters.AddWithValue("@Cantidad", i.Cantidad);
                        cmd.Parameters.AddWithValue("@Dia", i.Dia);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    // Ingreso por incubadora
                    cmd.CommandText = @"INSERT INTO IngresoPorIncubadora VALUES (@Semana, @Incubadora, @Cantidad)";
                    foreach (IngresoPorIncubadora i in Incubadoras) {
                        cmd.Parameters.AddWithValue("@Semana", Semana);
                        cmd.Parameters.AddWithValue("@Incubadora", i.Incubadora);
                        cmd.Parameters.AddWithValue("@Cantidad", i.Cantidad);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    // Ventas
                    cmd.CommandText = @"INSERT INTO Venta VALUES (@Semana, @Cliente, @Cantidad)";
                    foreach (Venta i in Ventas) {
                        cmd.Parameters.AddWithValue("@Semana", Semana);
                        cmd.Parameters.AddWithValue("@Cliente", i.Cliente);
                        cmd.Parameters.AddWithValue("@Cantidad", i.Cantidad);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }

                    // Otros
                    cmd.CommandText = @"INSERT INTO Global VALUES 
                                        (@Semana, @Ingreso, @IngresoEdit, @Mortalidad, @MortalidadEdit, 
                                        @Precio, @PrecioEdit, @Venta, @VentaEdit, @Liquidacion, @LiquidacionEdit)";
                    cmd.Parameters.AddWithValue("@Semana", Semana);
                    cmd.Parameters.AddWithValue("@Ingreso", Incubadoras.Sum(item => item.Cantidad));
                    cmd.Parameters.AddWithValue("@IngresoEdit", SqlDbType.Bit).Value = false;
                    cmd.Parameters.AddWithValue("@Mortalidad", Mortalidad);
                    cmd.Parameters.AddWithValue("@MortalidadEdit", SqlDbType.Bit).Value = false;
                    cmd.Parameters.AddWithValue("@Precio", Precio);
                    cmd.Parameters.AddWithValue("@PrecioEdit", SqlDbType.Bit).Value = false;
                    cmd.Parameters.AddWithValue("@Venta", Ventas.Sum(item => item.Cantidad));
                    cmd.Parameters.AddWithValue("@VentaEdit", SqlDbType.Bit).Value = false;
                    cmd.Parameters.AddWithValue("@Liquidacion", Liquidacion);
                    cmd.Parameters.AddWithValue("@LiquidacionEdit", SqlDbType.Bit).Value = false;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            await ShowContentDialogAsync("Crear Semana", "La semana " + Semana + " ha sido creada.");
            Semana = "00-0000";
            Incubadoras.Clear();
            Lotes.Clear();
            Ventas.Clear();
            Liquidacion = 0;
            Mortalidad = 6;
            Precio = 0;
        }

        private async Task ShowContentDialogAsync(string title, string content) {
            ContentDialog dialog = new ContentDialog() {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"
            };
            await dialog.ShowAsync();
        }
    }
}
