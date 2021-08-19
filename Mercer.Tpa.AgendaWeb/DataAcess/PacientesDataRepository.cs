/*
'===============================================================================
Mercer, Health And Benefits
This product, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Mercer.
This product is proprietary to Mercer trade secret information. The
documentation and all related materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Mercer

Copyright (c) 2010 by Mercer
'===============================================================================
*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using Mercer.Medicines.DataAccess;
using Mercer.Tpa.Agenda.Web.DataAcess.Abstract;
using Mercer.Tpa.Agenda.Web.Logic;

namespace Mercer.Tpa.Agenda.Web.DataAcess
{
    /// <summary>
    /// Proyecto: Módulo Agenda Médica
    /// Autor: Juan Camilo Zapata
    /// Fecha: 2010/4/27
    /// Funcionalidad: Métodos de consulta de pacientes
    /// </summary>
    public class PacientesDataRepository : IPacientesDataRepository
    {
        #region Variables privadas
        readonly string _connSicau = ConfigurationManager.ConnectionStrings["ConnectionStringSicau"].ConnectionString;
        
        #endregion

        #region Métodos públicos

        /// <summary>
        /// obtiene toda la informacion de un paciente ,
        ///  dependiendo si se pasa un idempleado o
        ///  idbeneficiario , este metodo consulta la base de datos SICAM
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <param name="idBeneficiario"></param>
        /// <returns></returns>
        public InfoPaciente GetPacienteByIds(int idEmpleado, int idBeneficiario)
        {
            DataSet ds = null;
            var info = new InfoPaciente();
            if (idBeneficiario > 0)
            {
                ds = SqlHelper.ExecuteDataset(_connSicau, "[dbo].[SICAM_OBTENER_BENEFICIARIO]", "IND", idBeneficiario);
                info.Id = (int)ds.Tables[0].Rows[0]["beneficiario_id"];
                if (ds.Tables[0].Rows[0]["primer_nombre"] != DBNull.Value)
                {
                    info.Nombre = info.Nombre + (string)ds.Tables[0].Rows[0]["primer_nombre"] + " ";
                }
                if (ds.Tables[0].Rows[0]["segundo_nombre"] != DBNull.Value)
                {
                    info.Nombre = info.Nombre + (string)ds.Tables[0].Rows[0]["segundo_nombre"] + " ";
                }
                if (ds.Tables[0].Rows[0]["primer_apellido"] != DBNull.Value)
                {
                    info.Nombre = info.Nombre + (string)ds.Tables[0].Rows[0]["primer_apellido"] + " ";
                }
                if (ds.Tables[0].Rows[0]["segundo_apellido"] != DBNull.Value)
                {
                    info.Nombre = info.Nombre + (string)ds.Tables[0].Rows[0]["segundo_apellido"] + " ";
                }
                if (ds.Tables[0].Rows[0]["fecha_nacimiento"] != DBNull.Value)
                {
                    info.FechaNacimiento = (DateTime)ds.Tables[0].Rows[0]["fecha_nacimiento"];
                }
                if (ds.Tables[0].Rows[0]["direccion"] != DBNull.Value)
                {
                    info.Direccion = (string)ds.Tables[0].Rows[0]["direccion"];
                }
                if (ds.Tables[0].Rows[0]["telefono"] != DBNull.Value)
                {
                    info.Telefono = (string)ds.Tables[0].Rows[0]["telefono"];
                }
                if(ds.Tables[0].Rows[0]["identificacion"]!=DBNull.Value)
                {
                    info.Identificacion = (string)ds.Tables[0].Rows[0]["identificacion"];
                }
            }
            else if (idEmpleado > 0)
            {
                ds = SqlHelper.ExecuteDataset(_connSicau, "[dbo].[SICAM_OBTENER_EMPLEADO]", "IND", idEmpleado);
                info.Id = (int)ds.Tables[0].Rows[0]["id_empleado"];
                if (ds.Tables[0].Rows[0]["primer_nombre"] != DBNull.Value)
                {
                    info.Nombre = info.Nombre + (string)ds.Tables[0].Rows[0]["primer_nombre"] + " ";
                }
                if (ds.Tables[0].Rows[0]["segundo_nombre"] != DBNull.Value)
                {
                    info.Nombre = info.Nombre + (string)ds.Tables[0].Rows[0]["segundo_nombre"] + " ";
                }
                if (ds.Tables[0].Rows[0]["apellido_paterno"] != DBNull.Value)
                {
                    info.Nombre = info.Nombre + (string)ds.Tables[0].Rows[0]["apellido_paterno"] + " ";
                }
                if (ds.Tables[0].Rows[0]["apellido_materno"] != DBNull.Value)
                {
                    info.Nombre = info.Nombre + (string)ds.Tables[0].Rows[0]["apellido_materno"] + " ";
                }
                if (ds.Tables[0].Rows[0]["fecha_nacimiento"] != DBNull.Value)
                {
                    info.FechaNacimiento = (DateTime)ds.Tables[0].Rows[0]["fecha_nacimiento"];
                }
                if (ds.Tables[0].Rows[0]["direccion"] != DBNull.Value)
                {
                    info.Direccion = (string)ds.Tables[0].Rows[0]["direccion"];
                }
                if (ds.Tables[0].Rows[0]["telefono"] != DBNull.Value)
                {
                    info.Telefono = (string)ds.Tables[0].Rows[0]["telefono"];
                }
                if (ds.Tables[0].Rows[0]["identificacion"] != DBNull.Value)
                {
                    info.Identificacion = (string)ds.Tables[0].Rows[0]["identificacion"];
                }
            }
            else
            {
                throw new ApplicationException("Se debe especificar el id del empleado o el de beneficiario.");
            }

            return info;
        }

        #endregion

    }
}
