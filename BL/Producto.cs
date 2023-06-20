using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ProyectoEcommerce2023Context context = new DL.ProyectoEcommerce2023Context())
                {
                    var query = context.Productos.FromSqlRaw("ProductoGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query.Count > 0)
                    {
                        foreach (var objProducto in query)
                        {
                            ML.Producto producto = new ML.Producto();

                            producto.IdProducto = objProducto.IdProducto;
                            producto.Nombre = objProducto.Nombre;
                            producto.PrecioUnitario = objProducto.PrecioUnitario;
                            producto.Stock = objProducto.Stock;
                            producto.Descripcion = objProducto.Descripcion;

                            producto.Proveedor = new ML.Proveedor();
                            objProducto.IdProveedorNavigation = new DL.Proveedor();

                            producto.Proveedor.IdProveedor = objProducto.IdProveedor.Value;
                            producto.Proveedor.Nombre = objProducto.IdProveedorNavigation.Nombre;

                            producto.Departamento = new ML.Departamento();
                            objProducto.IdDepartamentoNavigation = new DL.Departamento();

                            producto.Departamento.IdDepartamento = objProducto.IdDepartamento.Value;
                            producto.Departamento.Nombre = objProducto.IdDepartamentoNavigation.Nombre;

                            result.Objects.Add(producto);
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
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ProyectoEcommerce2023Context context = new DL.ProyectoEcommerce2023Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.Nombre}', {producto.PrecioUnitario}, {producto.Stock}, '{producto.Descripcion}', {producto.Proveedor.IdProveedor}, {producto.Departamento.IdDepartamento}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ProyectoEcommerce2023Context context = new DL.ProyectoEcommerce2023Context())
                {
                    var query = context.Productos.FromSqlRaw($"ProductoGetById {idProducto}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        ML.Producto producto = new ML.Producto();

                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.PrecioUnitario = query.PrecioUnitario;
                        producto.Stock = query.Stock;
                        producto.Descripcion = query.Descripcion;

                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = query.IdProveedor.Value;

                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = query.IdDepartamento.Value;

                        result.Object = producto;

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
        //public static File GenerarPDF()
        //{

        //}
    }
}