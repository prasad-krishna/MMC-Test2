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
    /// Realiza la edición y adición del medicamento
    /// </summary>
    public partial class AE_medicamento : PB_PaginaBase
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
                    this.loadControlLaboratory();

                    if (Request.QueryString["medicine_id"] != null)
                    {
                        this.loadMedicine(Convert.ToInt32(Request.QueryString["medicine_id"]));
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
            this.Aceptar.Click += new System.EventHandler(this.Aceptar_Click);
            this.Cancelar.Click += new System.EventHandler(this.Cancelar_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        /// <summary>
        /// Evento, realiza el llamado a la modificación o actualización de datos de los datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Aceptar_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (Request.QueryString["medicine_id"] != null)
                {
                    this.UpdateMedicine(Convert.ToInt32(Request.QueryString["medicine_id"]));
                    this.RegisterLog(Log.EnumActionsLog.ModificarMedicamento, Convert.ToInt32(Request.QueryString["medicine_id"]), "Modificación medicamento " + this.Fnombre.Text);
                    Response.Write("<script>alert('El medicamento fue modificado exitosamente'); top.close();</script>");
                }
                else
                {
                    int idMedicine = this.InsertMedicine();
                    this.RegisterLog(Log.EnumActionsLog.ModificarMedicamento, idMedicine, "Modificación medicamento " + this.Fnombre.Text);
                    Response.Write("<script>alert('El medicamento fue adicionado exitosamente'); top.close();</script>");
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
        /// Método, carga el listado de laboratorios
        /// </summary>
        private void loadControlLaboratory()
        {
            this.FillList("Laboratorios", "Laboratorio", this.Flaboratory, "--Seleccione--");

        }

        /// <summary>
        /// Método, carga la información del medicamento en el formulario
        /// </summary>
        private void loadMedicine(int p_medicine)
        {
            if (Request.QueryString["medicine_id"] != null)
            {
                Medicamentos objMedicine = new Medicamentos();
                objMedicine.empresa_id = Convert.ToInt32(Session["Company"]);
                objMedicine.IdMedicamento = p_medicine;
                objMedicine.GetMedicamentos();

                this.Fcodigo.Text = objMedicine.CodigoPos;
                this.Fnombre.Text = objMedicine.NombreComercial;
                this.Fprincipio.Text = objMedicine.PrincipioActivo;
                this.Fpresentacion.Text = objMedicine.Presentacion;
                this.Fforma_farmaceutica.Text = objMedicine.FormaFarmaceutica;
                this.Fcantidad_presentacion.Text = objMedicine.CantidadPresentacion;
                this.Fconcentracion.Text = objMedicine.Concentracion;
                this.Fregistro_sanitario.Text = objMedicine.RegistroSanitario;
                this.Fregimen.Text = objMedicine.Regimen;
                this.Flaboratory.SelectedValue = objMedicine.IdLaboratorio.ToString();
                this.Factivo.SelectedValue = objMedicine.Activo.ToString();
                this.Freembolsable.SelectedValue = objMedicine.Reembolsable.ToString();
                this.Fprecio.Text = string.Format("{0:0,0}", objMedicine.PrecioPublico);
                this.FprecioDistri.Text = string.Format("{0:0,0}", objMedicine.PrecioDistribuidor);
            }
        }

        /// <summary>
        /// Método, modifica los datos del medicamento
        /// </summary>
        private void UpdateMedicine(int p_idMedicine)
        {
            Medicamentos objMedicine = new Medicamentos();
            objMedicine.IdMedicamento = p_idMedicine;
            this.LoadObjectMedicine(objMedicine);
            objMedicine.UpdateMedicamentos();
        }

        /// <summary>
        /// Método, inserta un nuevo medicamento
        /// </summary>
        private int InsertMedicine()
        {

            Medicamentos objMedicine = new Medicamentos();
            this.LoadObjectMedicine(objMedicine);
            int idMedicine = objMedicine.InsertMedicamentos();
            return idMedicine;
        }

        /// <summary>
        /// Método, carga la información del formulario en un objeto 
        /// </summary>
        /// <param name="objMedicine"></param>
        private void LoadObjectMedicine(Medicamentos objMedicine)
        {
            objMedicine.CodigoPos = this.Fcodigo.Text.Trim();
            objMedicine.NombreComercial = this.Fnombre.Text.Trim();
            objMedicine.PrincipioActivo = this.Fprincipio.Text.Trim();
            objMedicine.FormaFarmaceutica = this.Fforma_farmaceutica.Text.Trim();
            objMedicine.Presentacion = this.Fpresentacion.Text.Trim();
            objMedicine.CantidadPresentacion = this.Fcantidad_presentacion.Text.Trim();
            objMedicine.Concentracion = this.Fconcentracion.Text.Trim();
            objMedicine.Regimen = this.Fregimen.Text.Trim();
            objMedicine.IdLaboratorio = Convert.ToInt32(this.Flaboratory.SelectedValue);
            objMedicine.Reembolsable = Convert.ToInt32(this.Freembolsable.SelectedValue);
            objMedicine.Activo = Convert.ToInt32(this.Factivo.SelectedValue);
            objMedicine.empresa_id = Convert.ToInt32(Session["Company"]);
            objMedicine.RegistroSanitario = this.Fregistro_sanitario.Text.Trim();
            objMedicine.IdTipoServicio = Convert.ToInt16(Servicios.EnumTiposServicio.Medicamentos);

            if (this.Fprecio.Text != string.Empty)
            {
                objMedicine.PrecioPublico = Convert.ToDecimal(this.Fprecio.Text);
            }
            if (this.FprecioDistri.Text != string.Empty)
            {
                objMedicine.PrecioDistribuidor = Convert.ToDecimal(this.FprecioDistri.Text);
            }
        }

        #endregion

    }
}
