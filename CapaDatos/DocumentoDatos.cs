using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class DocumentoDatos
    {
        dbdocumentsEntities db = new dbdocumentsEntities();
        public void CreateDocuments(Document doc)
        {
            db.Documents.Add(doc);
            db.SaveChanges();
        }
        public List<Document> GetDocuments()
        {
            return db.Documents.Include(a => a.User).Include(a => a.Departament).Include(a => a.Departament1).ToList();
        }
        public List<Document> GetDocumentsByUser(int? id)
        {
            return db.Documents.Where(x => (x.User_Id == id)).Include(a => a.User).Include(a => a.Departament).Include(a => a.Departament1).ToList();
        }
        public List<Document> GetDocumentsByOrigin(int? id)
        {
            return db.Documents.Where(x => (x.Departament_Origen_Id == id)).Include(a => a.User).Include(a => a.Departament).Include(a => a.Departament1).ToList();
        }
        public List<Document> GetDocumentsByDestination(int? id)
        {
            return db.Documents.Where(x => (x.Departament_Destino_Id == id)).Include(a => a.User).Include(a => a.Departament).Include(a => a.Departament1).ToList();
        }
        public List<Document> GetDocumentsByDate(DateTime? fechainicio, DateTime? fechafinal)
        {
            return db.Documents.Where(x => (x.Fecha >= fechainicio && x.Fecha <= fechafinal)).Include(a => a.User).Include(a => a.Departament).Include(a => a.Departament1).ToList();
        }
        public Document GetDocumento(int id)
        {
            return db.Documents.Find(id);
        }
        public void EditarDocumento(Document doc)
        {
            var documento = db.Documents.Find(doc.Id);
            documento.Numero = doc.Numero;
            documento.Nombre = doc.Nombre;
            documento.User_Id = doc.User_Id;
            documento.Departament_Origen_Id = doc.Departament_Origen_Id;
            documento.Departament_Destino_Id = doc.Departament_Destino_Id;
            db.SaveChanges();
        }
        public void EliminarDocumento(int id)
        {
            var d = db.Documents.Find(id);
            db.Documents.Remove(d);
            db.SaveChanges();
        }
    }
}
