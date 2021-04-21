using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMVC.Models;
using ProyectoMVC.Models.TableViewModels;

namespace ProyectoMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }


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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}