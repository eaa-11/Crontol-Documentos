using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class DepartamentoDatos
    {
        dbdocumentsEntities db = new dbdocumentsEntities();
        public void CreateDepartment(Departament dep)
        {
            db.Departaments.Add(dep);
            db.SaveChanges();
        }
        public List<Departament> GetDepartaments(string filter)
        {
            return db.Departaments.Where(x => (
                x.Nombre.Contains(filter)
            ) || filter == null).ToList();
        }
        public Departament GetDepartament(int id)
        {
            return db.Departaments.Find(id);
        }
        public void EditarDepartamento(Departament dep)
        {
            var dept = db.Departaments.Find(dep.Id);
            dept.Nombre = dep.Nombre;
            dept.Siglas = dep.Siglas;
            db.SaveChanges();
        }
        public void EliminarDepartamento(int id)
        {
            var dept = db.Departaments.Find(id);
            db.Departaments.Remove(dept);
            db.SaveChanges();
        }
    }
}
