using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;
using System.Web;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UsuarioController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Login(ML.Usuario usuario, string password)
        {
            // Crear una instancia del algoritmo de hash bcrypt
            var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);
            // Obtener el hash resultante para la contraseña ingresada 
            var passwordHash = bcrypt.GetBytes(20);

            if (usuario.UserName != null)
            {
                // Insertar usuario en la base de datos
                usuario.Password = passwordHash;
                ML.Result result = BL.Usuario.Add(usuario);
                return View();
            }
            else
            {
                // Proceso de login
                ML.Result result = BL.Usuario.GetByEmail(usuario.Email);
                usuario = (ML.Usuario)result.Object;

                if (usuario.Password.SequenceEqual(passwordHash))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult CambiarPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CambiarPassword(string email)
        {
            //llamar al metodo
            string emailOrigen = "leoebravo@gmail.com";

            MailMessage mailMessage = new MailMessage(emailOrigen, email, "Recuperar Contraseña", "<p>Correo para recuperar contraseña</p>");
            mailMessage.IsBodyHtml = true;

            //string contenidoHTML = System.IO.File.ReadAllText(@"C:\users\digis\Documents\IISExpress\Leonardo Escogido Bravo\Proyecto2023Ecommerce\PL\Views\Usuario\Email.html");

            //string contenidoHTML = System.IO.File.ReadAllText(Path.Combine("Views", "Usuario", "Email.html"));


            string contenidoHTML = System.IO.File.ReadAllText(Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Templates", "Email.html"));




            mailMessage.Body = contenidoHTML;
            //string url = "http://localhost:5057/Usuario/NewPassword/" + HttpUtility.UrlEncode(email);
            string url = "http://192.168.0.104/Usuario/NewPassword/" + HttpUtility.UrlEncode(email);
            mailMessage.Body = mailMessage.Body.Replace("{Url}", url);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, "kuwzpcgwbrmueviw");

            smtpClient.Send(mailMessage);
            smtpClient.Dispose();

            ViewBag.Modal = "show";
            ViewBag.Mensaje = "Se ha enviado un correo de confirmación a tu correo electronico";
            return View();
        }
        [HttpGet]
        public ActionResult NewPassword(string email)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Email = email;
            return View(usuario);
        }
        [HttpPost]
        public ActionResult NewPassword(string password, string email)
        {
            return View();
        }
    }
}
