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
    public partial class V_DetallaProducto : ContentPage
    {
        public int IdSeleccionado, StockSeleccionado;
        public string NombreSeleccionado, DescripcionSeleccionada;
        public float PrecioSeleccionado;
        private SQLiteAsyncConnection con;
        IEnumerable<T_Pedidos> ResultadoDelete;
        IEnumerable<T_Pedidos> ResultadoUpdate;
        public V_DetallaProducto(int id, string nombre, string descripcion, int stock, float precioUnidad)
        {
            InitializeComponent();
            con = DependencyService.Get<ISQLiteDB>().GetConnection();
            IdSeleccionado = id;
            NombreSeleccionado = nombre;
            DescripcionSeleccionada = descripcion;
            StockSeleccionado = stock;
            PrecioSeleccionado = precioUnidad;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            mensaje.Text = "ID: " + IdSeleccionado;
            nombre.Text = NombreSeleccionado;
            descripcion.Text = DescripcionSeleccionada;
            stock.Text = Convert.ToString(StockSeleccionado);
            precioUnidad.Text = Convert.ToString(PrecioSeleccionado);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var rutaB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TiendaDeportiva.db3");
            var db = new SQLiteConnection(rutaB);
            ResultadoUpdate = Update(db, nombre.Text, descripcion.Text, Convert.ToString(stock.Text), precioUnidad.Text, IdSeleccionado);
            DisplayAlert("Confirmar", "Los datos del pedido " + IdSeleccionado + " serán actualizados", "Aceptar");
        }

        public static IEnumerable<T_Pedidos> Update(SQLiteConnection db, string nombre, string descripcion,
           string stock , string precioUnidad, int IdSeleccionado)
        {
            float precioU;
            int stockin;
            stockin = int.Parse(stock);
            precioU = float.Parse(precioUnidad);

            return db.Query<T_Pedidos>("UPDATE T_Productos SET Nombre=?, Descripcion=?, Stock=?, PrecioUnidad=? WHERE ID=?", 
                nombre, descripcion, stockin, precioU,  IdSeleccionado);
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TiendaDeportiva.db3");
            var db = new SQLiteConnection(rutaDB);
            ResultadoDelete = Delete(db, IdSeleccionado);
            DisplayAlert("Confirmar", "El producto se eliminó satisfactoriamente", "OK");
            limpiar();
        }
        public static IEnumerable<T_Pedidos> Delete(SQLiteConnection db, int id)
        {
            return db.Query<T_Pedidos>("DELETE FROM T_Productos WHERE ID=?", id);
        }
        public void limpiar()
        {
            nombre.Text = "";
            descripcion.Text = "";
            stock.Text = "";
            precioUnidad.Text = "";
        }
    }
}