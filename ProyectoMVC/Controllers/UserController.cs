using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMVC.Models;
using ProyectoMVC.Models.TableViewModels;
using ProyectoMVC.Models.ViewModels;
using ProyectoMVC.Filters;
using System.IO;

namespace ProyectoMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
       
        public ActionResult Index()
        {
            int w = Convert.ToInt32(Session["idusu"]);
            List<UserTableViewModel> lst = null;

            using(cursomvcEntities db = new cursomvcEntities())
            {
                lst = (from t in db.user
                      where t.Id == w
                      select new UserTableViewModel
                      {
                          Email = t.email,
                          Id = t.Id,
                          Edad = t.edad,
                          Nombre = t.nombre
                          
                      }).ToList();
            }

            return View(lst);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Add(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new cursomvcEntities())
            {
                user oUser = new user();
                oUser.IdState = 1;
                oUser.email = model.Email;
                oUser.password = model.Password;
                oUser.edad = model.Edad;

                db.user.Add(oUser);
                db.SaveChanges();
            }

            return Redirect(Url.Content("~/User/"));
            
        }

        public ActionResult Edit(int id)
        {
            EditUserViewModel model = new EditUserViewModel();

            using (var db = new cursomvcEntities())
            {
                var oUser = db.user.Find(id);
                model.Edad = (int)oUser.edad;
                model.Email = oUser.email;
                model.Id = oUser.Id;
                model.Nombre = oUser.nombre;

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new cursomvcEntities())
            {
                var oUser = db.user.Find(model.Id);
                oUser.email = model.Email;
                oUser.edad = model.Edad;
                oUser.nombre = model.Nombre;

                if(model.Password != null && model.Password.Trim() != "")
                {
                    oUser.password = Encrypt.GetSHA256(model.Password);
                }

                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                
            }

            return Redirect(Url.Content("~/User/"));
        }


        [HttpPost]
        public ActionResult Delete(int Id)
        {


            using (var db = new cursomvcEntities())
            {
                var oUser = db.user.Find(Id);
                oUser.IdState = 3;               

                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }

            return Content("1");
        }


        public ActionResult NewArticle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveArticulo(ArchivoViewModel mdel)
        {
            int ses = (int)Session["idusu"];
            string nameimage = mdel.Imagen.FileName;
            DateTime today = DateTime.Now;

            string RutaSitio = Server.MapPath("~/");
            string pathImg = Path.Combine(RutaSitio+"/Files/"+ nameimage);
           
            if (!ModelState.IsValid) 
            {
                return View("NewArticle",mdel);
            }

            using (cursomvcEntities db = new cursomvcEntities())
            {
                articulo oArticulo = new articulo();
                oArticulo.idState = 1;
                oArticulo.nombre = mdel.Nombre;
                oArticulo.descripcion = mdel.Descripcion;
                oArticulo.fecha = today;
                oArticulo.nombreImg = nameimage;
                oArticulo.idUsuario = ses;

                db.articulo.Add(oArticulo);
                db.SaveChanges();
            }

            mdel.Imagen.SaveAs(pathImg);
            @TempData["Message"] = "Se ha publicado tu artículo";

            return RedirectToAction("Index","Home");
               
        }

        public ActionResult Article(int id)//id del articulo
        {

            
            List<ArticleTableViewModel> lst = null;
           
            using (cursomvcEntities db = new cursomvcEntities())
            {
                lst = (from t in db.articulo
                       where t.id == id
                       select new ArticleTableViewModel
                       {
                           Nombre = t.nombre,
                          Imagen = t.nombreImg,
                          IdUsu = t.idUsuario,
                          Descripcion = t.descripcion
                       }).ToList();

               var u = (from a in db.articulo
                        join b in db.user
                        on a.idUsuario equals b.Id
                        select b.nombre);

                ViewBag.AutorArticulo = u.FirstOrDefault();

            }

            
            string pathImg = "../../Files/";
            
            
            ViewBag.NombreArticulo = lst;
            ViewBag.RutaImagen = pathImg;
            return View();
        }

    }
}
