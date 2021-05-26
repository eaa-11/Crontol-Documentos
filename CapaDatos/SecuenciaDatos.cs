using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class SecuenciaDatos
    {
        dbdocumentsEntities db = new dbdocumentsEntities();
        public void CreateSecuencia(Secuencia Secuencia)
        {
            db.Secuencias.Add(Secuencia);
            db.SaveChanges();
        }
        public List<Secuencia> GetSecuencias()
        {
            return db.Secuencias.ToList();
        }
        public Secuencia GetSecuencia(int id)
        {
            return db.Secuencias.Find(id);
        }
        public Secuencia GetSecuencia()
        {
            return db.Secuencias.FirstOrDefault();
        }
        public void EditarSecuencia(Secuencia Secuencia)
        {
            var s = db.Secuencias.Find(Secuencia.Id);
            s.Numero = Secuencia.Numero;
            db.SaveChanges();
        }
        public void EliminarSecuencia(int id)
        {
            var d = db.Secuencias.Find(id);
            db.Secuencias.Remove(d);
            db.SaveChanges();
        }
    }
}
