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
    public partial class V_DetallePedido : ContentPage
    {
        public int IdSeleccionado;
        public string FechaPedidoSeleccionada, DireccionSeleccionada, FechaEnvioSeleccionada, MedioSeleccionado, FechaEntregaSeleccionada;
        public float TotalSeleccionado;
        private SQLiteAsyncConnection con;
        IEnumerable<T_Pedidos> ResultadoDelete;
        IEnumerable<T_Pedidos> ResultadoUpdate;
        public V_DetallePedido(int id, string fechaPedido, string direccion, string fechaEnvio, string medio, string fechaEntrega, float total)
        {
            InitializeComponent();
            con = DependencyService.Get<ISQLiteDB>().GetConnection();
            IdSeleccionado = id;
            FechaPedidoSeleccionada = fechaPedido;
            DireccionSeleccionada = direccion;
            FechaEnvioSeleccionada = fechaEnvio;
            MedioSeleccionado = medio;
            FechaEntregaSeleccionada = fechaEntrega;
            TotalSeleccionado = total;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            mensaje.Text = "ID: " + IdSeleccionado;
            fechaPedido.Text = FechaPedidoSeleccionada;
            direccion.Text = DireccionSeleccionada;
            fechaEnvio.Text = FechaEnvioSeleccionada;
            medio.Text = MedioSeleccionado;
            fechaEntrega.Text = FechaEntregaSeleccionada;
            total.Text = Convert.ToString(TotalSeleccionado);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var rutaB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TiendaDeportiva.db3");
            var db = new SQLiteConnection(rutaB);
            ResultadoUpdate = Update(db, fechaPedido.Text, direccion.Text, fechaEnvio.Text, medio.Text, fechaEntrega.Text, 
                Convert.ToString(total.Text), IdSeleccionado);
            DisplayAlert("Confirmar","Los datos del pedido " + IdSeleccionado + " serán actualizados","Aceptar");
        }

        public static IEnumerable<T_Pedidos> Update(SQLiteConnection db, string fechaPedido, string direccion, 
            string fechaEnvio, string medio, string fechaEntrega, string total, int IdSeleccionado)
        {
            float toutal;
            toutal = float.Parse(total);
            DateTime fecha_pedido, fecha_entrega, fecha_envio;
            fecha_pedido = Convert.ToDateTime(fechaPedido);
            fecha_envio = Convert.ToDateTime(fechaEnvio);
            fecha_entrega = Convert.ToDateTime(fechaEntrega);

            return db.Query<T_Pedidos>("UPDATE T_Pedidos SET FechaPedido =?, DireccionPedido =?, FechaEnvio =?, MedioEntrega=?, " +
                "FechaEntrega =?, TotalPago =? WHERE ID =?", fecha_pedido, direccion, fecha_envio, medio, fecha_entrega, toutal, IdSeleccionado);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TiendaDeportiva.db3");
            var db = new SQLiteConnection(rutaDB);
            ResultadoDelete = Delete(db, IdSeleccionado);
            DisplayAlert("Confirmar","El pedido se eliminó satisfactoriamente","OK");
            limpiar();
        }

        public static IEnumerable<T_Pedidos> Delete(SQLiteConnection db, int id)
        {
            return db.Query<T_Pedidos>("DELETE FROM T_Pedidos WHERE ID=?", id);
        }

        public void limpiar()
        {
            mensaje.Text = "";
            fechaPedido.Text = "";
            direccion.Text = "";
            fechaEnvio.Text = "";
            medio.Text = "";
            fechaEntrega.Text = "";
            total.Text = "";
        }
    }
}