using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace ProyectoMVC.Models.ViewModels
{
    /*Clase para hacer el registro de un usuario
    1. Hacer los campos de clase
    2. Agregar System.ComponentModel.DataAnnotations para VALIDACIONES
     */
    public class UserViewModel
    {
        [Required] //<--- Uso del dataAnnotations para hacer el campo obligatorio
        [EmailAddress]
        [StringLength(maximumLength:100,MinimumLength =1,ErrorMessage ="El {0} debe contener al menos {1} caracteres")]
        [Display(Name ="Correo electrónico")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Las contraseñas no son iguales")]
        [Display(Name ="Confirmar contraseña")]
        public string ConfirmPassword { get; set; }


        [Required]
        [Display(Name ="Edad")]
        public int Edad { get; set; }
    }


}