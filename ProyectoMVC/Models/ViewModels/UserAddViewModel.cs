using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProyectoMVC.Models;
using ProyectoMVC.Models.TableViewModels;
using ProyectoMVC.Models.ViewModels;


namespace ProyectoMVC.Models.ViewModels
{
    public class UserAddViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required] //<--- Uso del dataAnnotations para hacer el campo obligatorio
        [EmailAddress]
        [EmailExist(ErrorMessage = "Ésta cuenta de correo ya esta asociada a un perfil en nuestro Blog")]
        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "El {0} debe contener al menos {1} caracteres")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "La contraseña debe tener: Longitud de 8 carácteres, letras mayúsculas y minúsculas, números y símbolos especiales")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no son iguales")]
        [Display(Name = "Confirmar contraseña")]
        public string ConfirmPassword { get; set; }


        [Required]
        [Range(14, 80, ErrorMessage = "Debes tener más de 14 años para registrarte")]
        [Display(Name = "Edad")]
        public int Edad { get; set; }

        
        public string TokenCuenta { get; set; }
        public int Id { get; set; }
    }

    public class EmailExistAttribute : ValidationAttribute
    {
        
        public override bool IsValid(object value)
        {
            using(cursomvcEntities db = new cursomvcEntities())
            {
                var lst = from t in db.user where t.email == value.ToString() select value.ToString();
                List<string> miLista = new List<string>();
                foreach (var item in lst)
                {
                    miLista.Add(item.ToString());
                }

                if (miLista.Contains(value.ToString()))
                {
                    return false;
                }
                else { return true; }
            }
           
        }
         


    }

    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }



        [Required] //<--- Uso del dataAnnotations para hacer el campo obligatorio
        [EmailAddress]
        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = "El {0} debe contener al menos {1} caracteres")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "La contraseña debe tener: Longitud de 8 carácteres, letras mayúsculas y minúsculas, números y símbolos especiales")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no son iguales")]
        [Display(Name = "Confirmar contraseña")]
        public string ConfirmPassword { get; set; }


        [Required]
        [Display(Name = "Edad")]
        public int Edad { get; set; }
    }
}