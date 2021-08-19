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

namespace TPA.interfaz_empleado.forma
{
    /// <summary>
    /// Realiza el cambio de contraseña del usuario actual
    /// </summary>
    /// <remarks>Autor: Adriana Diazgranados</remarks>
    /// <remarks>Fecha de creación: 6 de Octubre de 2008</remarks>
    public partial class AE_password : PB_PaginaBase
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
        public void ChangePasswordUser(int p_idUser)
        {
            Users objUser;
            Security objSecurity;

            //Inicio MAHG Marco A. Herrera G. 19/01/10
            //Identifica si el password ingresado es correcto
            bool bolPassword = true;

            //Fin MAHG Marco A. Herrera G. 19/01/10
            try
            {
                //Consultar el usuario para verificar la contraseña actual digitada
                objUser = new Users();
                objSecurity = new Security();
                objUser.IdUser = p_idUser;

                /*Inicio MAHG Marco A. Herrera Gabriel 19/01/10 
                  Se valida si la contraseña anterior es correcta
                */
                //objUser.Password = objSecurity.EncryptString(this.txtActual.Text);

                objUser.GetUsers();

                if (objUser.NewPassword)
                {
                    bolPassword = Security.VerifyHash(this.txtActual.Text.Trim(), objUser.Password.Trim());
                }
                else
                {
                    objUser = new Users();
                    objUser.IdUser = p_idUser;
                    objUser.Password = objSecurity.EncryptString(this.txtActual.Text.Trim());
                    objUser.GetUsers();
                }

                //Fin MAHG 19/01/10


                /* Proyecto: AMEX
                 * Autor: Marco A. Herrera Gabriel
                 * Fecha: 22/02/10
                 * Funcionalidad: Se valida que el password temporal no se asigne más de una vez al usuario
                */
                #region Que el password temporal no sea asignado más de una vez

                objUser.Password = this.txtNueva.Text.Trim();

                if (objUser.PasswordInicial())
                {
                    throw new Exception("Esta contraseña ya fue utilizada, por favor introduzca otra contraseña.");
                }

                #endregion


                if (this.txtNueva.Text.Length < 8)
                    throw new Exception("La contraseña debe contener mas de 7 caracteres");
                //Si la contraseña es correcta, se modifica el password encriptándolo
                if (objUser.Login != null && bolPassword)
                {

                    /*Inicio MAHG Marco A. Herrera Gabriel 19/01/10 
                      Se cambia el método de encriptación a SHA256 + Salt
                    */
                    //objUser.Password = objSecurity.EncryptString(this.txtNueva.Text);

                    objUser.Password = Security.SHA256_Encrypt(this.txtNueva.Text, null);
                    objUser.NewPassword = true;
                    objUser.ExpiredPassword = false;
                    objUser.UpdateUsers(null, null, null);

                    //Fin MAHG 19/01/10
                }
                else
                {
                    throw new Exception("La contraseña actual es incorrecta");
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

            if (Validaciones.ExistenCaracteresEspeciales(txtNueva.Text.Trim()))
            {
                return true;
            }

            if (Validaciones.ExistenCaracteresEspeciales(txtConfirmacion.Text.Trim()))
            {
                return true;
            }

            if (this.txtActual.Text.Trim() == this.txtNueva.Text.Trim())
            {
                DisplayMessage("La nueva contraseña debe ser diferente a la anterior");
                return false;
            }

            return false;
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
            ////base.OnInit(e);
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
            try
            {
                /* Inicio Marco A. Herrera Gabriel MAHG 20/01/10 */
                //Inicio
                //Auto:Diego Montejano Avila
                //Proyecto: Auditoria 2014
                //Fecha: 2014/09/19
                //Observaciones:Validar la complejidad de la contraseña y los últimos 8 passwords
                UsersPasswords validaUltimosPasswords = new UsersPasswords();
                validaUltimosPasswords.IdUser = Convert.ToInt32(Session["IdUser"]);
                if (validaUltimosPasswords.IdUser == 0)
                {
                    Response.Write("<script language='javascript'>alert('El usuario no es valido.');</script>");
                    return;
                }
                else if (!this.ValidaContrasena(validaUltimosPasswords.IdUser.ToString(),
                             this.txtActual.Text, this.txtNueva.Text,
                             this.txtConfirmacion.Text))
                    return;
                else if (!this.ValidaContrasena(this.txtNueva.Text))
                    return;
                else if (validaUltimosPasswords.ValidaUltimosPasswords(this.txtNueva.Text))
                {
                    Response.Write("<script language='javascript'>alert('La contraseña ya ha sido usada en sus últimos 8 cambios de contraseña.');</script>");
                    return;
                }
                //Fin
                //Valida si existen caracteres especiales en los campos


                this.lblMensaje.Text = "";
                this.ChangePasswordUser(Convert.ToInt32(Session["IdUser"]));
                this.RegisterLog(Log.EnumActionsLog.CambiarContraseña, Convert.ToInt32(Session["IdUser"]), "Id Usuario:" + Convert.ToInt32(Session["IdUser"]) + " Usuario:" + Session["NameUser"].ToString());
                this.DisplayMessageExito("La contraseña fue modificada exitosamente. Por seguridad, debe volver a firmarse.");

                //GAMM Actulizamos sesión Activa
                UpdateSession(Convert.ToInt32(Session["IdUser"].ToString()), Request.Cookies["{24618D5F-65A9-43cf-A40B-CB15DC3328DA}"].Value, 1);

                //GAMM Regeneramos session
                //AntiHack.RegenerarSessionId();

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "redirect", "<script>window.location.href = '../../AE_login_admin.aspx';</script>");
                //GAMM.



                //Fin MAHG 20/01/10


            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion



    }
}
