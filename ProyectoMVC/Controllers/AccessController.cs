using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using ProyectoMVC.Models;
using ProyectoMVC.Models.TableViewModels;
using ProyectoMVC.Models.ViewModels;
using ProyectoMVC.Filters;


namespace ProyectoMVC.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            HttpContext.Application["verifyAccount"] = 2;
            List<ArticleTableViewModel> lst = null;

            using (cursomvcEntities db = new cursomvcEntities())
            {
                lst = (from t in db.articulo
                       where t.idState == 1
                       orderby t.fecha
                       select new ArticleTableViewModel
                       {
                           Nombre = t.nombre,
                           Id = t.id
                           
                       }).ToList();
            }
            return View(lst);
        }

      

        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(UserAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model) ;
            }

            string body="";
            VerifyEmail objVerifyEmail = new VerifyEmail();
            try
            {
                using (var db = new cursomvcEntities())
                {
                    user oUser = new user();
                    oUser.IdState = 2;   //1-Activo 2-Activacion Pendiente 3-Eliminado
                    oUser.nombre = model.Nombre;
                    oUser.email = model.Email;
                    oUser.password = Encrypt.GetSHA256(model.Password);
                    oUser.edad = model.Edad;
                    oUser.tokenCuenta = Encrypt.genTok(model.Email);

                    db.user.Add(oUser);
                    db.SaveChanges();

                    string c = "\"";
                    
                   
                    body = @"
                <body>
	                <style>
                      h3{color:default;}
                      h4{color:lightgreen;}
                    </style>
 
                 <div>   
 	
                 <h3>Hola "+oUser.nombre+ @".</h3>
                   <p>Gracias por unirte a nuestro Blog. Para hacer uso de nuestra plataforma por favor da clic en el enlace para verificar tu cuenta.</p>
                      <div align="+c+"center"+c+ @">
                      <a href="+c+ "https://localhost:44376/Access/VerifyComplete?tk=" +oUser.tokenCuenta +"&n="+oUser.Id+ c+@">Activar Cuenta</a>
                       
                        <br>
                        <br>
                        <div align=" + c + "left" + c + @">
                        Ricardo Alcántara Castro<br>
                        5561086391 <br>
                        Ingeniero en Computación
                    </div>
                 </body>
                ";

                 objVerifyEmail.sendMail(oUser.email, "Activar Cuenta", body);
                }
                HttpContext.Application["verifyAccount"] = 1;
               
                return Redirect(Url.Content("~/Access/Verify/"));
            }
            catch (Exception ex)
            {

                return Redirect(Url.Content("~/Access/")+ ex.Message);
            }
            
            
        }

        public ActionResult Verify()
        {
            if (HttpContext.Application["verifyAccount"].Equals(1))
            {
                return View();
            }
            else
            {
                return Redirect(Url.Content("~/Access/"));
            }
        }


        public ActionResult VerifyComplete()
        {
            string value = Request.QueryString["tk"];
            int idk =Convert.ToInt32(Request.QueryString["n"]);

            if (value != "")
            {
                using (var db = new cursomvcEntities())
                {
                    var activada = from d in db.user where d.tokenCuenta == value && d.IdState == 2 && d.Id== idk select d.IdState;

                    if (activada.Contains(2))
                    {
                        var oUser = db.user.Find(idk);
                        oUser.IdState = 1;

                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return View();
                    }
                   

                    else
                    {
                        return Redirect(Url.Content("~/Access/"));
                    }

                }
            }
            else
            {
                return Redirect(Url.Content("~/Access/"));
            }


        }

        public ActionResult Enter(string user, string pass)
        {
            try
            {
                string ePass = Encrypt.GetSHA256(pass);
                using(cursomvcEntities db = new cursomvcEntities())
                {
                    var lst = from usu in db.user
                              where usu.email == user && usu.password == ePass && usu.IdState == 1
                              select usu;

                    var lst2 = from usu in db.user
                              where usu.email == user && usu.password == ePass && usu.IdState == 1
                              select usu.email;

                    var lst3 = from usu in db.user
                               where usu.email == user && usu.password == ePass && usu.IdState == 1
                               select usu.nombre;

                    var lst4 = from usu in db.user
                               where usu.email == user && usu.password == ePass && usu.IdState == 1
                               select usu.Id;

                    if (lst.Count() > 0)
                    {
                        Session["user"] = lst.First();
                        Session["email"] = lst2.First();
                        Session["nombre"] = lst3.First();
                        Session["idusu"] = lst4.First();

                        return Content("1");
                    }
                    else
                    {
                        return Content("Usuario Inválido. O cuenta no activada");
                    }

                }

               
            }
            catch (Exception ex)
            {

                return Content("Ocurrio un error :(" + ex.Message);
            }
        }

        public ActionResult RecuperarPass(string email)
        {
            string body = "";
            VerifyEmail objVerifyEmail = new VerifyEmail();
            user oUser = new user();
            
            try
            {
               
                using (cursomvcEntities db = new cursomvcEntities())
                {
                    try
                    {
                       var lst = from usu in db.user
                                  where usu.email == email && usu.IdState == 1   //Tiene que ser un usuario con cuenta activada
                                  select usu.Id;

                        Random cod = new Random(Convert.ToInt32(lst.First()));
                        int n = cod.Next(1000, 1000000);

                        var okUser = db.user.Find(Convert.ToInt32(lst.First()));
                        okUser.password = Encrypt.GetSHA256(n.ToString());
                        db.Entry(okUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        int codigo = n;

                        if (lst.Count() > 0)
                        {
                            //Mandas el correo

                            string c = "\"";


                            body = @"
                        <body>
	                        <style>
                              h3{color:default;}
                              h4{color:lightgreen;}
                            </style>
 
                         <div>   
 	
                         <h3>Hola " + okUser.nombre + @".</h3>
                           <p>Damos seguimiento a la recuperación de tu contraseña. Podrás acceder a nuestro blog con el siguiente código.</p>
                            <p>Código de Acceso: " + codigo + @"</p>  
                         </body>
                        ";

                            objVerifyEmail.sendMail(email, "Recuperar Contraseña", body);

                            return Content("1");
                        }
                        else
                        {
                            return Content("El correo proporcionado no se encuentra registrado en nuestro Blog");
                        }
                    }
                    catch (Exception d)
                    {

                        return Content("El correo proporcionado no se encuentra registrado en nuestro Blog");
                    }
                   

                    
                 }
                        


            }
            catch (Exception ex)
            {

                return Content("Ocurrio un error :(" + ex.Message);
            }
        }
    }
}