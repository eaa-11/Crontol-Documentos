using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using PagedList;

namespace Presentacion.Controllers
{
    [Authorize]
    public class DepartamentController : Controller
    {
        // GET: Departament
        public ActionResult Index(string filter, int? i)
        {
            return View(DepartamentoNegocio.GetDepartamanets(filter).ToPagedList(i ?? 1, 10));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Departament obj)
        {
            try
            {
                DepartamentoNegocio.CreateDepartment(obj);
                return RedirectToAction("Index");
            }
            catch (Exception )
            {
                ModelState.AddModelError("", "Error al crear el departamento");
                return View(obj);
            }
        }     
        public ActionResult Details(int id)
        {
            var dept = DepartamentoNegocio.GetDepartament(id);
            return View(dept);
        }
        public ActionResult Edit(int id)
        {
            var dept = DepartamentoNegocio.GetDepartament(id);
            return View(dept);
        }

        [HttpPost]
        public ActionResult Edit(Departament obj)
        {
            try
            {
                DepartamentoNegocio.EditarDepartamento(obj);
                return RedirectToAction("Index");
            }
            catch (Exception )
            {
                ModelState.AddModelError("", "Error al editar el departamento");
                return View(obj);
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dept = DepartamentoNegocio.GetDepartament(id.Value);
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
                DepartamentoNegocio.EliminarDepartamento(id);
                return RedirectToAction("Index");
            }
            catch (Exception )
            {
                ModelState.AddModelError("", "Error al eliminar el departamento");
                return View(id);
            }
        }
    }
}