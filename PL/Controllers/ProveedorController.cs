using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProveedorController : Controller
    {
        public ActionResult Index()
        {
            ML.Result result = BL.Proveedor.GetAll();
            ML.Proveedor proveedor = new ML.Proveedor(result.Objects);
            return View(proveedor);
        }
    }
}
