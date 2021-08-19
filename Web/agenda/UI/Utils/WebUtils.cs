using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Mercer.Tpa.Agenda.Web.UI.Utils
{
    public static class WebUtils
    {
        /// <summary>
        /// Revisa que todos los parametros esperados se encuentren 
        /// en el request o lanza una excepción si no lo están
        /// </summary>
        /// <param name="parametros"></param>
        public static void RequerirParametros(params string[] parametros)
        {
            List<string> parametrosNoEncontrados = new List<string>();
            var strBuilderFaltantes = new StringBuilder();
            foreach (var parametro in parametros)
            {
                if (HttpContext.Current.Request.Params[parametro] == null)
                {
                    strBuilderFaltantes.Append(parametro);
                    strBuilderFaltantes.Append(",");
                }
            }
            if (strBuilderFaltantes.Length > 0)
            {
                throw new ApplicationException("Parametros requeridos no encontrados:" + strBuilderFaltantes.ToString());
            }
        }
    }
}
