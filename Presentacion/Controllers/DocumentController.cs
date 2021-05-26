using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
    public class DocumentController : Controller
    {
        // GET: Document
        public ActionResult Index(string tipofiltro, int? Departament_Destino_Id, int? Departament_Origen_Id, int? User_Id, DateTime? fechainicio, DateTime? fechafinal, int? i)
        {
            ViewBag.Departament_Destino_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre", Departament_Destino_Id);
            ViewBag.Departament_Origen_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre", Departament_Origen_Id);
            ViewBag.User_Id = new SelectList(UsuarioNegocio.GetUsers(""), "Id", "Nombres", User_Id);
            ViewBag.tipofiltro = tipofiltro;
            ViewBag.fechainicio = fechainicio;
            ViewBag.fechafinal = fechafinal;

            if (tipofiltro != null)
            {                
                switch (tipofiltro)
                {
                    case "usuario":
                        if(User_Id != null)
                        {
                            return View(DocumentoNegocio.GetDocumentsByUser(User_Id).ToPagedList(i ?? 1, 10));
                        }
                        ViewBag.Error = "Selecciona un Usuario";
                        break;
                    case "origen":
                        if(Departament_Origen_Id != null) { 
                            return View(DocumentoNegocio.GetDocumentsByOrigin(Departament_Origen_Id).ToPagedList(i ?? 1, 10));
                        }
                        ViewBag.Error = "Selecciona un Departamento de Origen";
                        break;
                    case "destino":
                        if (Departament_Destino_Id != null)
                        {
                            return View(DocumentoNegocio.GetDocumentsByDestination(Departament_Destino_Id).ToPagedList(i ?? 1, 10));
                        }
                        ViewBag.Error = "Selecciona un Departamento de Destino";
                        break;
                    case "fecha":
                        if(fechainicio != null && fechafinal != null)
                        {
                            return View(DocumentoNegocio.GetDocumentsByDate(fechainicio, fechafinal).ToPagedList(i ?? 1, 10));
                        }
                        ViewBag.Error = "Ingrese una fecha inicio y una fecha final";
                        break;
                    case "todos":
                        return View(DocumentoNegocio.GetDocuments().ToPagedList(i ?? 1, 10));
                    default:
                        ViewBag.Error = "Seleccionar el Tipo de filtro";
                        break;
                        
                }
            }
            return View(DocumentoNegocio.GetDocuments().ToPagedList(i ?? 1, 10));
        }
        public ActionResult Create()
        {
            ViewBag.Departament_Destino_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre");
            ViewBag.Departament_Origen_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre");
            ViewBag.User_Id = new SelectList(UsuarioNegocio.GetUsers(""), "Id", "Nombres");
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,User_Id,Departament_Origen_Id,Departament_Destino_Id")] Document document)
        { 
            if (ModelState.IsValid)
            {
                document.Fecha = DateTime.Now;
                string numero;
                Secuencia secuencia = SecuenciaNegocio.GetSecuencia();
                if (secuencia == null)
                {
                    numero = "001";
                    SecuenciaNegocio.CreateSecuencia(new Secuencia() { Numero = 2 });
                }
                else
                {
                    if (secuencia.Numero >= 0 && secuencia.Numero < 10)
                    {
                        numero = "00" + secuencia.Numero;
                    }
                    else if (secuencia.Numero >= 10 && secuencia.Numero < 100)
                    {
                        numero = "0" + secuencia.Numero;
                    }
                    else
                    {
                        numero = "" + secuencia.Numero;
                    }
                    secuencia.Numero = secuencia.Numero + 1;
                    SecuenciaNegocio.EditarSecuencia(secuencia);
                }
                document.Numero = numero;
                Departament Origen = DepartamentoNegocio.GetDepartament(document.Departament_Origen_Id);
                Departament Destino = DepartamentoNegocio.GetDepartament(document.Departament_Destino_Id);

                document.Nombre = document.Fecha.Year + "-" + Origen.Siglas + "-" + Destino.Siglas + "-" + numero;
                DocumentoNegocio.CreateDocuments(document);
                return RedirectToAction("Index");
            }

            ViewBag.Departament_Destino_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre", document.Departament_Destino_Id);
            ViewBag.Departament_Origen_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre", document.Departament_Origen_Id);
            ViewBag.User_Id = new SelectList(UsuarioNegocio.GetUsers(""), "Id", "Nombres", document.User_Id);
            ModelState.AddModelError("", "Error al crear el documento");
            return View(document);
        }
        public ActionResult Details(int id)
        {
            var doc = DocumentoNegocio.GetDocumento(id);
            return View(doc);
        }
        public ActionResult Edit(int id)
        {
            Document document = DocumentoNegocio.GetDocumento(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.Departament_Destino_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre", document.Departament_Destino_Id);
            ViewBag.Departament_Origen_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre", document.Departament_Origen_Id);
            ViewBag.User_Id = new SelectList(UsuarioNegocio.GetUsers(""), "Id", "Nombres", document.User_Id);
            return View(document);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Numero,Nombre,User_Id,Departament_Origen_Id,Departament_Destino_Id")] Document document)
        {
            if (ModelState.IsValid)
            {
                DocumentoNegocio.EditarDocumento(document);
                return RedirectToAction("Index");
            }
            ViewBag.Departament_Destino_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre", document.Departament_Destino_Id);
            ViewBag.Departament_Origen_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre", document.Departament_Origen_Id);
            ViewBag.User_Id = new SelectList(UsuarioNegocio.GetUsers(""), "Id", "Nombres", document.User_Id);
            ViewBag.Error = "Error al editar el Documento";
            return View(document);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var doc = DocumentoNegocio.GetDocumento(id.Value);
            if (doc == null)
            {
                return HttpNotFound();
            }
            return View(doc);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                DocumentoNegocio.EliminarDocumento(id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error al eliminar el documento");
                return View(id);
            }
        }
    }
}