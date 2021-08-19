/*
HC, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Mercer LLC.
HC is proprietary to Mercer LLC trade secret information. The
documentation and all related HC materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Mercer LLC.
*/
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPA;
using Mercer.Medicines.Logic;
using System.Data;

namespace TPA.interfaz_admon.forma
{
    /// <summary> 
    /// Clase que permite realizar las operaciones de ABC de la tabla EmpresaUsers               
    /// </summary>
    
    public partial class AE_empresasusuarios : PB_PaginaBase
    {
        #region Variables globales

        int intIdUser;

        #endregion

        /// <summary>
        /// Proyecto: AMEX
        /// Requerimiento: Permisos de usuarios por empresa
        /// Funcionalidad: Clase que permite realizar las operaciones de ABC de la tabla EmpresaUsers
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 09/07/2010                     
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Declaración de variables

            int intIdUser;
            Users objUsuario;
            #endregion
            #region Inicialización de variables

            intIdUser = 0;
            objUsuario = new Users();

            #endregion

            base.Page_Load(sender, e);


            if (!this.Page.IsPostBack)
            {
                intIdUser = int.Parse(Request.QueryString["IdUser"]);

                if (intIdUser != null && intIdUser != 0)
                {
                    this.intIdUser = intIdUser;
                    objUsuario.IdUser = intIdUser;
                    objUsuario.GetUsers();

                    txtIdPrestador.Text = Convert.ToString(objUsuario.IdPrestador);
                    lblNombreUsuario.Text = objUsuario.NameUser;
                    LlenarDataGrid();
                }
                else
                {
                    this.DisplayMessage("Ocurrió un error al obtener los permisos del usuario");
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            #region Declaración de variables
            EmpresaUsers objEmpresaUsers;
            EmpresaPrestadores objEmpresaPrestadores;

            #endregion

            #region Inicialización de variables

            //***********************RS****************************
            objEmpresaPrestadores = new EmpresaPrestadores();
            this.loadObjectEmpresaPrestador(objEmpresaPrestadores);
            //***********************RS****************************

            objEmpresaUsers = new EmpresaUsers();
            objEmpresaUsers.IdUser = int.Parse(Request.QueryString["IdUser"]);
            objEmpresaUsers.UsuarioCambio = (int)Session["IdUser"];

            #endregion


            try
            {
                //***********************RS****************************
                objEmpresaPrestadores.IdPrestador = Convert.ToInt16(txtIdPrestador.Text);
                objEmpresaPrestadores.DeletePrestadoresEmpresa();
                //***********************RS****************************

                foreach (GridViewRow row in gvEmpresas.Rows)
                {
                    objEmpresaUsers.IP = (string)Session["IPUser"];
                    objEmpresaUsers.Habilitada = ((CheckBox)row.FindControl("chkHabilitar")).Checked;
                    objEmpresaUsers.Empresa_id = int.Parse(((Label)row.FindControl("lblEmpresaId")).Text);
                    objEmpresaUsers.InsertEmpresaUsers();

                    //***********************RS****************************
                    this.loadObjectEmpresaPrestador(objEmpresaPrestadores);
                    objEmpresaPrestadores.Habilitada = ((CheckBox)row.FindControl("chkHabilitar")).Checked;
                    objEmpresaPrestadores.Empresa_id = int.Parse(((Label)row.FindControl("lblEmpresaId")).Text);
                    if (((CheckBox)row.FindControl("chkHabilitar")).Checked)
                    {
                        if (objEmpresaPrestadores.IdPrestador != null && objEmpresaPrestadores.IdPrestador != 0)
                        {
                            objEmpresaPrestadores.InsertPrestadoresEmpresa();
                        }
                    }
                    //***********************RS****************************

                }

                this.DisplayMessage("Los cambios se han registrado correctamente");
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.ToString());
            }
        }

        private void LlenarDataGrid()
        {
            try
            {

                #region Declaración  de variables

                EmpresaUsers objEmpresaUsers;
                DataSet dsDatos;

                #endregion

                #region Inicialización de variables

                objEmpresaUsers = new EmpresaUsers();
                objEmpresaUsers.IdUser = intIdUser;
                objEmpresaUsers.UsuarioCambio = (int)Session["IdUser"];

                #endregion



                dsDatos = objEmpresaUsers.ListEmpresaUsers();

                if (dsDatos != null && dsDatos.Tables.Count > 0)
                {
                    gvEmpresas.DataSource = dsDatos;
                    gvEmpresas.DataBind();

                    SeleccionarEmpresas();

                }
                else
                {
                    this.DisplayMessage("Ocurrió un error al obtener los permisos del usuario");
                }
            }
            catch (Exception ex)
            {
                this.DisplayMessage(ex.ToString());
            }

        }

        private void SeleccionarEmpresas()
        {
            foreach (GridViewRow row in gvEmpresas.Rows)
            {
                int intHabilitar = int.Parse(((Label)row.FindControl("lblHabilitar")).Text);

                ((CheckBox)row.FindControl("chkHabilitar")).Checked = Convert.ToBoolean(intHabilitar);

            }


        }

        public void loadObjectEmpresaPrestador(EmpresaPrestadores objEmpresaPrestador)
        {
            objEmpresaPrestador.IdPrestador = Convert.ToInt16(txtIdPrestador.Text);
            objEmpresaPrestador.PersonaAprobacionIngreso = null;
            objEmpresaPrestador.PersonaAprobacionRetiro = null;
            objEmpresaPrestador.MotivoRetiro = null;
            objEmpresaPrestador.FechaIngreso = DateTime.Today;
            objEmpresaPrestador.FechaRetiro = new DateTime(1900, 1, 1);
            objEmpresaPrestador.Activo = true;
        }

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

        protected void btnRegresar_Click(object sender, EventArgs e)
        {

            Response.Redirect("Lis_user.aspx");
        }
        #endregion

        protected void chkSeleccionarTodos_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvEmpresas.Rows)
            {
                ((CheckBox)row.FindControl("chkHabilitar")).Checked = chkSeleccionarTodos.Checked;
            }
        }
    }

}
