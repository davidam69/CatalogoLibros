using System.ComponentModel.DataAnnotations;

namespace CatalogoLibros.Models
{
    public class Libro
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El campo Título es obligatorio.")]
        public string? titulo { get; set; }

        [Range(1800, 2100, ErrorMessage = "El año debe estar entre 1800 y 2100.")]
        public int anioPublicacion { get; set; }

        [Required(ErrorMessage = "El campo Autor es obligatorio.")]
        public Autor? autor { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un autor.")]
        public int autorId { get; set; }
        public string? sinopsis{ get; set; }
        public string? UrlImagen { get; set; }
    }
}
