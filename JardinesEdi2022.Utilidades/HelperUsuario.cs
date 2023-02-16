using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JardinesEdi2022.Datos;
using JardinesEdi2022.Datos.Repositorios;
using JardinesEdi2022.Entidades.Entidades;
using JardinesEdi2022.Servicios.Facades;
using JardinesEdi2022.Servicios.Servicios;

namespace JardinesEdi2022.Utilidades
{
    public static class HelperUsuario
    {
        private static IUsuariosServicios _servicios;
        public static string GenerarClave()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 6);
        }

        public static string ConvertirSha256(string texto)
        {
            SHA256 sha256 = SHA256Managed.Create();
            Encoding encoding = Encoding.UTF8;
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(texto));
            foreach (var t in stream)
                sb.AppendFormat("{0:x2}", t);

            return sb.ToString();

        }
        public static void CreateAdminUser()
        {
            using (var context = new ViveroSqlDbContext())
            {
                var repositorio = new UsuariosRepositorio(context);
                var unitOfWork = new UnitOfWork(context);
                var servicio = new UsuariosServicios(repositorio, unitOfWork);


                string clave = ConvertirSha256("Admin123.");
                var usuario = new Usuario()
                {
                    Correo = "admin@admin.com",
                    Nombre = "John",
                    Apellido = "Admin",
                    Activo = true,
                    Rol = WC.AdminRole,
                    Reestablecer = false,
                    Clave = clave

                };
                if (servicio.GetUsuarioByEmail(usuario.Correo) == null)
                {
                    servicio.Guardar(usuario);
                }
            }
        }

        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("catedrasinstituto432022@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("catedrasinstituto432022@gmail.com", "qsgjbllnxaapnvpx"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,

                };
                smtp.Send(mail);
                resultado = true;
            }
            catch (Exception e)
            {
                resultado = false;
            }

            return resultado;
        }

    }
}
