using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMVC.Models.TableViewModels;
using ProyectoMVC.Models.ViewModels;

namespace ProyectoMVC.Controllers
{
    public class UploadImageController : Controller
    {
        // GET: UploadImage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Save(ArchivoViewModel model)
        {
            return View();
        }
    }
}