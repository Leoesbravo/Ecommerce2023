using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ProyectoEcommerce2023Context context = new DL.ProyectoEcommerce2023Context())
                {
                    var obj = context.Usuarios.FromSqlRaw($"UsuarioGetById {idUsuario}").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = obj.IdUsuario;
                        usuario.Nombre = obj.Nombre;
                        usuario.ApellidoPaterno = obj.ApellidoPaterno;
                        usuario.ApellidoMaterno = obj.ApellidoMaterno;
                        usuario.UserName = obj.Username;
                        usuario.Email = obj.Email;
                        usuario.Password = obj.Password;

                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo realizar la consulta";
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
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ProyectoEcommerce2023Context context = new DL.ProyectoEcommerce2023Context())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.UserName}', '{usuario.Email}', @Password",new SqlParameter("@Password", usuario.Password));

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
        public static ML.Result GetByEmail(string email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ProyectoEcommerce2023Context context = new DL.ProyectoEcommerce2023Context())
                {
                    var obj = context.Usuarios.FromSqlRaw($"UsuarioGetByEmail '{email}'").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = obj.IdUsuario;
                        usuario.Nombre = obj.Nombre;
                        usuario.ApellidoPaterno = obj.ApellidoPaterno;
                        usuario.ApellidoMaterno = obj.ApellidoMaterno;
                        usuario.UserName = obj.Username;
                        usuario.Email = obj.Email;
                        usuario.Password = obj.Password;

                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo realizar la consulta";
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

    }
}
