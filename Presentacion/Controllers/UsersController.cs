using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CapaEntidad;
using CapaNegocio;
using PagedList;
using Presentacion.Models;

namespace Presentacion.Controllers
{
    public class UsersController : Controller
    {
        [Authorize]
        // GET: Users
        public ActionResult Index(string filter, int? i)
        {
            return View(UsuarioNegocio.GetUsers(filter).ToPagedList(i ?? 1, 10));
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            User user = UsuarioNegocio.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Departamento_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre");
            return View();
        }

        // POST: Users/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombres,Apellidos,Email,Username,Password,Cargo,Departamento_Id")] User user)
        {
            if (ModelState.IsValid)
            {
                UsuarioNegocio.CreateUser(user);
                return RedirectToAction("Index");
            }
            ViewBag.Departamento_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre", user.Departamento_Id);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            User user = UsuarioNegocio.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Departamento_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre", user.Departamento_Id);
            return View(user);
        }

        // POST: Users/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombres,Apellidos,Email,Username,Password,Cargo,Departamento_Id")] User user)
        {
            if (ModelState.IsValid)
            {
                UsuarioNegocio.EditarUsuario(user);
                return RedirectToAction("Index");
            }
            ViewBag.Departamento_Id = new SelectList(DepartamentoNegocio.GetDepartamanets(""), "Id", "Nombre", user.Departamento_Id);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            User user = UsuarioNegocio.GetUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuarioNegocio.EliminarUsuario(id);
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var usuario = UsuarioNegocio.Login(username, password);
                if (usuario != null)
                {
                    
                    FormsAuthentication.SetAuthCookie(usuario.Username, true);
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Error = "Las credenciales no existen o no coinciden";
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
