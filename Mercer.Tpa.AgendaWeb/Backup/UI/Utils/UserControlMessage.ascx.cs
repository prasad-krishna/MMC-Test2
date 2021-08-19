using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercer.Tpa.Agenda.Web.UI.Utils
{

    public partial class UserControlMessage : System.Web.UI.UserControl
    {
        public EnumUserMessage TipoMensaje{ get; set;}
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        public void MostrarMensaje(string msg, EnumUserMessage tipo)
        {
            TipoMensaje = tipo;
            LblMessage.Text = Server.HtmlEncode(msg);
            ContenedorMensaje.Attributes.Add("class","Mensaje" + TipoMensaje);
            this.Visible = true;
        }
    }
} 
