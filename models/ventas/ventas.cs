namespace Cinema.Models
{
    public class Venta
    {
        public string idVenta { get; set; }
        public string nombreCliente { get; set; }
        public int cantidadBoletos { get; set;}
        public int precioTotal { get; set; }
        public string pelicula { get; set; }
        public string metodoPago { get; set; }
    }
}
