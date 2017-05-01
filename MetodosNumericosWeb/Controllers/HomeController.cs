using MetodosNumericos.BusinessLogic.Implementacion;
using MetodosNumericos.BusinessLogic.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetodosNumericosWeb.Controllers
{
    public class HomeController : Controller
    {
        IEcuacion _ecuacion;
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerValoresCordenadas(string sFunction, int inicial, int final)
        {
            _ecuacion = new EcuacionGeneral();
            var valores = _ecuacion.EvaluateInX(sFunction, inicial, final);            
            return Json(valores, JsonRequestBehavior.AllowGet);
        }

        
    }
}