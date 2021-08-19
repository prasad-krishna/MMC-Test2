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

namespace TPA.interfaz_admon.forma
{
    /// <summary>
    /// Permite guardar el registro del aviso de privacidad    
    /// </summary>
    /// <remarks>Autor: Ricardo Silva
    /// Fecha de creación: 05/10/2011
    /// </remarks>

    
    public partial class AE_avisoPrivacidad : PB_PaginaBase
    {
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            //Inicio Ricardo Silva 05/10/2011
            //Se insertan los campos del aviso de privacidad            

            try
            {
                //Se carga el load de la página base

                base.Page_Load(sender, e);

                if (!this.Page.IsPostBack)
                {

                    int empleadoID;
                    int beneficiarioID;
                    beneficiarioID = Convert.ToInt32(Request.QueryString["beneficiario_id"]);
                    empleadoID = Convert.ToInt32(Request.QueryString["employee_id"]);
                    SIC_PRIVACIDAD Privacidad = new SIC_PRIVACIDAD();
                    string fechaUltimaFirma;

                    Privacidad.GetSIC_PRIVACIDAD(beneficiarioID, empleadoID);


                    if (Privacidad.fechaUltimaFirma != null)
                    {
                        //lblfechafirma.Visible = true;
                        //LblUltimaFirma.Visible = true;

                        //dice si el paciente ha firmado el aviso de privacidad
                        chkAvisoPrivacidad.Checked = true;

                        fechaUltimaFirma = Convert.ToString(Privacidad.fechaUltimaFirma);
                        fechaUltimaFirma = fechaUltimaFirma.Substring(0, 10);
                        txtFechaAvisoPrivacidad.Text = fechaUltimaFirma;
                        //LblUltimaFirma.Text = fechaUltimaFirma;
                        //lnkBorrarPrivacidad.Visible = true;
                    }
                    else
                    {
                        //lblNoFechaFirma.Visible = true;
                    }
                }
            }

            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
        }
        
        #region Eventos

        protected void Aceptar_Click(object sender, EventArgs e)
        {
            if (txtFechaAvisoPrivacidad.Text != string.Empty && chkAvisoPrivacidad.Checked == true)
            {              
             int empleadoID;
            int beneficiarioID;
            beneficiarioID = Convert.ToInt32(Request.QueryString["beneficiario_id"]);
            empleadoID = Convert.ToInt32(Request.QueryString["employee_id"]);
            SIC_PRIVACIDAD Privacidad = new SIC_PRIVACIDAD();

            SIC_PRIVACIDAD objPrivacidad = new SIC_PRIVACIDAD();
            objPrivacidad.Id_empleado = Convert.ToInt32(Request.QueryString["employee_id"]);

            if (beneficiarioID != null && beneficiarioID > 0)
                objPrivacidad.Beneficiario_id = beneficiarioID;
            else
                objPrivacidad.Beneficiario_id = -1;

            if (chkAvisoPrivacidad.Checked == true)
            {
                objPrivacidad.Firma = true;
            }
            else
            {
                objPrivacidad.Firma = false;
            }


            if (chkAvisoPrivacidad.Checked == true)
            {
                objPrivacidad.Fecha_firma = Convert.ToDateTime(txtFechaAvisoPrivacidad.Text);
                objPrivacidad.InsertSIC_PRIVACIDAD();
            }
            else
            {
                objPrivacidad.DeleteSIC_PRIVACIDAD(beneficiarioID, empleadoID);
            }
            Response.Write("<script>window.opener.ActualizarPrivacidad(); window.close();</script>");

            DisplayMessage("Se han registrado exitosamente los datos");     
            }
            DisplayMessage("Debe confirmar si se firmo el aviso de privacidad");            

        }

        protected void Cerrar_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.opener.ActualizarPrivacidad();top.close();</script>");
        }

        protected void lnkBorrarPrivacidad_Click(object sender, EventArgs e)
        {
            int empleadoID;
            int beneficiarioID;
            beneficiarioID = Convert.ToInt32(Request.QueryString["beneficiario_id"]);
            empleadoID = Convert.ToInt32(Request.QueryString["employee_id"]);
            SIC_PRIVACIDAD objPrivacidad = new SIC_PRIVACIDAD();
            objPrivacidad.DeleteSIC_PRIVACIDAD(beneficiarioID, empleadoID);
            Response.Write("<script>window.opener.ActualizarPrivacidad(); window.close();</script>");


        }

        #endregion      

    }

        
    }