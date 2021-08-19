/*
'===============================================================================
Delima Mercer (Colombia) Ltda, Sistema Autorizaciones y Reembolsos
This product, including any programs, documentation, distribution media, and all aspects
and modifications thereof shall remain the sole property of Delima Mercer (Colombia) Ltda.
This product is proprietary to Mercer trade secret information. The
documentation and all related materials shall not be copied, altered, revised,
enhanced, and/or improved in any way unless authorized in writing by Delima Mercer (Colombia) Ltda

Copyright (c) 2010 by Delima Mercer (Colombia) Ltda
'===============================================================================
*/

using System;
using System.Data.SqlClient;
using System.Configuration;

namespace Mercer.Medicines.DataAccess
{
	/// <summary>
	/// Esta clase provee la funcionalidad para realizar la conexión a la base de datos
	/// </summary>
	/// <remarks>Autor: Adriana Diazgranados</remarks>
	/// <remarks>Fecha de creación: 6 de Octubre</remarks>
	public class Connection
	{
		#region Attributes
		
		/// <summary>Atributo, conexión a la base de datos.</summary>
		private SqlConnection _objConnection;		
        		
		#endregion

		#region Properties

		/// <summary>
		/// Propiedad, Objeto conexión a la base de datos
		/// </summary>
		public SqlConnection objConnection
		{
			get { return _objConnection; }
			set { _objConnection = value; }
		}

		


		#endregion

		#region Enumeration

		/// <summary>
		/// Enumeración, lista las acciones de registro en el log del sistema
		/// </summary>
		public enum EnumConnections
		{
			ConnectionReembolsos =1,
			ConnectionSICAU = 2,
            ConnectionSICAM = 3
		}

		#endregion

		#region Methods
		/// <summary>
		/// Constructor, inicializa cada uno de los atributos de la clase CConnection.
		/// </summary>	
		public Connection(EnumConnections p_typeConnection)
		{			
            if (null == objConnection)
            {
                objConnection = new SqlConnection(ConnectionString(p_typeConnection));                
            }
		}
		
		/// <summary>Método, abre la conexión a la base de datos.</summary>		
		public void Open()
		{
			try
			{
				if(objConnection.State != System.Data.ConnectionState.Open){
					objConnection.Open();
				}
			}
			catch (SqlException ex){
				throw(new Exception(ex.Message));
			}
			catch (Exception ex){
				throw(new Exception(ex.Message));
			}
		}
		
		/// <summary>Método, cierra la conexión a la base de datos.</summary>		
		public void Close()
		{
			try
			{
				if(objConnection.State == System.Data.ConnectionState.Open){
                    objConnection.Close();
				}
			}
			catch (SqlException ex){
				throw(new Exception(ex.Message));
			}
			catch (Exception ex){
				throw(new Exception(ex.Message));
			}
		}

        /// <summary>Propiedad que retorna la cadena de texto con datos de conexión.</summary>
        /// <exception cref="System.ArgumentException">No existe una cadena de coneccion configurada en el webconfig</exception>
        public static string ConnectionString(EnumConnections p_typeConnection)
        {
            /*Inicio Marco A. Herrera Gabriel MAHG 19/01/2010*/
            //System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
            /*Fin MAHG 19/01/2010*/
			string strConn;
			if(p_typeConnection == Connection.EnumConnections.ConnectionSICAU)
            {
                /*Inicio Marco A. Herrera Gabriel MAHG 19/01/2010*/                
                //strConn = ((string)(configurationAppSettings.GetValue("ConnectionStringSicau", typeof(string))));
                strConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringSicau"].ToString();
                /*Fin MAHG 19/01/2010*/
			}
			else
			{
                if (p_typeConnection == Connection.EnumConnections.ConnectionReembolsos || p_typeConnection == null)
                {
                    /*Inicio Marco A. Herrera Gabriel MAHG 19/01/2010*/
                    //strConn = ((string)(configurationAppSettings.GetValue("ConnectionStringReembolsos", typeof(string))));
                    strConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringReembolsos"].ToString();
                    /*Fin MAHG 19/01/2010*/
                }
                else
                {
                    strConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringSicam"].ToString();                                       
                }
			}

            if (string.Empty.Equals(strConn))
                throw new System.ArgumentException("El string de conexión a la base de datos está vacío");
            
			return strConn;            
        }

		#endregion
	}
}
