using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Result resultAreas = BL.Area.GetAll();
            ML.Result resultProveedor = BL.Proveedor.GetAll();
            ML.Result result = BL.Producto.GetAll();

            ML.Producto producto = new ML.Producto();
            producto.Proveedor = new ML.Proveedor();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();

            producto.Productos = result.Objects;
            producto.Proveedor.Proveedores = resultProveedor.Objects;
            producto.Departamento.Area.Areas = resultAreas.Objects;
            return View(producto);
        }
        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            if(producto.IdProducto == 0)
            {
                result = BL.Producto.Add(producto);
                if (result.Correct)
                {
                    TempData["value"] = "alert-warning";
                    return RedirectToAction("GetAll");
                    //return PartialView("Modal");
                }   
                else
                {
                    TempData["value"] = "alert-warning";
                    return RedirectToAction("GetAll");
                }
            }
            return View();

        }
    }
}
