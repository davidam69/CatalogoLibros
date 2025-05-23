﻿using System.Globalization;
using System.Reflection.PortableExecutable;
using CatalogoLibros.Models;
using CsvHelper;

namespace CatalogoLibros.Models
{
    public class RepositorioLibros
    {
        public static List<Libro> ObtenerLibrosDesdeCSV(string rutaCSV)
        {
            var libros = new List<Libro>();

            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "|",
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using (var reader = new StreamReader(rutaCSV))
            using (var csv = new CsvReader(reader, config))
            {
                var registros = csv.GetRecords<LibroCSV>().ToList();

                var autores = registros
                    .GroupBy(r => new { r.autor_id, r.autor_nombre })
                    .Select(g => new Autor { id = g.Key.autor_id, nombre = g.Key.autor_nombre })
                    .ToList();

                foreach (var registro in registros)
                {
                    var autor = autores.FirstOrDefault(a => a.id == registro.autor_id);
                    libros.Add(new Libro
                    {
                        id = registro.autor_id * 100 + libros.Count(l => l?.autor?.id == registro.autor_id) + 1,
                        titulo = registro.libro_titulo,
                        anioPublicacion = registro.anio_publicacion,
                        autor = autor,
                        sinopsis = registro.sinopsis,
                        UrlImagen = registro.UrlImagen
                    });
                }
            }

            return libros;
        }

    }

    public class LibroCSV
    {
        public int autor_id { get; set; }
        public string? autor_nombre { get; set; }
        public string? libro_titulo { get; set; }
        public int anio_publicacion { get; set; }
        public string? sinopsis { get; set; }
        public string? UrlImagen { get; set; }
    }
}
