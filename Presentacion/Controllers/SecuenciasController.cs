using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace Presentacion.Controllers
{
    [Authorize]
    public class SecuenciasController : Controller
    {
        // GET: Secuencia
        public ActionResult Index()
        {
            var dept = SecuenciaNegocio.GetSecuencias();
            return View(dept);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Secuencia obj)
        {
            try
            {
                var secuencia = SecuenciaNegocio.GetSecuencia();
                if(secuencia == null)
                {
                    SecuenciaNegocio.CreateSecuencia(obj);
                }
                else
                {
                    secuencia.Numero = obj.Numero;
                    SecuenciaNegocio.EditarSecuencia(secuencia);
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error al crear el Secuencia");
                return View(obj);
            }
        }
        public ActionResult Details(int id)
        {
            var dept = SecuenciaNegocio.GetSecuencia(id);
            return View(dept);
        }
        public ActionResult Edit(int id)
        {
            var dept = SecuenciaNegocio.GetSecuencia(id);
            return View(dept);
        }

        [HttpPost]
        public ActionResult Edit(Secuencia obj)
        {
            try
            {
                SecuenciaNegocio.EditarSecuencia(obj);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error al editar el Secuencia");
                return View(obj);
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dept = SecuenciaNegocio.GetSecuencia(id.Value);
            if (dept == null)
            {
                return HttpNotFound();
            }
            return View(dept);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                SecuenciaNegocio.EliminarSecuencia(id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error al eliminar el Secuencia");
                return View(id);
            }
        }
    }
}
