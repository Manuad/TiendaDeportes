using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using TiendaDeportes.Datos;
using TiendaDeportes.Tablas;

namespace TiendaDeportes.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_prinProductos : ContentPage
    {
        private SQLiteAsyncConnection conexion;

        public V_prinProductos()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var rutaDB = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TiendaDeportiva.db3");
                var db = new SQLiteConnection(rutaDB);
                db.CreateTable<T_Productos>();
                IEnumerable<T_Productos> resultado = SELECT_WHERE(db, nombre.Text);
                if (resultado.Count() > 0)
                {
                    Navigation.PushAsync(new V_readProductos());
                }
                else
                {
                    DisplayAlert("Info", "No se ha encontrado ningun pedido con esa fecha", "Aceptar");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<T_Productos> SELECT_WHERE(SQLiteConnection db, string nombre)
        {

            return db.Query<T_Productos>("SELECT * FROM T_Productos WHERE Nombre=?", nombre);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new V_Productos());
        }
    }
}