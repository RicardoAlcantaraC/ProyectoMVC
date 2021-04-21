using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoMVC.Controllers
{
    public class CerrarController : Controller
    {
        // GET: Cerrar
       

        public ActionResult Logoff()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Access");

        }
    }
}