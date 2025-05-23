﻿using Microsoft.AspNetCore.Mvc;
using CatalogoLibros.Models;
public class LibroController : Controller
{

     public IActionResult Index()
     {
         ViewBag.ColorFondo = TempData["ColorFondo"] ?? "white";
         TempData.Keep("ColorFondo");
         var rutaCSV = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/autores_libros.csv");
         var libros = RepositorioLibros.ObtenerLibrosDesdeCSV(rutaCSV);
         return View(libros);
     }


    public IActionResult Detalle(int id)
    {
        ViewBag.ColorFondo = TempData["ColorFondo"] ?? "white";
        TempData.Keep("ColorFondo"); // mantiene TempData para futuras páginas
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
        ViewBag.ColorFondo = TempData["ColorFondo"] ?? "white";
        TempData.Keep("ColorFondo"); // mantiene TempData para futuras páginas
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

    public IActionResult Privacidad()
    {
        ViewBag.ColorFondo = TempData["ColorFondo"] ?? "white";
        TempData.Keep("ColorFondo"); // mantiene TempData para futuras páginas
        return View();
    }

    public IActionResult CambiarFondo(string color)
    {
        TempData["ColorFondo"] = color;
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Crear()
    {
        ViewBag.Autores = ObtenerAutores(); // Este método debe devolverte una lista de autores.
        return View();
    }

    [HttpPost]
    public IActionResult Crear(Libro libro)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Autores = ObtenerAutores(); // Necesario para volver a cargar la lista si hay errores
            return View(libro);
        }

        // Simular almacenamiento. Podrías guardar en una lista o base de datos.
        // libro.Id = GenerarId(); si lo necesitas
        // libros.Add(libro);

        return RedirectToAction("Detalle", new { id = libro.id });
    }

    public  List<Autor> ObtenerAutores()
    {
        var rutaCSV = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/autores_libros.csv");
        var autores = new List<Autor>();

        var lineas = System.IO.File.ReadAllLines(rutaCSV);

        foreach (var linea in lineas.Skip(1)) // Saltar encabezado
        {
            var partes = linea.Split(',');

            if (partes.Length >= 2 && int.TryParse(partes[0], out int id))
            {
                autores.Add(new Autor
                {
                    id = id,
                    nombre = partes[1]
                });
            }
        }

        return autores;
    }
}
