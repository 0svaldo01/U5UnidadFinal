using System;
using System.Collections.Generic;
using System.Text;

namespace U5UnidadF.Models
{
    class Pelicula
    {
        public string Titulo { get; set; }
        public string DuracionMin { get; set; }
        public string Calificacion { get; set; }
        public string Director { get; set; }
        public int Año { get; set; }
        public string Sinopsis { get; set; }
        public string Genero { get; set; } 
        public string Portada { get; set; } = "";


    }
}
