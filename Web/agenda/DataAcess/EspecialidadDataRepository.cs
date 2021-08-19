using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using Mercer.Medicines.DataAccess;
using Mercer.Tpa.Agenda.Web.DataAcess.Abstract;
using Mercer.Tpa.Agenda.Web.Logic;

namespace Mercer.Tpa.Agenda.Web.DataAcess
{
    public class EspecialidadDataRepository : IEspecialidadDataRepository
    {
        string conn = ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ConnectionString;
        /// <summary>
        /// Retorna todas especialidades
        /// </summary>
        /// <returns>Lista tipo Especialidad con todas las especialidades disponibles</returns>
        public IEnumerable<Especialidad> GetEspecialidades()
        {
            DataSet ds = SqlHelper.ExecuteDataset(conn, "[dbo].[ListEspecialidad]");
            List<Especialidad> especialidadesDB =new List<Especialidad>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Especialidad especialidad = new Especialidad();
                especialidad.Nombre=(String)ds.Tables[0].Rows[i]["NombreEspecialidad"];
                especialidad.Id=(int)ds.Tables[0].Rows[i]["idEspecialidad"];
                especialidadesDB.Add(especialidad);
            }
            return especialidadesDB;

        }
        public Especialidad GetEspecialidadById(int idEspecialidad)
        {
            DataSet ds = SqlHelper.ExecuteDataset(conn, "[dbo].[GetEspecialidadById]",idEspecialidad);
            Especialidad especialidad=new Especialidad();
            especialidad.Id=Convert.ToInt32(ds.Tables[0].Rows[0]["IdEspecialidad"]);
            especialidad.Nombre=(string)ds.Tables[0].Rows[0]["NombreEspecialidad"];
            return especialidad;
        }

    }
}
