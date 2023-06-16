using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace PL.Controllers
{
    public class VentaProductoController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public VentaProductoController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
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

        //con Cache
        //public ActionResult AddProduct(int idProducto)
        //{
        //    // Obtén el carrito de compras desde la memoria cache
        //    List<object> productos;
        //    if (!_memoryCache.TryGetValue("Carrito", out productos))
        //    {
        //        // Si el carrito de compras no existe en la memoria cache, crea uno nuevo
        //        productos = new List<object>();
        //    }

        //    // Agrega el ID del producto al carrito
        //    productos.Add(idProducto);

        //    // Guarda el carrito actualizado en la memoria cache
        //    _memoryCache.Set("Carrito", productos);

        //    // Redirecciona a la página de carrito de compras o a donde desees
        //    return RedirectToAction("Carrito");
        //}
        //con Cookies
        public ActionResult Add(int idProducto)
        {

            ML.Producto producto = new ML.Producto();

            producto.Productos = new List<object>();

            if (Request.Cookies["Carrito"] != null)
            {
                var ventaSession = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(Request.Cookies["Carrito"]);

                foreach (var obj in ventaSession)
                {
                    ML.Producto producto1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());
                    producto.Productos.Add(producto1);
                }
            }
            ML.Result result = BL.Producto.GetById(idProducto);
            if (result.Correct)
            {
                producto.Productos.Add(result.Object);
            }

            string cookieValueUpdated = System.Text.Json.JsonSerializer.Serialize(producto.Productos);
            Response.Cookies.Append("Carrito", cookieValueUpdated, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            });

            return RedirectToAction("Index");
        }
        public ActionResult DeleteCarrito()
        {
            Response.Cookies.Delete("Carrito");
            return RedirectToAction("Index");
        }
    }
}
