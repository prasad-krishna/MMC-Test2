using System;
using System.Collections.Generic;
using System.Web;

namespace Mercer.Tpa.Agenda.Web.Sistema
{
    /// <summary>
    /// Fachada para el acceso a las variables de sesión
    /// </summary>
    public class SessionManager
    {
        /// <summary>
        /// Retorna el id de la empreesa.
        /// Esta variable se asigna en la pantalla de logueo del sistema.
        /// Retorna -1 Si no se encuentra la variable
        /// </summary>
        public static int IdEmpresa
        {

            get
            {
                if (Testing)
                    return 1;

                if (HttpContext.Current.Session["Company"] == null)
                    return -1;
                return Convert.ToInt32(HttpContext.Current.Session["Company"]);

            }
        }

        private static bool _testing=false;

        /// <summary>
        /// Flag util para poder hacer pruebas unitarias que usen sesión.
        /// </summary>
        public static bool Testing
        {
            get { return _testing; }
            set { _testing = value; }
        }

        /// <summary>
        /// Retorna el id del usuario.
        /// Esta variable se asigna en la pantalla de logueo del sistema.
        /// Retorna -1 Si no se encuentra la variable
        /// </summary>
        public static int IdUser
        {

            get
            {
                if (Testing)
                    return 1;
                if (HttpContext.Current.Session["IdUser"] == null)
                    return -1;
                return Convert.ToInt32(HttpContext.Current.Session["IdUser"]);

            }
        }

        /// <summary>
        /// Retorna un bool indicando si el usuario actual es administrador.
        /// (La convención de utilizar "S" ya existía en el sistema, asi que tenemos que seguir utilizandola
        /// </summary>
        public static bool EsAdministrador
        {
            get
            {
                if (Testing)
                    return true;

                if (HttpContext.Current.Session["Administrador"] == null || HttpContext.Current.Session["Administrador"].ToString() != "S")
                    return false;
                return true;
            }


        }

        public static string NombreUsuario
        {
            get
            {
                return HttpContext.Current.Session["NameUser"] == null ? string.Empty : HttpContext.Current.Session["NameUser"].ToString();
            }
        }

        /// <summary>
        /// Retorna el id del prestador o -1 si no es prestador
        /// </summary>
        public static int IdPrestador
        {
            get
            {
                if (HttpContext.Current.Session["IdPrestador"] == null)
                    return -1;

                try
                {
                    return  Convert.ToInt32(HttpContext.Current.Session["IdPrestador"]);

                }
                catch (Exception exception)
                {
                    return -1;
                }
            }
        }
    }
}
