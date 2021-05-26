using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocio
{
    public class UsuarioNegocio
    {
        private static UsuarioDatos obj = new UsuarioDatos();

        public static  void CreateUser(User user)
        {
            obj.CreateUser(user);
        }
        public static List<User> GetUsers(string filter)
        {
            return obj.GetUsers(filter);
        }
        public static User GetUser(int id)
        {
            return obj.GetUser(id);
        }
        public static void EditarUsuario(User user)
        {
            obj.EditarUsuario(user);
        }
        public static void EliminarUsuario(int id)
        {
            obj.EliminarUsuario(id);
        }
        public static User Login(string username, string password)
        {
            return obj.Login(username, password);
        }
    }
}
