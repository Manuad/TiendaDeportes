using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using System.IO;
using TiendaDeportes.Tablas;
using TiendaDeportes.Datos;
using SQLiteNetExtensions;

namespace TiendaDeportes.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_prinProdPedido : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public V_prinProdPedido()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var id_pedido = int.Parse(idPedido.Text);
                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TiendaDeportiva.db3");
                var db = new SQLiteConnection(rutaDB);db.CreateTable<T_PedidoProducto>();
                IEnumerable<T_PedidoProducto> resultado = SELECT_WHERE(db, id_pedido);
                if (resultado.Count() > 0)
                {
                    Navigation.PushAsync(new V_readPedidoProducto());
                }
                else
                {
                    DisplayAlert("Info", "No se han encontrado detalles con ese id", "Ok");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<T_PedidoProducto> SELECT_WHERE(SQLiteConnection db, int id_pedido)
        {

            return db.Query<T_PedidoProducto>("SELECT * FROM T_PedidoProducto WHERE IdPedido =?", id_pedido);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Vistas.V_createProPedido());
        }
    }
}