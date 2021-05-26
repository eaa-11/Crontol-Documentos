using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class DepartamentoNegocio
    {
        private static DepartamentoDatos obj = new DepartamentoDatos();

        public static void CreateDepartment(Departament dep)
        {
            obj.CreateDepartment(dep);
        }
        public static List<Departament> GetDepartamanets(string filter)
        {
            return obj.GetDepartaments(filter);
        }
        public static Departament GetDepartament(int id)
        {
            return obj.GetDepartament(id);
        }
        public static void EditarDepartamento(Departament dep)
        {
            obj.EditarDepartamento(dep);
        }
        public static void EliminarDepartamento(int id)
        {
            obj.EliminarDepartamento(id);
        }
    }
}
