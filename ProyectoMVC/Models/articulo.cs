//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class articulo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public System.DateTime fecha { get; set; }
        public string descripcion { get; set; }
        public int idState { get; set; }
        public string nombreImg { get; set; }
        public Nullable<int> idUsuario { get; set; }
    
        public virtual cstate cstate { get; set; }
        public virtual user user { get; set; }
    }
}
