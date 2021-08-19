using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Mercer.Medicines.Logic;

namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Realiza la edición y adición de la línea de negocio
    /// </summary>
    public partial class AE_lineanegocio : PB_PaginaBase
    {
        #region Atributos




        #endregion

        #region Inicializacion

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
                    if (Request.QueryString["IdLineaNegocio"] != null)
                    {
                        this.loadLineaNegocio(Convert.ToInt32(Request.QueryString["IdLineaNegocio"]));
                    }
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Evento, realiza el llamado a la modificación o actualización de de los datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Aceptar_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Request.QueryString["IdLineaNegocio"] != null)
                {
                    this.UpdateLineaNegocio(Convert.ToInt32(Request.QueryString["IdLineaNegocio"]));
                    this.RegisterLog(Log.EnumActionsLog.ModificarLineaNegocio, Convert.ToInt32(Request.QueryString["IdLineaNegocio"]), "Modificación de línea de negocio " + this.txtNombreLineaNegocio.Text);
                    Response.Write("<script>alert('La línea de negocio fue modificada exitosamente'); top.close();</script>");
                }
                else
                {
                    int idLineaNegocio = this.InsertLineaNegocio();
                    this.RegisterLog(Log.EnumActionsLog.AgregarLineaNegocio, idLineaNegocio, "Adición de línea de negocio " + this.txtNombreLineaNegocio.Text);
                    Response.Write("<script>alert('La línea de negocio fue adicionada exitosamente'); top.close();</script>");
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }


        /// <summary>
        /// Evento, cierra la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Cancelar_Click(object sender, System.EventArgs e)
        {
            Response.Write("<script>top.close();</script>");
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método, carga la información de la línea de negocio en el formulario
        /// </summary>
        private void loadLineaNegocio(int p_idlineanegocio)
        {
            if (Request.QueryString["IdLineaNegocio"] != null)
            {
                LineasNegocio objLineasNegocio = new LineasNegocio();
                objLineasNegocio.IdLineaNegocio = p_idlineanegocio;
                //objLineasNegocio.empresa_id = Convert.ToInt32(Session["Company"]);
                objLineasNegocio.GetLineasNegocio();

                this.txtNombreLineaNegocio.Text = objLineasNegocio.NombreLineaNegocio;
                this.rblactiva.SelectedValue = Convert.ToString(Convert.ToInt32(objLineasNegocio.activa));
            }
        }

        /// <summary>
        /// Método, modifica los datos de la línea de negocio
        /// </summary>
        private void UpdateLineaNegocio(int p_idlineanegocio)
        {
            LineasNegocio objLineasNegocio = new LineasNegocio();
            objLineasNegocio.IdLineaNegocio = p_idlineanegocio;
            this.LoadObjectLineasNegocio(objLineasNegocio);
            objLineasNegocio.UpdateLineasNegocio();
        }

        /// <summary>
        /// Método, inserta una nueva línea de negocio
        /// </summary>
        private int InsertLineaNegocio()
        {
            LineasNegocio objLineasNegocio = new LineasNegocio();
            this.LoadObjectLineasNegocio(objLineasNegocio);
            int idLineaNegocio = objLineasNegocio.InsertLineasNegocio();
            return idLineaNegocio;
        }

        /// <summary>
        /// Método, carga la información del formulario en un objeto 
        /// </summary>
        /// <param name="objLineasNegocio"></param>
        private void LoadObjectLineasNegocio(LineasNegocio objLineasNegocio)
        {
            objLineasNegocio.NombreLineaNegocio = this.txtNombreLineaNegocio.Text.Trim();
            objLineasNegocio.empresa_id = Convert.ToInt32(Session["Company"]);
            objLineasNegocio.activa = Convert.ToBoolean(Convert.ToInt32(this.rblactiva.SelectedValue));
        }

        #endregion

    }
}
