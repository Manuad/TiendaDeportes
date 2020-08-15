using SQLite;

namespace TiendaDeportes.Tablas
{
    public class T_Productos
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(255)]
        public string Nombre { get; set; }

        [MaxLength(255)]
        public string Descripcion { get; set; }

        public int Stock { get; set; }

        public float PrecioUnidad { get; set; }
    }
}
