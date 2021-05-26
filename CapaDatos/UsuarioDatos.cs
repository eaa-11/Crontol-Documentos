using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class UsuarioDatos
    {
        dbdocumentsEntities db = new dbdocumentsEntities();
        public void CreateUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public List<User> GetUsers(string filter)
        {
            return db.Users.Where(x => (
                x.Nombres.Contains(filter) ||
                x.Apellidos.Contains(filter) ||
                x.Cargo.Contains(filter) ||
                x.Departament.Nombre.Contains(filter)
            ) || filter == null).Include(a => a.Departament).ToList();
        }
        public User GetUser(int id)
        {
            return db.Users.Find(id);
        }
        public void EditarUsuario(User user)
        {
            var u = db.Users.Find(user.Id);
            u.Nombres = user.Nombres;
            u.Apellidos = user.Apellidos;
            u.Email = user.Email;
            u.Username = user.Username;
            u.Password = user.Password;
            u.Cargo = user.Cargo;
            u.Departamento_Id = user.Departamento_Id;
            db.SaveChanges();
        }
        public void EliminarUsuario(int id)
        {
            var d = db.Users.Find(id);
            db.Users.Remove(d);
            db.SaveChanges();
        }
        public User Login(string username, string password)
        {
            var usuario = db.Users.FirstOrDefault(x => ((x.Username.Equals(username) || x.Email.Equals(username)) && x.Password.Equals(password)));
            if (usuario != null)
            {
                return usuario;
            }
            return null;
        }
    }
}
