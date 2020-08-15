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


namespace TiendaDeportes.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_Productos : ContentPage
    {
        private SQLiteAsyncConnection con;

        public V_Productos()
        {
            InitializeComponent();
            con = DependencyService.Get<ISQLiteDB>().GetConnection();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var DatosRegistro = new T_Productos
            {
                Nombre = nombre.Text,
                Descripcion = descripcion.Text,
                Stock = int.Parse(stock.Text),
                PrecioUnidad = float.Parse(precioUnidad.Text)
            };
            con.InsertAsync(DatosRegistro);
            limpiarFormulario();
            DisplayAlert("Info","Producto almacendo en la base de Datos","OK");
            
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            limpiarFormulario();
        }

        void limpiarFormulario()
        {
            nombre.Text = "";
            descripcion.Text = "";
            stock.Text = "";
            precioUnidad.Text = "";
        }
    }
}