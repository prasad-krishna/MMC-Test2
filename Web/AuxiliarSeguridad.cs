using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Mercer.Medicines.Logic;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

//Este código se ocupara para el framework 2.0 para que puedan emplearse las extensiones de metodos.
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class
         | AttributeTargets.Method)]
    public sealed class ExtensionAttribute : Attribute { }
}
//Este código se ocupara para el framework 2.0 para que puedan emplearse las extensiones de metodos.
namespace TPA
{


    public static class AuxiliarSeguridad
    {
        #region Variables

        #endregion


        //static string caracteresValidos = @".|,|:|;|(|)|[|]|{|}|?|!|¿|¡|@|-|/|\|#";
        /// <summary>
        /// Autor: Diego Montejano Avila
        /// Fecha: 2014/09/18
        /// Proyecto:Auditoria seguridad 2014
        /// Validar al usuario
        /// </summary>
        /// <param name="pg">Página que invoca el método</param>
        /// <param name="usuario">usuario que se validara</param>
        /// <returns></returns>
        public static bool ValidaUsuario(
                   this Page pg,
                   string usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario))
                {
                    pg.Response.Write("<script language='javascript'>alert('El usuario es un dato requerido.');</script>");
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return true;
        }

        /// <summary>
        /// Autor: Diego Montejano Avila
        /// Fecha: 2014/09/18
        /// Proyecto:Auditoria seguridad 2014
        /// Valida que las contraseñas no sean vacias, que no sean iguales a la contraseña anterior y que la nueva contraseña no sea igual al usuario
        /// </summary>
        /// <param name="pg">página que invoca el método</param>
        /// <param name="usuario">usuario que requiere el cambio de contraseña</param>
        /// <param name="contrasena">contraseña anterior</param>
        /// <param name="contrasenaNueva">contraseña nueva</param>
        /// <param name="contrasenaNuevaConfirmacion">confirmación de contraseña</param>
        /// <returns>false o true</returns>
        public static bool ValidaContrasena(
            this Page pg,
            string usuario,
            string contrasena,
            string contrasenaNueva,
            string contrasenaNuevaConfirmacion)
        {
            try
            {
                //Valida que el usuario no este vacio
                if (string.IsNullOrEmpty(usuario))
                {
                    pg.Response.Write("<script language='javascript'>alert('El usuario es un dato requerido.');</script>");
                    return false;
                }
                //Valida que la contraseña anterior no este vacia
                else if (string.IsNullOrEmpty(contrasena))
                {
                    pg.Response.Write("<script language='javascript'>alert('La contraseña anterior es un dato requerido.');</script>");
                    return false;
                }
                //Valida que la nueva contraseña no este vacia
                else if (string.IsNullOrEmpty(contrasenaNueva))
                {
                    pg.Response.Write("<script language='javascript'>alert('La contraseña nueva es un dato requerido.');</script>");
                    return false;
                }
                //Valida que la confirmación de la nueva contraseña no este vacia
                else if (string.IsNullOrEmpty(contrasenaNuevaConfirmacion))
                {
                    pg.Response.Write("<script language='javascript'>alert('La confirmación de la contraseña es un dato requerido.');</script>");
                    return false;
                }
                //Valida que el usuario no sea igual a la nueva contraseña
                else if (usuario.Equals(contrasenaNueva))
                {
                    pg.Response.Write("<script language='javascript'>alert('El usuario y la contraseña nueva no pueden ser iguales.');</script>");
                    return false;
                }
                //Valida que la contraseña anterior y la nueva contraseña no sean iguales
                else if (contrasena.Equals(contrasenaNueva))
                {
                    pg.Response.Write("<script language='javascript'>alert('La contraseña nueva no puede ser igual a la contraseña anterior.');</script>");
                    return false;
                }
                //Valida que la nueva contraseña y su confirmación sean iguales
                else if (!contrasenaNueva.Equals(contrasenaNuevaConfirmacion))
                {
                    pg.Response.Write("<script language='javascript'>alert('La contraseña nueva y la confirmación no coinciden.');</script>");
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return true;
        }
        /// <summary>
        /// Autor: Diego Montejano Avila
        /// Fecha: 2014/09/18
        /// Proyecto:Auditoria seguridad 2014
        /// Valida que las contraseñas no sean vacias, que no sean iguales a la contraseña anterior y que la nueva contraseña no sea igual al usuario
        /// </summary>
        /// <param name="pg">página que invoca el método</param>
        /// <param name="usuario">usuario que requiere el cambio de contraseña</param>
        /// <param name="contrasena">contraseña anterior</param>
        /// <param name="contrasenaNueva">contraseña nueva</param>
        /// <param name="contrasenaNuevaConfirmacion">confirmación de contraseña</param>
        /// <returns>false o true</returns>
        public static bool ValidaContrasena(
            this Page pg,
            string usuario,
            string contrasenaNueva,
            string contrasenaNuevaConfirmacion)
        {
            try
            {
                //Valida que el usuario no este vacio
                if (string.IsNullOrEmpty(usuario))
                {
                    pg.Response.Write("<script language='javascript'>alert('El usuario es un dato requerido.');</script>");
                    return false;
                }
                //Valida que la nueva contraseña no este vacia
                else if (string.IsNullOrEmpty(contrasenaNueva))
                {
                    pg.Response.Write("<script language='javascript'>alert('La contraseña nueva es un dato requerido.');</script>");
                    return false;
                }
                //Valida que la confirmación de la nueva contraseña no este vacia
                else if (string.IsNullOrEmpty(contrasenaNuevaConfirmacion))
                {
                    pg.Response.Write("<script language='javascript'>alert('La confirmación de la contraseña es un dato requerido.');</script>");
                    return false;
                }
                //Valida que el usuario no sea igual a la nueva contraseña
                else if (usuario.Equals(contrasenaNueva))
                {
                    pg.Response.Write("<script language='javascript'>alert('El usuario y la contraseña nueva no pueden ser iguales.');</script>");
                    return false;
                }
                //Valida que la nueva contraseña y su confirmación sean iguales
                else if (!contrasenaNueva.Equals(contrasenaNuevaConfirmacion))
                {
                    pg.Response.Write("<script language='javascript'>alert('La contraseña nueva y la confirmación no coinciden.');</script>");
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return true;
        }

        /// <summary>
        /// Autor: Diego Montejano Avila
        /// Fecha: 2014/08/15
        /// Proyecto:Auditoria seguridad 2014
        /// Valida los caracteres de entrada para una contraseña 
        /// </summary>
        /// <param name="pg">página que invoca el método</param>
        /// <param name="contrasena">contraseña que será evaluada</param>
        /// <returns>false o true</returns>
        public static bool ValidaContrasena(this Page pg, string contrasena)
        {

            try
            {
                CaracteresObligatoriosPassword caracteresPassword = new CaracteresObligatoriosPassword();
                string caracteresValidos = "";
                string expCaracteresValidos = "";
                caracteresPassword.ObtenerCaracteresValidos = true;

                caracteresValidos = caracteresPassword.ConsultaCaracteresObligatoriosPassword().Tables[0].Rows[0][0].ToString();

                expCaracteresValidos = "(?=.*[" + caracteresValidos.Replace("|", "\\") + "])";

                Regex regex = new Regex("(?=^.{8,}$)"
                    + (expCaracteresValidos == null || expCaracteresValidos == "" ? "" : expCaracteresValidos)
                    + "(?=.*\\d)(?=.*[A-Z])(?=.*[a-z]).*$");

                if (CleanInput(contrasena.Trim()) != contrasena.Trim())
                {
                    pg.Response.Write("<script language='javascript'>alert('La nueva contraseña contiene caracteres no validos.');</script>");
                    return false;
                }
                if (!regex.IsMatch(contrasena.Trim()))
                {
                    pg.Response.Write(@"<script language='javascript'>alert('La nueva contraseña debe tener mínimo 15 y máximo 32 caracteres, por lo menos una letra mayúscula y una minúscula, un número y al menos uno de los siguientes caracteres especiales:.,:;()[]{}?!¿¡@-_/\#.');</script>");
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        /// <summary>
        /// Autor: Diego Montejano Avila
        /// Fecha: 2014/08/15
        /// Proyecto:Auditoria seguridad 2014
        /// Quita los caracteres invalidos tomando como base los caracteres válidos definidos
        /// </summary>
        /// <param name="strIn">cadena que sera evaluada</param>
        /// <returns>cadena sin caracteres invalidos</returns>
        static string CleanInput(string strIn)
        {
            CaracteresObligatoriosPassword caracteresPassword = new CaracteresObligatoriosPassword();
            string caracteresValidos = "";

            caracteresPassword.ObtenerCaracteresValidos = true;
            caracteresValidos = caracteresPassword.ConsultaCaracteresObligatoriosPassword().Tables[0].Rows[0][0].ToString();

            return Regex.Replace(strIn, @"[^\w" + caracteresValidos.Replace("|", "\\") + "]", "");
        }
        /// <summary>
        ///Auto:Diego Montejano Avila
        ///Proyecto: Auditoria 2014
        ///Fecha: 2014/09/19
        /// Genera una contraseña aleatoria en base a los caracteres que estan en base de datos.
        /// </summary>
        /// <returns></returns>
        public static string GeneraContrasena()
        {
            try
            {
                #region Variables

                Random obj = new Random();
                //Variable auxiliar en caso de que base de datos no regrese ningún registro de configuración.
                string posibles = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                //Objeto para obtener la configuración de caracteres permitidos y caracteres para formar una nueva contraseña.
                CaracteresObligatoriosPassword caracteresPassword = new CaracteresObligatoriosPassword();
                //Tabla para almacenar la configuración de caracteres para formar la nueva contraseña.
                DataTable dtCaracteresPassword = caracteresPassword.ConsultaCaracteresObligatoriosPassword().Tables[0];
                //Total de registros que contienen los caracteres para formar la nueva contraseña.
                int totListCarPass = dtCaracteresPassword.Rows.Count - 1;
                //Contador de ayuda para saber si ya tomo un caracter de cada renglon de la tabla de caracteres para formar la nueva contraseña
                int contListCarPass = 0;
                //Variable para guardar la posicion del renglon aleatorio cuando ya se tomo un caracter de cada renglon de la tabla de caracteres para formar una nueva contraseña
                int posListCarPassAleatoria = 0;
                //Variable para almacenar la longitud de la variable axuliar en caso de que no se configure en base de datos los caracteres validos
                int longitud = posibles.Length;
                //Letra aleatoria
                char letra;
                //Longitud de la nueva contraseña
                int longitudnuevacadena = 15;
                //Nueva contraseña
                string nuevacadena = "";
                #endregion
                for (int i = 0; i < longitudnuevacadena; i++)
                {
                    //Si existe configuración de caracteres validos en la base de datos los tomamos sino se toma el default.
                    if (totListCarPass > -1)
                    {
                        //Si ya se tomo un caracter de cada renglon de la tabla de caracteres validos para formar la nueva contraseña se toma un reglon aleatorio
                        if (contListCarPass <= totListCarPass)
                            letra = dtCaracteresPassword.Rows[contListCarPass][0].ToString()[obj.Next(dtCaracteresPassword.Rows[contListCarPass][0].ToString().Length)];
                        else
                        {
                            posListCarPassAleatoria = obj.Next(dtCaracteresPassword.Rows.Count);
                            letra = dtCaracteresPassword.Rows[posListCarPassAleatoria][0].ToString()[obj.Next(dtCaracteresPassword.Rows[posListCarPassAleatoria][0].ToString().Length)];
                        }
                    }
                    else
                        letra = posibles[obj.Next(longitud)];
                    //concatena la letra obtenida aleatoriamente
                    nuevacadena += letra.ToString();
                    contListCarPass++;
                }
                return nuevacadena;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Valida BlackList dentro de una cadena de Texto .
        /// </summary>
        /// /// <param name="texto">Texto a validar</param>
        /// <param name="cadenaConexion">Cadena de conexión</param>
        /// <param name="sql">Instrucción SQL</param>        
        public static bool ValidarTexto(string texto, string cadenaConexion, string Sql)
        {
            List<string> BlackList = new List<string>();

            //Recuperamos Lista a través de base de datos
            BlackList = RecuperarDatos(cadenaConexion, Sql);

            bool existeValor = false;

            //Recorremos la lista negra
            foreach (string item in BlackList)
            {
                existeValor = texto.Contains(item);
                //Regresamos a la primera coincidencia.
                if (existeValor)
                {
                    return existeValor;
                }
            }
            //Recorre todos los valores
            return existeValor;
        }

        /// <summary>
        /// Recupera BlackList (base de datos).
        /// </summary>
        /// <param name="connectionString">Cadena de conexión</param>
        /// <param name="sql">Instrucción SQL</param>        
        private static List<string> RecuperarDatos(string connectionString, string sql)
        {

            SqlConnection connection;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();

            connection = new SqlConnection(connectionString);

            //List
            List<string> BlackList = new List<string>();

            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);

                adapter.Dispose();
                command.Dispose();

                BlackList = ds.Tables[0].AsEnumerable()
                               .Select(r => r.Field<string>("Elemento"))
                               .ToList();


                return BlackList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool GrabaLog(string connectionString, string SesId, string SesionBorrar, string UrlReferrerHost, string RUA, string RURQ, string Dir, string aspx, string RQS, string valWizard)
        {

            SqlConnection connection;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();

            string sSQL;

            sSQL = "Execute dbo.spSELGlobalValidaEntrada " +
                                            "'" + SesId + "'," +
                                            "'" + SesionBorrar + "'," +
                                            "'" + UrlReferrerHost + "'," +
                                            "'" + RUA + "'," +
                                            "'" + RURQ + "'," +
                                            "'" + Dir + "'," +
                                            "'" + aspx + "'," +
                                            "'" + RQS + "'," +
                                            "'" + valWizard + "'";
            

            connection = new SqlConnection(connectionString);

            DataTable dt = new DataTable();
            bool val = false;

            try
            {
                connection.Open();
                command = new SqlCommand(sSQL, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);

                adapter.Dispose();
                command.Dispose();

                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {

                    val = Convert.ToBoolean(ds.Tables[0].Rows[0].ItemArray[0]);
                }

                return val;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

    }


    
}