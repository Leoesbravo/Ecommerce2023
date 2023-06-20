using Microsoft.AspNetCore.Mvc;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

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
        public ActionResult Reporte()
        {
            ML.Result result = BL.Producto.GetAll();

            // Crear un nuevo documento PDF en una ubicación temporal
            string rutaTempPDF = Path.GetTempFileName() + ".pdf";
            PdfDocument document = new PdfDocument(new PdfWriter(rutaTempPDF));

            // Crear un objeto de layout para agregar contenido al documento
            Document doc = new Document(document);

            doc.Add(new Paragraph("Lista de Productos registrados"));

            // Crear la tabla para mostrar la lista de objetos
            Table table = new Table(5); // 5 columnas

            // Añadir las celdas de encabezado a la tabla
            table.AddHeaderCell("ID Producto");
            table.AddHeaderCell("Producto");
            table.AddHeaderCell("Descripción");
            table.AddHeaderCell("Precio Unitario");
            table.AddHeaderCell("Stock");

            // Añadir filas a la tabla con los datos de la lista de objetos
            foreach (ML.Producto producto in result.Objects)
            {
                table.AddCell(producto.IdProducto.ToString());
                table.AddCell(producto.Nombre);
                table.AddCell(producto.Descripcion);
                table.AddCell(producto.PrecioUnitario.ToString());
                table.AddCell(producto.Stock.ToString());
            }

            // Añadir la tabla al documento
            if (doc != null && table != null)
            {
                doc.Add(table);

                // Cerrar el documento
                doc.Close();
            }

            // Obtener el contenido del archivo PDF como un arreglo de bytes
            byte[] fileBytes = System.IO.File.ReadAllBytes(rutaTempPDF);

            // Eliminar el archivo temporal
            System.IO.File.Delete(rutaTempPDF);

            // Descargar el archivo PDF
            return new FileStreamResult(new MemoryStream(fileBytes), "application/pdf")
            {
                FileDownloadName = "ReporteProductoss.pdf"
            };

        }
    }
}
