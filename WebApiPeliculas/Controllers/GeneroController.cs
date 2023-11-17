using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPeliculas.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApiPeliculas.Controllers
{
	[ApiController] //Con esto se dice que la clase es un controlador
	[Route("api/[controller]")]
	public class GeneroController : Controller //Siempre debe extenderse de controler
	{
		private readonly ApplicationDbContext context;
		public GeneroController(ApplicationDbContext context)
		{
			this.context = context; //El This es para hacer referencia a la variable

		}

		//Endpoint dar de alta genero
		[HttpPost("RegistrarGenero")]
		//Task sirve para poder manadar diferentes tipos de valores (Como en formato Jaison)
		//Async funciona para hacer diferentes funciones asincronas, utilizando metodos asincronos
		public async Task<ActionResult> RegistrarGenero(Genero genero)
		{
			var existeGenero = await context.Generos.AnyAsync(x => x.Nombre == genero.Nombre);
			if (existeGenero)
			{
				return BadRequest($"El genero {genero.Nombre} ya existe");
			}

			context.Add(genero);
			await context.SaveChangesAsync();
			return Ok(genero);
		}


		//End point para listar todas las categorias
		[HttpGet("ListarGeneros")]
		public async Task<ActionResult<List<Genero>>> ListarGeneros()
		{
			var generos = await context.Generos.ToListAsync();
			return Ok(generos);
		}

		//End point para buscar la informacion de un genero en especifico
		[HttpGet("GeneroEspecifico/{nombre}")]
		public async Task<ActionResult<Genero>> GeneroEspecifico(string nombre)
		{
			var genero = await context.Generos.FirstOrDefaultAsync(x => x.Nombre == nombre);
			if (genero == null)
			{
				return NotFound();
			}
			return Ok(genero);
		}

		//End point para actualizar informacion de un genero
		[HttpPut("GeneroActualizado/{nombre}")]
		public async Task<ActionResult<Genero>> ActualizarGenero(string nombre, [FromBody] Genero generoActualizado)
		{
			var genero = await context.Generos.FirstOrDefaultAsync(x => x.Nombre == nombre);

			if (genero == null)
			{
				return NotFound();
			}

			genero.Nombre = generoActualizado.Nombre; // Actualiza el nombre u otros campos según sea necesario.

			try
			{
				await context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				// Manejar excepciones de concurrencia si es necesario.
				return Conflict();
			}

			return Ok(genero);
		}

		//Endpoint para eliminar un genero
		[HttpDelete("EliminarGenero/{id:int}")]
		public async Task<ActionResult> EliminarCategoria(int id)
		{
			var existe = await context.Generos.AnyAsync(z => z.Id == id);
			if (!existe) return NotFound();
			context.Remove(new Genero() { Id = id });
			await context.SaveChangesAsync();
			return Ok();
		}

		//End point para modidificar todo un genero "Prof.Cesar"
		[HttpPut("Modificar/{id:int}")]
		public async Task<ActionResult> ModificarGenero(int id, Genero genero)
		{
			var existe = await context.Generos.AnyAsync(x => x.Id == id); //Aunque no exista el AnyAsync manda el valor como null
			if (!existe) return NotFound("El producto no existe");

			context.Update(genero);
			await context.SaveChangesAsync();
			return Ok(genero);
		}


	}
}

