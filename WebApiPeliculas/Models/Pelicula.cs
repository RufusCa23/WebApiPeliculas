using System.ComponentModel.DataAnnotations;

namespace WebApiPeliculas.Models
{
	public class Pelicula
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "El Campo {0} es requerido")]
		[StringLength(maximumLength:50, ErrorMessage = "El campo {0} no debe tener mas de {1} caracter")]
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		public double Calificacion {  get; set; }
		public double Duracion { get; set; }
		public string Imagen { get; set; }

	}
}
