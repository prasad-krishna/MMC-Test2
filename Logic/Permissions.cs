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
using System.Data;
using System.Text;
using Mercer.Medicines.DataAccess;

namespace Mercer.Medicines.Logic
{
	/// <summary>
	/// Esta clase provee la funcionalidad para
	/// </summary>
	/// <remarks>Autor: Adriana Diazgranados</remarks>
	/// <remarks>Fecha de creación: </remarks>
	public class Permissions : GeneralProcess
	{
		#region Attributes
		
		/// <summary>Atributo, Id del menú</summary>
		private int _IdPermission;
		/// <summary>Atributo, Nombre del menú</summary>
		private string _NamePermission;
		/// <summary>Atributo, Id del tipo de menú</summary>
		private EnumPermissionsTypes _IdPermissionType;
		/// <summary>Atributo, Id del menú padre</summary>
		private int _IdPermissionParent;
		/// <summary>Atributo, Url de redirección del menú</summary>
		private string _URL;
		/// <summary>Atributo, Orden de despliegue del menú</summary>
		private short _OrderPermission;
		/// <summary>Atributo, Indica si es padre</summary>
		private bool _Parent;
		/// <summary>Atributo, Id del usuario </summary>
		private int _IdUser;
		
		#endregion
		
		#region Properties
		
		/// <summary>Propiedad, Id del menú</summary>
		public int IdPermission
		{
			get	{ return _IdPermission; }
			set	{ _IdPermission = value; }
		}
		/// <summary>Propiedad, Nombre del menú</summary>
		public string NamePermission
		{
			get	{ return _NamePermission; }
			set	{ _NamePermission = value; }
		}
		/// <summary>Propiedad, Tipo de menú</summary>
		public EnumPermissionsTypes IdPermissionType
		{
			get	{ return _IdPermissionType; }
			set	{ _IdPermissionType = value; }
		}
		/// <summary>Propiedad, Id del menú padre</summary>
		public int IdPermissionParent
		{
			get	{ return _IdPermissionParent; }
			set	{ _IdPermissionParent = value; }
		}
		/// <summary>Propiedad, Url de redirección del menú</summary>
		public string URL
		{
			get	{ return _URL; }
			set	{ _URL = value; }
		}
		/// <summary>Propiedad, Orden de despliegue del menú</summary>
		public short OrderPermission
		{
			get	{ return _OrderPermission; }
			set	{ _OrderPermission = value; }
		}
		/// <summary>Propiedad, Indica si es padre</summary>
		public bool Parent
		{
			get	{ return _Parent; }
			set	{ _Parent = value; }
		}
		/// <summary>Propiedad, Id del usuario de creación si el usuario es del sistema</summary>
		public int IdUser
		{
			get	{ return _IdUser; }
			set	{ _IdUser = value; }
		}
		
		
		#endregion	

		#region Enumeration

		/// <summary>
		/// Enumeración, lista los tipos de permisos
		/// </summary>
		public enum EnumPermissionsTypes
		{
			Menu = 1,
			Funcionalidad = 2
		}

		/// <summary>
		/// Enumeración, lista los tipos de permisos
		/// </summary>
		public enum EnumPermissions
		{
			AdicionarServicios = 19,
			ImprimirCertificadosConstancias = 22,
            ConsultarRetirados = 23,
            AdicionarMedicamentos = 24,
            RealizaConsultas = 33
		}

		#endregion
		
		#region Methods
		
		public Permissions()
		{
		}
	
		public Permissions(int IdPermission, string NamePermission, EnumPermissionsTypes IdPermissionType, int IdPermissionParent, string URL, short OrderPermission)
		{
			_IdPermission = IdPermission;
			_NamePermission = NamePermission;
			_IdPermissionType = IdPermissionType;
			_IdPermissionParent = IdPermissionParent;
			_URL = URL;
			_OrderPermission = OrderPermission;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultPermission()
		{
			DataSet dsList;
			try
			{
				dsList= this.List();
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return dsList;
		}

		/// <summary>
		/// Método para la consulta
		/// </summary>
		/// <returns>DataSet con los resultados de la consulta</returns>
		public DataSet ConsultPermissionsUser(int idUser)
		{
			DataSet dsList;
			try
			{
				dsList= this.List("UserPermissions", this.IdPermissionType, this.IdPermissionParent, this.Parent, idUser);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return dsList;
		}


		/// <summary>
		/// Método para la carga de un objeto de este tipo
		/// </summary>
		public void GetPermission()
		{
			try
			{
				this.Consult();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

        /// <summary>
        /// Método para la consulta de Menus
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultPermissionsUserGeneral(int idUser)
        {
            DataSet dsList;
            try
            {
                dsList = this.List("UserPermissionsGeneral", this.IdPermissionType, this.IdPermissionParent, this.Parent, idUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        /// <summary>
        /// Método caducar sessiones validas
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ExpireSesionUser(int idUser, string SessionId)
        {
            DataSet dsList;
            try
            {
                dsList = this.List("UserSessionExpire", idUser, SessionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        #endregion


    }
}


