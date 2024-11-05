namespace Cinema.Models
{
    public class Pelicula
    {
        public int idPelicula { get; set; }
        public string imgURL { get; set; }
        public string nombrePeli { get; set; }
        public string descripcion { get; set; }
        public int rating { get; set; }
        public int duracion { get; set; }
        public int asientosDisponibles { get; set; } 
        public int precioBoleto { get; set; } 

    }
}
