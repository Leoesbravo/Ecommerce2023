using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        public ActionResult Index()
        {
            ML.Result result = BL.Producto.GetAll();

            ML.Producto producto = new ML.Producto();
            producto = CargarFormulario();
            producto.Productos = result.Objects;
            return View(producto);
        }
        [HttpPost]
        public ActionResult Index(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            if(producto.IdProducto == 0)
            {
                result = BL.Producto.Add(producto);
                if (result.Correct)
                {
                    TempData["alert"] = "alert-success";
                    TempData["modal"] = "show";
                    TempData["mensaje"] = "Se ha completado el registro del producto " + producto.Nombre + "en la base de datos.";
                    return View();
                    //return PartialView("Modal");
                }   
                else
                {
                    ML.Result resultAreas = BL.Area.GetAll();
                    ML.Result resultProveedor = BL.Proveedor.GetAll();
                    ML.Result resultProducto = BL.Producto.GetAll();
                    producto.Proveedor = new ML.Proveedor();
                    producto.Departamento = new ML.Departamento();
                    producto.Departamento.Area = new ML.Area();

                    producto.Productos = resultProducto.Objects;
                    producto.Proveedor.Proveedores = resultProveedor.Objects;
                    producto.Departamento.Area.Areas = resultAreas.Objects;
                    TempData["value"] = "alert-warning";
                    TempData["modal"] = "show";
                    TempData["mensaje"] = "Ocurrio un error al intentar registrar el producto " + producto.Nombre + "en la base de datos: " + result.ErrorMessage;
                    return View(producto);
                }
            }
            return View();

        }
        public static ML.Producto CargarFormulario()
        {
            ML.Result resultAreas = BL.Area.GetAll();
            ML.Result resultProveedor = BL.Proveedor.GetAll();

            ML.Producto producto = new ML.Producto();
            producto.Proveedor = new ML.Proveedor();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();

            producto.Proveedor.Proveedores = resultProveedor.Objects;
            producto.Departamento.Area.Areas = resultAreas.Objects;
            return producto;
        }
    }
}
