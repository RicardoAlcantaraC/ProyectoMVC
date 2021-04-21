using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoMVC.Models.TableViewModels
{
    public class ArticleTableViewModel
    {
        public string Nombre { get; set; }
        public DateTime FechaHora { get; set; }
        public string Descripcion { get; set; }
        public int Id { get; set; }
        public string Imagen { get; set; }
        public int? IdUsu { get; set; }
    }

   
}