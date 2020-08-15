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
using System.Security.Cryptography;

namespace TiendaDeportes.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_readPedidoProducto : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<T_PedidoProducto> TablaPedidoProducto;
        public V_readPedidoProducto()
        {
            InitializeComponent();
            con = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaProduPedido.ItemSelected += ListaProduPedido_ItemSelected;
        }

        private void ListaProduPedido_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_PedidoProducto)e.SelectedItem;
            var item = Obj.IdPedido.ToString();
            var pedido = Obj.IdPedido.ToString();
            var producto = Obj.IdProducto.ToString();
            var cant = Obj.Cantidad.ToString();
            var precio = Obj.PrecioUnitario.ToString();
            var sub = Obj.SubTotal.ToString();
            var desc = Obj.Descuento.ToString();
            int Id = Convert.ToInt32(item);
            int idProducto = Convert.ToInt32(producto);
            int cantidad = Convert.ToInt32(cant);
            float precioUnitario = float.Parse(precio);
            float subtotal = float.Parse(sub);
            float descuento = float.Parse(desc);

            try
            {
                Navigation.PushAsync(new V_DetallePedidoProducto(Id, idProducto, cantidad, precioUnitario, subtotal, descuento));
            }
            catch
            {
                throw;
            }
        }

        protected async override void OnAppearing()
        {
            var ResultadoRegistro = await con.Table<T_PedidoProducto>().ToArrayAsync();
            TablaPedidoProducto = new ObservableCollection<T_PedidoProducto>(ResultadoRegistro);
            ListaProduPedido.ItemsSource = TablaPedidoProducto;
            base.OnAppearing();
        }
    }
}