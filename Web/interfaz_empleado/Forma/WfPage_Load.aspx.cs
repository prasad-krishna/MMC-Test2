using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Mercer.Medicines.Logic;
using System.Web.Security;
//GAMM
using MMC.Seguridad.Utilerias;

namespace TPA.interfaz_empleado.Forma
{
    public partial class WfPage_Load : PB_PaginaBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {

            base.Page_Load(sender, e);
            UpdateSession(Convert.ToInt32(Session["IdUser"].ToString()), Request.Cookies["{24618D5F-65A9-43cf-A40B-CB15DC3328DA}"].Value, 0);
            
            AntiHack.LimpiaSession();
            AntiHack.RegenerarSessionId();
        }
    }
}