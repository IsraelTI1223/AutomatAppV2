using AutomatApp.Business.CatWH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomatAppV2.Controllers
{
    public class CatProveedorController : Controller
    {
        // GET: CatProveedor
        public ActionResult Index()
        {
            CatWHBusiness WHBusiness = new CatWHBusiness();
            return View();
        }
    }
}