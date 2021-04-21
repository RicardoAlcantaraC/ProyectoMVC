using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoMVC.Models.ViewModels
{
    public class ArchivoViewModel
    {
        [Required]
        [DisplayName("Imágen")]
        public HttpPostedFileBase Imagen { get; set; }

        [Required]
        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        public string nombreImagen { get; set; }
    }
}