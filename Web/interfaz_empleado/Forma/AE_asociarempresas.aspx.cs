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
    /// Realiza el ingreso o actualización de usuarios
    /// </summary>
    /// <remarks>Autor: Adriana Diazgranados</remarks>
    /// <remarks>Fecha de creación: 16 de Octubre de 2008</remarks>
    public partial class AE_asociarempresas : PB_PaginaBase
    {

        #region Atributtes

      
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
                    btnAsociar.Visible = false;
                    string strEmpresaID = Request.QueryString["EmpresaID"];
                    if (strEmpresaID != null)
                    {
                        if (this.EliminarAsociacion(int.Parse(strEmpresaID)) == 1)
                        {
                               DisplayMessageExito("La asociación ha sido eliminada con éxito");
                        }
                        else
                        {
                            DisplayMessage("Ocurrió un error al eliminar la asociación, por favor avise al administrador");
                        }
                    }
                    this.LoadControls();                   
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.Message);
            }
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
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion
      

        protected void btnAsociar_Click(object sender, EventArgs e)
        {
            //Asociar empresa
            if (Validaciones())
            {
                if (EmpresaAsociada())
                {
                    this.DisplayMessage("Las empresa no puede asociarse debido a que alguna de estas ha sido asociada a otra empresa.");
                }
                else
                {
                    this.AsociarEmpresas();
                    this.DisplayMessage("La empresa ha sido asociada con éxito.");

                    this.ddlEmpresaHC.Enabled = false;
                    this.ddlEmpresaSICAM.Enabled = false;
                    this.ddlPolizas.Enabled = false;
                    this.btnImportar.Enabled = true;
                    this.btnAsociar.Enabled = false;
                }
            }
            //Importar asegurados
            if (Validaciones())
            {
                if (EmpresasAsociadas())
                {
                    if (this.ImportarAsegurados())
                    {
                        this.DisplayMessage("Se importaron correctamente los asegurados", "AE_asociarempresas.aspx");
                    }
                    else
                    {
                        this.DisplayMessage("Ocurrió un error al importar los asegurados, por favor contacte al administrador.");
                    }
                }
            }
        }

        //protected void btnImportar_Click(object sender, EventArgs e)
        //{
        //    if (Validaciones())
        //    {
        //        if (EmpresasAsociadas())
        //        {
        //            if (this.ImportarAsegurados())
        //            {
        //                this.DisplayMessage("Se importaron correctamente los asegurados", "AE_asociarempresas.aspx");
        //            }
        //            else
        //            {
        //                this.DisplayMessage("Ocurrió un error al importar los asegurados, por favor contacte al administrador.");
        //            }
        //        }                
        //    }
        //}

        private bool ImportarAsegurados()
        {
            SIC_EMPRESA_SICAM objEmpresaSICAM = new SIC_EMPRESA_SICAM();
            objEmpresaSICAM.EmpClave = int.Parse(this.ddlEmpresaSICAM.SelectedItem.Value);
            return objEmpresaSICAM.InsertAsegurados();
        }       

        protected void gvEmpresas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmpresas.PageIndex = e.NewPageIndex;
            FillEmpresasAsociadas();
        }

        protected void ddlEmpresaSICAM_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsPolizas;
            DataSet dsFechaPolizas; 
            this.ddlPolizas.Items.Clear();

            SIC_EMPRESA_SICAM objEmpreas_sicam = new SIC_EMPRESA_SICAM();
            objEmpreas_sicam.EmpClave = int.Parse(ddlEmpresaSICAM.SelectedItem.Value);

            dsFechaPolizas = objEmpreas_sicam.Get_FechaPoliza(int.Parse(ddlEmpresaSICAM.SelectedItem.Value));

            this.dllFechaPoliza.DataTextField = "Usuario";
            this.dllFechaPoliza.DataValueField = "Fecha";

            this.dllFechaPoliza.DataSource = dsFechaPolizas;
            this.dllFechaPoliza.DataBind();

            if ((dllFechaPoliza.Items.Count) > 0)
            {
                this.lblPolizaFecha.Visible = true; 
                this.lblPolizaFecha.Text = "La póliza fue activada para HC el día  " + (dllFechaPoliza.Items[0].Value).Substring(0,10) + " a las" + (dllFechaPoliza.Items[0].Value).Substring(10) + " por el usuario " + dllFechaPoliza.Items[0].Text;
            }
            else
            {
                this.lblPolizaFecha.Visible = false;
            }
            
                          

            this.ddlPolizas.DataTextField = "nombre";
            this.ddlPolizas.DataValueField = "polClave";

            dsPolizas = objEmpreas_sicam.Get_Polizas();           

            if (dsPolizas.Tables[0].Rows.Count != 0) 
            
            {
                this.ddlPolizas.Enabled = true;
                lblErrorPoliza.Text = "";

                foreach (DataRow row in dsPolizas.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(row["polPredeterminadaHC"].ToString()))
                    {
                        this.ddlPolizas.Items.Insert(0, new ListItem(row["nombre"].ToString(), row["polClave"].ToString()));
                        return;
                    }
                }

                this.ddlPolizas.DataSource = dsPolizas;
                this.ddlPolizas.DataBind();
                this.ddlPolizas.Items.Insert(0, new ListItem("- Seleccione una póliza -", "0"));

            }

            else
            {
                String nombreEmpresa;
                nombreEmpresa = ddlEmpresaSICAM.SelectedItem.Text;
                lblErrorPoliza.Text = "La empresa " + nombreEmpresa + " no cuenta con una poliza compatible con el sistema, por favor contacte al administrador.";
                this.ddlPolizas.Enabled = false;
            }            

        }
        #endregion

        #region Methods

        private bool EmpresaAsociada()
        {
            
            SIC_EMPRESA_SICAM objEmpresaSICAM = new SIC_EMPRESA_SICAM();

            objEmpresaSICAM.Empresa_Id = int.Parse(ddlEmpresaHC.SelectedItem.Value);
            objEmpresaSICAM.EmpClave = int.Parse(ddlEmpresaSICAM.SelectedItem.Value);           

            return (objEmpresaSICAM.GetSIC_EMPRESA_SICAM() != 0);            
        }


        private bool EmpresasAsociadas()
        {
            SIC_EMPRESA_SICAM objEmpresaSICAM = new SIC_EMPRESA_SICAM();

            objEmpresaSICAM.Empresa_Id = int.Parse(ddlEmpresaHC.SelectedItem.Value);
            objEmpresaSICAM.EmpClave = int.Parse(ddlEmpresaSICAM.SelectedItem.Value);

            if (objEmpresaSICAM.GetSIC_EMPRESA_SICAM() == 1 && objEmpresaSICAM.Empresa_Id == int.Parse(ddlEmpresaHC.SelectedItem.Value) && objEmpresaSICAM.EmpClave == int.Parse(ddlEmpresaSICAM.SelectedItem.Value))
            {
                return true;
            }
            else
            {
                DisplayMessage("Las empresas no se encuentran asociadas, por favor asocie estas para que puedan importarse los asegurados");

                return false;
            }


        }

        private void AsociarEmpresas()
        {
            SIC_EMPRESA_SICAM objEmpresaSICAM = new SIC_EMPRESA_SICAM();

            objEmpresaSICAM.Empresa_Id = int.Parse(ddlEmpresaHC.SelectedItem.Value);
            int empresa_id = int.Parse(ddlEmpresaHC.SelectedItem.Value);
            objEmpresaSICAM.EmpClave = int.Parse(ddlEmpresaSICAM.SelectedItem.Value);
            objEmpresaSICAM.PolClave = ddlPolizas.SelectedItem.Value;
            this.RegisterLog(Log.EnumActionsLog.AsociarEmpresa, empresa_id, "Id Usuario:" + Convert.ToInt32(Session["IdUser"]) + " Empresa:" + empresa_id);
            
            objEmpresaSICAM.InsertSIC_EMPRESA_SICAM();            
        }

        private void LoadControls()
        {
            SIC_EMPRESA objEmpresa = new SIC_EMPRESA();
            SIC_EMPRESA_SICAM objEmpresa_Sicam = new SIC_EMPRESA_SICAM();

            this.ddlEmpresaHC.DataTextField = "nombre";
            this.ddlEmpresaHC.DataValueField = "empresa_id";
            this.ddlEmpresaHC.DataSource = objEmpresa.ConsultSIC_EMPRESA();
            this.ddlEmpresaHC.DataBind();
            this.ddlEmpresaHC.Items.Insert(0, new ListItem("- Seleccione una empresa-", "0"));

            this.ddlEmpresaSICAM.DataTextField = "emsRazon_social";
            this.ddlEmpresaSICAM.DataValueField = "emsClave";
            this.ddlEmpresaSICAM.DataSource = objEmpresa_Sicam.ConsultSic_Empresa_SICAM();
            this.ddlEmpresaSICAM.DataBind();
            this.ddlEmpresaSICAM.Items.Insert(0, new ListItem("- Seleccione una empresa -", "0"));

            this.ddlPolizas.Items.Insert(0, new ListItem("- Seleccione una póliza -", "0"));
            this.FillEmpresasAsociadas();
            
        }

        private void FillEmpresasAsociadas()
        {
            SIC_EMPRESA_SICAM objEmpresa_Sicam = new SIC_EMPRESA_SICAM();

            gvEmpresas.DataSource = objEmpresa_Sicam.Get_EMPRESAS();
            gvEmpresas.DataBind();
        }
        
        private bool Validaciones()
        {
            if (ddlEmpresaHC.SelectedItem.Value == "0" || ddlEmpresaSICAM.SelectedItem.Value == "0")
            {
                this.DisplayMessage("Seleccione una empresa por favor");

                return false;
            }

            return true;
        }

        private int EliminarAsociacion(int intEmpresaID)
        {
            SIC_EMPRESA_SICAM objEmpresa = new SIC_EMPRESA_SICAM();
            objEmpresa.EmpClave = intEmpresaID;

            return objEmpresa.DeleteAsociacion();
        }

        #endregion

        protected void ddlEmpresaHC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EmpresaAsociada())
            {
                btnAsociar.Visible = false;                
            }
            else
	        {
                btnAsociar.Visible = true; 
	        }

        }

    }
}
