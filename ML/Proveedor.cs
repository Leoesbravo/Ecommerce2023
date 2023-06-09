using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Proveedor
    {
        public Proveedor(int idProveedor, string telefono, string nombre)
        {
            IdProveedor = idProveedor;
            Telefono = telefono;
            Nombre = nombre;
        }
        public Proveedor()
        {

        }
        public Proveedor(List<object> proveedores)
        {
            Proveedores = proveedores;
        }
        public int IdProveedor { get; set; }
        public string Telefono { get; set; }
        public string  Nombre { get; set; }
        public List<object> Proveedores { get; set; }
    }
}
