using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaNegocio
{
    public class DocumentoNegocio
    {
        private static DocumentoDatos obj = new DocumentoDatos();

        public static void CreateDocuments(Document doc)
        {
            obj.CreateDocuments(doc);
        }
        public static List<Document> GetDocuments()
        {
            return obj.GetDocuments();
        }
        public static List<Document> GetDocumentsByUser(int? id)
        {
            return obj.GetDocumentsByUser(id);
        }
        public static List<Document> GetDocumentsByOrigin(int? id)
        {
            return obj.GetDocumentsByOrigin(id);
        }
        public static List<Document> GetDocumentsByDestination(int? id)
        {
            return obj.GetDocumentsByDestination(id);
        }
        public static List<Document> GetDocumentsByDate(DateTime? fechainicio, DateTime? fechafinal)
        {
            return obj.GetDocumentsByDate(fechainicio, fechafinal);
        }
        public static Document GetDocumento(int id)
        {
            return obj.GetDocumento(id);
        }
        public static void EditarDocumento(Document doc)
        {
            obj.EditarDocumento(doc);
        }
        public static void EliminarDocumento(int id)
        {
            obj.EliminarDocumento(id);
        }
    }
}
