using SQLite;
using SQLiteNetExtensions.Attributes;

namespace TiendaDeportes.Tablas
{
    [Table("T_PedidoProducto")]
    public class T_PedidoProducto
    {
        [ForeignKey(typeof(T_Pedidos))]
        public int IdPedido { get; set; }

        [ForeignKey(typeof(T_Productos))]
        public int IdProducto { get; set; }

        public int Cantidad { get; set; }
        public float PrecioUnitario { get; set; }
        public float SubTotal { get; set; }
        public float Descuento { get; set; }

    }
}
