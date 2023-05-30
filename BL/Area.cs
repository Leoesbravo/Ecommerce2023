using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Area
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ProyectoEcommerce2023Context context = new DL.ProyectoEcommerce2023Context())
                {
                    var query = context.Areas.FromSqlRaw("AreaGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query.Count > 0)
                    {
                        foreach (var objArea in query)
                        {
                            ML.Area proveedor = new ML.Area(objArea.IdArea, objArea.Nombre);
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
