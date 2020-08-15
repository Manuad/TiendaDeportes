using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using TiendaDeportes.Datos;
using TiendaDeportes.Tablas;
using System.IO;
using System.Collections.ObjectModel;

namespace TiendaDeportes.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_readPedidos : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<T_Pedidos> TablaPedidos;
        public V_readPedidos()
        {
            InitializeComponent();
            con = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaPedidos.ItemSelected += ListaPedidos_ItemSelected;
        }

        private void ListaPedidos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Pedidos)e.SelectedItem;
            var item = Obj.ID.ToString();
            var fechaPedido = Obj.FechaPedido.ToString();
            var direccion = Obj.DireccionPedido;
            var fechaEnvio = Obj.FechaEnvio.ToString();
            var medio = Obj.MedioEntrega;
            var fechaEntrega = Obj.FechaEntrega.ToString();
            var total = Obj.TotalPago.ToString();
            int Id = Convert.ToInt32(item);
            float toutal = float.Parse(total);
            try
            {
                Navigation.PushAsync(new V_DetallePedido(Id, fechaPedido, direccion, fechaEnvio, medio, fechaEntrega, toutal));
            }

            catch (Exception)
            {
                throw;
            }
        }

        protected async override void OnAppearing()
        {
            var ResultadoRegistro = await con.Table<T_Pedidos>().ToListAsync();
            TablaPedidos = new ObservableCollection<T_Pedidos>(ResultadoRegistro);
            ListaPedidos.ItemsSource = TablaPedidos;
            base.OnAppearing();
        }
    }
}