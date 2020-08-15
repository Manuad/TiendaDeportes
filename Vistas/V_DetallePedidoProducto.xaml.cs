using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TiendaDeportes.Datos;
using TiendaDeportes.Tablas;
using SQLite;
using System.IO;

namespace TiendaDeportes.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_DetallePedidoProducto : ContentPage
    {
        public int IdPedido, IdProducto, Cantidad;
        public float PrecioUnitario, Subtotal, Descuento;
        private SQLiteAsyncConnection con;
        IEnumerable<T_PedidoProducto> ResultadoDelete;
        IEnumerable<T_PedidoProducto> ResultadoUpdate;
        public V_DetallePedidoProducto(int idPedido, int idProducto, int cantidad, float precioUnitario, float subtotal, float descuento)
        {
            InitializeComponent();
            con = DependencyService.Get<ISQLiteDB>().GetConnection();
            IdPedido = idPedido;
            IdProducto = idProducto;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Subtotal = subtotal;
            Descuento = descuento;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            idPedido.Text = "ID pedido " + IdPedido;
            idProducto.Text = Convert.ToString(IdProducto);
            cantidad.Text = Convert.ToString(Cantidad);
            precioUnitario.Text = Convert.ToString(PrecioUnitario);
            subtotal.Text = Convert.ToString(Subtotal);
            descuento.Text = Convert.ToString(Descuento);

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TiendaDeportiva.db3");
            var db = new SQLiteConnection(rutaDB);
            ResultadoUpdate = Update(db, idPedido.Text, idProducto.Text, cantidad.Text, precioUnitario.Text, descuento.Text, Convert.ToString(IdPedido));
            DisplayAlert("Confirmar", "Los datos del pedido " + IdPedido + " serán actualizados", "Aceptar");

        }
        public static IEnumerable<T_PedidoProducto> Update(SQLiteConnection db, string idPedido, string idProducto, string cantidad, string precioUnitario, string subtotal, string descuento)
        {
            float precio, sub, descount;
            int pedido, producto, cant;
            precio = float.Parse(precioUnitario);
            sub = float.Parse(subtotal);
            descount = float.Parse(descuento);
            cant = int.Parse(cantidad);

            return db.Query<T_PedidoProducto>("UPDATE T_PedidoProducto SET Cantidad =?, PrecioUnitario =?, Subtotal=?, Descuento=? WHERE IdPedido=? AND IdProducto=?", cant, precio, sub, descount);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TeindaDeportiva.db3");
            var db = new SQLiteConnection(rutaDB);
            ResultadoDelete = Delete(db, IdPedido);
            DisplayAlert("Confirmar", "El pedido se eliminó satisfactoriamente", "Ok");
            limpiarFormulario();
        }

        public static IEnumerable<T_PedidoProducto> Delete(SQLiteConnection db, int idPedido)
        {
            return db.Query<T_PedidoProducto>("DELETE FROM T_PedidoProducto WHERE IdPedido=?", idPedido);
        }
        public void limpiarFormulario()
        {
            idPedido.Text = "";
            idProducto.Text = "";
            cantidad.Text = "";
            precioUnitario.Text = "";
            subtotal.Text = "";
            descuento.Text = "";
        }
    }
}
