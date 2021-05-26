using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class SecuenciaNegocio
    {
        public static SecuenciaDatos datos = new SecuenciaDatos(); 
        public static void CreateSecuencia(Secuencia Secuencia)
        {
            datos.CreateSecuencia(Secuencia);
        }
        public static List<Secuencia> GetSecuencias()
        {
            return datos.GetSecuencias();
        }
        public static Secuencia GetSecuencia(int id)
        {
            return datos.GetSecuencia(id);
        }
        public static Secuencia GetSecuencia()
        {
            return datos.GetSecuencia();
        }
        public static void EditarSecuencia(Secuencia Secuencia)
        {
            datos.EditarSecuencia(Secuencia);
        }
        public static void EliminarSecuencia(int id)
        {
            datos.EliminarSecuencia(id);
        }
    }
}
