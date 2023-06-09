using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VentaProductoController : Controller
    {
        public ActionResult Index()
        {
            ML.Result result = BL.Producto.GetAll();
            if (result.Correct)
            {
                ML.Producto producto = new ML.Producto(result.Objects);
                return View(producto);
            }
            else
            {
                ViewBag.Mensaje = result.ErrorMessage;
                return View();
            }

        }
    }
}
