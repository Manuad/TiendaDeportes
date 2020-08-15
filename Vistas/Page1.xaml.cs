using SQLite;
using System;
using TiendaDeportes.Datos;
using TiendaDeportes.Tablas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiendaDeportes.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private SQLiteAsyncConnection con;
        public Page1()
        {
            InitializeComponent();
            con =  DependencyService.Get<ISQLiteDB>().GetConnection();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            DateTime fecha_pedido, fecha_entrega, fecha_envio;
            fecha_pedido = Convert.ToDateTime(fechaPedido.Text);
            fecha_entrega = Convert.ToDateTime(fechaEntrega.Text);
            fecha_envio = Convert.ToDateTime(fechaEnvio.Text);

            var DatosRegistro = new T_Pedidos
            {
                FechaPedido = fecha_pedido,
                DireccionPedido = direccion.Text,
                FechaEnvio = fecha_envio,
                MedioEntrega = medio.Text,
                FechaEntrega = fecha_entrega,
                TotalPago = float.Parse(total.Text)
            };
            con.InsertAsync(DatosRegistro);
            limpiarFormulario();
            DisplayAlert("Info", "Pedido guardado con éxito", "Ok");
        }

        private void Button_Clicked_1(object sender, System.EventArgs e)
        {
            limpiarFormulario();
        }
        void limpiarFormulario()
        {


            direccion.Text = "";
            fechaEnvio.Text = "";
            medio.Text = "";
            fechaEntrega.Text = "";
            total.Text = "";
        }

    }

}