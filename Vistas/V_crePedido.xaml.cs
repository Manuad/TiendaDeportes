using SQLite;
using System;
using TiendaDeportes.Datos;
using TiendaDeportes.Tablas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiendaDeportes.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_crePedido : ContentView
    {
        private SQLiteAsyncConnection conexion;
        public V_crePedido()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            var DatosRegistro = new T_Pedidos
            {
                FechaPedido = DateTime.Parse(fechaPedido.Text),
                DireccionPedido = direccion.Text,
                FechaEnvio = DateTime.Parse(fechaEnvio.Text),
                MedioEntrega = medio.Text,
                FechaEntrega = DateTime.Parse(fechaEntrega.Text),
                TotalPago = float.Parse(total.Text)
            };
        }

        private void Button_Clicked_1(object sender, System.EventArgs e)
        {
            limpiarFormulario();
        }
        void limpiarFormulario()
        {

            fechaPedido.Text = "";
            direccion.Text = "";
            fechaEnvio.Text = "";
            medio.Text = "";
            fechaEntrega.Text = "";
            total.Text = "";
        }
    }
}