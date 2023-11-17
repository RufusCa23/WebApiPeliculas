using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiPeliculas.Models;

namespace WebApiPeliculas
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}
		public DbSet<Pelicula> Peliculas { get; set; }
		public DbSet<Genero> Generos { get; set; }
	}

}
