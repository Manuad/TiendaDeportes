using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TiendaDeportes.Datos;
using TiendaDeportes.Tablas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiendaDeportes.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_prinPedido : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public V_prinPedido()
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
                db.CreateTable<T_Pedidos>();
                IEnumerable<T_Pedidos> resultado = SELECT_WHERE(db, fechaPedido.Text);
                if (resultado.Count() > 0)
                {
                    Navigation.PushAsync(new V_readPedidos());
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
       

        public static IEnumerable<T_Pedidos> SELECT_WHERE(SQLiteConnection db, string fechaPedido)
        {
            
            DateTime fecha;
            fecha = Convert.ToDateTime(fechaPedido);
            
            return db.Query<T_Pedidos>("SELECT * FROM T_Pedidos WHERE FechaPedido=?", fecha);
        }


        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page1());
        }
    }
}