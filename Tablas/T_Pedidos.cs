using SQLite;
using System;

namespace TiendaDeportes.Tablas
{
    public class T_Pedidos
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public DateTime FechaPedido { get; set; }

        [MaxLength(255)]
        public string DireccionPedido { get; set; }

        public DateTime FechaEnvio { get; set; }

        [MaxLength(255)]
        public string MedioEntrega { get; set; }

        public DateTime FechaEntrega { get; set; }

        public float TotalPago { get; set; }

    }
}
