using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Mercer.Medicines.Logic;
using System.Web.Security;
//GAMM
using MMC.Seguridad.Utilerias; 

namespace TPA
{
    /// <summary>
    /// Modificación de contraseña
    /// </summary>
    public partial class AE_Contrasena : PB_PaginaBase
    {
        #region Attributes



        #endregion

        #region Initializing

        /// <summary>
        /// Inicialización
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                //Inicio MAHG 22/01/10
                //Se carga el load de la página base

                base.Page_Load(sender, e);

                //Fin MAHG 22/01/10

                if (!this.Page.IsPostBack)
                {
                    this.lblMensaje.Text = "";
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Método, realiza el cambio de contraseña del usuario
        /// </summary>
        /// <param name="p_idUser"></param>
        public bool ChangePasswordUser(int p_idUser)
        {
            #region Declaración de variables

            Users objUser;
            Security objSecurity;
            //Inicio MAHG Marco A. Herrera G. 21/01/10
            EmpresaDatos objEmpresa;
            EmpresaUsers objEmpresaUsers;
            bool bolPassword;//Identifica si el password ingresado es correcto
            //Fin MAHG Marco A. Herrera G. 21/01/10



            #endregion

            #region Inicialización de variables

            objUser = new Users();
            objSecurity = new Security();
            objUser.IdUser = p_idUser;
            //Inicio MAHG Marco A. Herrera G. 21/01/10
            objEmpresa = new EmpresaDatos();
            bolPassword = false;
            //Fin MAHG Marco A. Herrera G. 21/01/10

            #endregion

            try
            {
                //Consultar el usuario para verificar la contraseña actual digitada
                objUser.GetUsers();

                /*Inicio MAHG Marco A. Herrera Gabriel 21/01/10 
                 Se verifica si la contraseña esta encriptada con SHA256 ó MD5
                */
                if (objUser.NewPassword)
                {
                    bolPassword = Security.VerifyHash(this.txtActual.Text.Trim(), objUser.Password.Trim());
                }
                else
                {
                    bolPassword = (objUser.Password == objSecurity.EncryptString(this.txtActual.Text.Trim()));

                }
                //Fin MAHG Marco A. Herrera 21/01/10             

                /* Proyecto: AMEX
                 * Autor: Marco A. Herrera Gabriel
                 * Fecha: 22/02/10
                 * Funcionalidad: Se valida que el password temporal no se asigne más de una vez al usuario
                */
                #region Que el password temporal no sea asignado más de una vez

                objUser.Password = this.txtNueva.Text.Trim();
                if (objUser.PasswordInicial())
                {
                    DisplayMessage("Esta contraseña ya fue utilizada, por favor introduzca otra contraseña.");

                    return false;
                }

                #endregion

                if (objUser.IdUser != 0 && objUser.Active && !objUser.Blocking && bolPassword)
                {
                    objUser.NewPassword = true;//Bandera que indica si se ocupa la encriptacion SHA256
                    objUser.ExpiredPassword = false;
                    objUser.Password = Security.SHA256_Encrypt(this.txtNueva.Text.Trim(), null);
                    objUser.UpdateUsers(null, null, null);

                    //Inicio Marco A. Herrrera Gabriel MAHG 21/01/10
                    //Se restablece en cero los intentos de acceso al sistema
                    objUser.UpdateUsersAccess(0, null, null);
                    //Fin Marco A. Herrrera Gabriel MAHG 21/01/10                    


                    //Inicio MAHG 14/07/2010
                    //Se verifica si el usuario tiene sólo una empresa configurada

                    objEmpresaUsers = new EmpresaUsers();

                    objEmpresaUsers.IdUser = objUser.IdUser;
                    DataSet dsDatos = objEmpresaUsers.GetEmpresasUser();

                    if (dsDatos.Tables[0].Rows.Count == 1)
                    {
                        Session["Company"] = int.Parse(dsDatos.Tables[0].Rows[0]["empresa_id"].ToString());
                    }

                    //Fin 14/07/2010 MAHG

                    return true;
                }
                else
                {

                    /*Inicio 21/01/10 Marco A. Herrera Gabriel 
                     Se actualiza el intento de autenticación de acuerdo a la empresa.
                     */

                    if (objUser.Blocking)
                    {
                        DisplayMessage("Usted superó el número de intentos fallidos para acceder, su contraseña ha sido bloqueada, consulte con el administrador del sistema");
                    }
                    else
                    {
                        objEmpresaUsers = new EmpresaUsers();

                        objEmpresaUsers.IdUser = objUser.IdUser;
                        DataSet dsDatos = objEmpresaUsers.GetEmpresasUser();

                        if (dsDatos.Tables[0].Rows.Count <= 0)
                        {
                            DisplayMessage("No tienes permisos en ninguna empresa, por favor contacte al administrador");
                        }
                        else
                        {
                            objEmpresa.Empresa_id = int.Parse(dsDatos.Tables[0].Rows[0]["empresa_id"].ToString());
                            objEmpresa.GetEmpresaDatos();

                            if (objEmpresa.IntentosBloqueaPassword > 0)
                            {
                                //Se actualiza el intento fallido
                                objUser.AccessUnsuccessful++;
                                objUser.UpdateUsersAccess(objUser.AccessUnsuccessful, null, null);

                                //Se verifica si el usuario ha superado los intentos permitidos
                                if (objEmpresa.IntentosBloqueaPassword <= objUser.AccessUnsuccessful)
                                {
                                    objUser.Blocking = true;
                                    objUser.UpdateUsersAccess(null, null, objUser.Blocking);//Se bloquea el usuario                        

                                    DisplayMessage("Usted superó el número de intentos fallidos para acceder, su contraseña ha sido bloqueada, consulte con el administrador del sistema", "../AE_login_admin.aspx");
                                }
                                else
                                {
                                    DisplayMessage("La contraseña actual es incorrecta");
                                }
                            }
                        }

                        /*Fin 21/01/10 Marco A. Herrera Gabriel*/
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objUser = null;
            }
        }

        /// <summary>
        /// Proyecto: TPA-SICAM
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 20/01/10
        /// Funcionalidad: Valida los caracteres de los campos del formulario, los caracteres se consultan de la tabla 
        /// constantes, registro _CaracteresEspecialesLogin
        /// </summary>
        /// <returns>Booleano, Indica si encontro o no caracteres especiales en los campos</returns>
        private bool ValidarCaracteresEspeciales()
        {

            if (Validaciones.ExistenCaracteresEspeciales(txtActual.Text.Trim()))
            {
                return true;
            }
            //Inicio
            //Auto:Diego Montejano Avila
            //Proyecto: Auditoria 2014
            //Fecha: 2014/09/19
            //Observaciones:Se comentan las validaciones, se manejan validaciones con extensiones de funciones.

            /*
                        if (Validaciones.ExistenCaracteresEspeciales(txtNueva.Text.Trim()))
                        {
                            return true;
                        }

                        if (Validaciones.ExistenCaracteresEspeciales(txtConfirmacion.Text.Trim()))
                        {
                            return true;
                        }
                        */
            //Fin
            return false;
        }

        private bool ValidacionesFormularioCorrecto()
        {
            if (ValidarCaracteresEspeciales())
            {
                DisplayMessage("No se permite introducir caracteres especiales");
                return false;
            }
            /*
             if (this.txtNueva.Text.Length < 8)
             {
                 DisplayMessage("La contraseña debe contener mas de 7 caracteres");
                 return false;
             }

             if (this.txtActual.Text.Trim() == this.txtNueva.Text.Trim())
             {
                 DisplayMessage("La nueva contraseña debe ser diferente a la anterior");
                 return false;
             }*/

            //Inicio
            //Auto:Diego Montejano Avila
            //Proyecto: Auditoria 2014
            //Fecha: 2014/09/19
            //Observaciones:Se aplican validaciones para complejidad de password, que no ingresen los últimos 8 passwords.
            UsersPasswords validaUltimosPasswords = new UsersPasswords();
            validaUltimosPasswords.IdUser = Convert.ToInt32(Session["IdUser"]);
            if (validaUltimosPasswords.IdUser == 0)
            {
                Response.Write("<script language='javascript'>alert('El usuario no es valido.');</script>");
                return false;
            }
            else if (!this.ValidaContrasena(validaUltimosPasswords.IdUser.ToString(),
                         this.txtActual.Text, this.txtNueva.Text,
                         this.txtConfirmacion.Text))
                return false;
            else if (!this.ValidaContrasena(this.txtNueva.Text))
                return false;
            else if (validaUltimosPasswords.ValidaUltimosPasswords(this.txtNueva.Text))
            {
                Response.Write("<script language='javascript'>alert('La contraseña ya ha sido usada en sus últimos 8 cambios de contraseña.');</script>");
                return false;
            }
            //FIN
            return true;
        }

        #endregion

        #region Events

        #region Código generado por el Diseñador de Web Forms
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
            //
            InitializeComponent();
            //base.OnInit(e);
        }

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, realiza el lllamado al cambio de password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAceptar_Click(object sender, System.EventArgs e)
        {
            //try
            //{
                this.lblMensaje.Text = "";

                if (ValidacionesFormularioCorrecto())
                {
                    if (this.ChangePasswordUser(Convert.ToInt32(Session["IdUser"])))
                    {
                        this.RegisterLog(Log.EnumActionsLog.CambiarContraseña, Convert.ToInt32(Session["IdUser"]), "Id Usuario:" + Convert.ToInt32(Session["IdUser"]) + " Usuario:" + Session["NameUser"].ToString());

                    //GAMM Actulizamos sesión Activa
                    UpdateSession(Convert.ToInt32(Session["IdUser"].ToString()), Request.Cookies["{24618D5F-65A9-43cf-A40B-CB15DC3328DA}"].Value, 1);

                    //GAMM Regeneramos session
                    //AntiHack.RegenerarSessionId();

                    //GAMM. I. Redirecciona al Login
                    this.DisplayMessage("La contraseña se actualizo con exito");
                    Response.Redirect("AE_login_admin.aspx",false);
                    return;
                    //GAMM. F. Redirecciona al Login

                    //Inicio MAHG 14/07/2010 

                    object objIdEmpresa = Session["Company"];

                        if (objIdEmpresa != null)
                        {
                            //Inicio
                            //Autor:Diego Montejano Avila
                            //Proyecto:Auditoria 2014
                            //Fecha:23/09/2014
                            //Observaciones:Redirigir al disclaimer despues de logueo
                            //Response.Redirect("interfaz_empleado/forma/Home.aspx", true);
                            Session["urlRedirectDisclaimer"] = "interfaz_empleado/forma/Home.aspx";
                            Response.Redirect("AEDisclaimer.aspx", true);
                            return;
                            //Fin
                        }
                        else
                        {
                            //Inicio
                            //Autor:Diego Montejano Avila
                            //Proyecto:Auditoria 2014
                            //Fecha:23/09/2014
                            //Observaciones:Redirigir al disclaimer despues de logueo
                            //Response.Redirect("AE_Empresa.aspx", true);
                            Session["urlRedirectDisclaimer"] = "AE_Empresa.aspx";
                            Response.Redirect("AEDisclaimer.aspx", true);
                            return;
                            //Fin

                        }
                        //Fin MAHG 14/07/2010
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    this.DisplayMessage(ex.Message);
            //}
        }
        #endregion

    }
}
