using Microsoft.AspNetCore.Mvc;
using CatalogoLibros.Models;
public class LibroController : Controller
{

    public IActionResult Index()
    {
        var rutaCSV = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/autores_libros.csv");
        var libros = RepositorioLibros.ObtenerLibrosDesdeCSV(rutaCSV);
        return View(libros);
    }

    public IActionResult Detalle(int id)
    {
        var rutaCSV = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/autores_libros.csv");
        var libros = RepositorioLibros.ObtenerLibrosDesdeCSV(rutaCSV);
        var libro = libros.FirstOrDefault(l => l.id == id);

        if (libro == null)
        {
            ViewBag.Error = "Libro no encontrado.";
            return View("Error");
        }

        return View(libro);
    }

    public IActionResult PorAutor(int autorId)
    {
        var rutaCSV = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/autores_libros.csv");
        var libros = RepositorioLibros.ObtenerLibrosDesdeCSV(rutaCSV);
        var librosAutor = libros.Where(l => l?.autor?.id == autorId).ToList();

        if (librosAutor.Count == 0)
        {
            ViewBag.Mensaje = "Este autor no tiene libros en el catálogo.";
            return View("Error");
        }

        ViewBag.AutorNombre = librosAutor?.First()?.autor?.nombre;
        return View(librosAutor);
    }

}
