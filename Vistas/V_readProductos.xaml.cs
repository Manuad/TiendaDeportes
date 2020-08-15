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
using System.IO;
using System.Collections.ObjectModel;

namespace TiendaDeportes.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class V_readProductos : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<T_Productos> TablaProductos;
        public V_readProductos()
        {
            InitializeComponent();
            con = DependencyService.Get<ISQLiteDB>().GetConnection();
            ListaProductos.ItemSelected += ListaProductos_ItemSelected;
        }

        private void ListaProductos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var Obj = (T_Productos)e.SelectedItem;
            var item = Obj.ID.ToString();
            var nombre = Obj.Nombre;
            var descripcion = Obj.Descripcion;
            var stock = Obj.Stock.ToString();
            var precioUnitario = Obj.PrecioUnidad.ToString();
            int Id = Convert.ToInt32(item);
            float precioU = float.Parse(precioUnitario);
            int stockin = int.Parse(stock);
            try
            {
                Navigation.PushAsync(new V_DetallaProducto(Id, nombre, descripcion, stockin, precioU));
            }

            catch (Exception)
            {
                throw;
            }
        }

        protected async override void OnAppearing()
        {
            var ResultadoRegistro = await con.Table<T_Productos>().ToListAsync();
            TablaProductos = new ObservableCollection<T_Productos>(ResultadoRegistro);
            ListaProductos.ItemsSource = TablaProductos;
            base.OnAppearing();
        }
    }
}