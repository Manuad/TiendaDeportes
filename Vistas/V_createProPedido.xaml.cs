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


namespace TiendaDeportes.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_createProPedido : ContentPage
    {
        private SQLiteAsyncConnection conexion;

        public V_createProPedido()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var piezas = int.Parse(cantidad.Text);
            var precio = float.Parse(precioUnitario.Text);
            var subTotal = piezas * precio;
            var DatosResgistro = new T_PedidoProducto
            {
                IdPedido = int.Parse(idPedido.Text),
                IdProducto = int.Parse(idProducto.Text),
                Cantidad = int.Parse(cantidad.Text),
                PrecioUnitario = float.Parse(precioUnitario.Text),
                SubTotal = subTotal,
                Descuento = float.Parse(descuento.Text)
            };
            conexion.InsertAsync(DatosResgistro);
            limpiarFormulario();
            DisplayAlert("Info","Detalle Pedido almacenado","OK");
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            limpiarFormulario();
        }

        void limpiarFormulario()
        {
            idPedido.Text = "";
            idProducto.Text = "";
            cantidad.Text = "";
            precioUnitario.Text = "";
            descuento.Text = "";
        }

    }
}