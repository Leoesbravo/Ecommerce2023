using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Proveedor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ProyectoEcommerce2023Context context = new DL.ProyectoEcommerce2023Context())
                {
                    var query = context.Proveedors.FromSqlRaw("ProveedorGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query.Count > 0)
                    {
                        foreach (var objProveedor in query)
                        {
                            ML.Proveedor proveedor = new ML.Proveedor(objProveedor.IdProveedor, objProveedor.Telefono, objProveedor.Nombre);                       
                            result.Objects.Add(proveedor);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
