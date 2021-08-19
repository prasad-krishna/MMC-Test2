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
using System.Collections;

namespace Mercer.Medicines.Logic
{
    /// <summary>
    /// Esta clase provee la funcionalidad para administrar los usuarios que ingresan al sistema
    /// </summary>
    /// <remarks>Autor: Adriana Diazgranados</remarks>
    /// <remarks>Fecha de creación: Octubre 3 de 2008</remarks>
    public class Users : GeneralProcess
    {
        #region Attributes

        /// <summary>Atributo, Id del usuario</summary>
        private int _IdUser;
        /// <summary>Atributo, Id de la compañía a la que pertenece el usuario</summary>
        private int _empresa_id;
        /// <summary>Atributo, Nombre del usuario</summary>
        private string _NameUser;
        /// <summary>Atributo, Login para ingreso al sistema</summary>
        private string _Login;
        /// <summary>Atributo, Contaseña para ingreso al sistema</summary>
        private string _Password;
        /// <summary>Atributo, Email del usuario</summary>
        private string _Email;
        /// <summary>Atributo, Indica si se encuentra activo</summary>
        private bool _Active;
        /// <summary>Atributo, Id de la ciudad del usuario</summary>
        private int _IdCiudad;
        /// <summary>Atributo, Id si el usuario corresponde a un prestador o solicitante</summary>
        private int _IdPrestador;
        /// <summary>Atributo, Cantidad de accesos fallidos en login</summary>
        private int _AccessUnsuccessful;
        /// <summary>Atributo, Ultima fecha en que modificó password</summary>
        private DateTime _DateLastPassword;
        /// <summary>Atributo, Indica si el password está bloqueado</summary>
        private bool _Blocking;

        /*Inicio 18/01/10 MAHG Marco A. Herrera G.*/
        /// <summary>
        /// Atributo, Indica si el password se encuentra encriptado con SHA256
        /// </summary>
        private bool _NewPassword;
        private bool _ExpiredPassword;
        private string _PasswordCreated;
        private string _PasswordExpired;

        /*Fin 18/01/10 MAHG Marco A. Herrera G.*/
        // MFA code added Bhushan
        private string _BypassMfa;

        //OptionUser code added GAMM
        private bool _MostrarOpcionUsuarios;

        #endregion

        #region Properties

        /// <summary>Propiedad, Id del usuario</summary>
        public int IdUser
        {
            get { return _IdUser; }
            set { _IdUser = value; }
        }
        /// <summary>Propiedad, Id de la compañía a la que pertenece el usuario</summary>
        public int empresa_id
        {
            get { return _empresa_id; }
            set { _empresa_id = value; }
        }
        /// <summary>Propiedad, Nombre del usuario</summary>
        public string NameUser
        {
            get { return _NameUser; }
            set { _NameUser = value; }
        }
        /// <summary>Propiedad, Login para ingreso al sistema</summary>
        public string Login
        {
            get { return _Login; }
            set { _Login = value; }
        }
        /// <summary>Propiedad, Contaseña para ingreso al sistema</summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        /// <summary>Propiedad,  Email del usuario</summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        /// <summary>Propiedad, Indica si se encuentra activo</summary>
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
        /// <summary>Propiedad, Id de la ciudad del usuario</summary>
        public int IdCiudad
        {
            get { return _IdCiudad; }
            set { _IdCiudad = value; }
        }
        /// <summary>Propiedad,  Id si el usuario corresponde a un prestador o solicitante</summary>
        public int IdPrestador
        {
            get { return _IdPrestador; }
            set { _IdPrestador = value; }
        }
        /// <summary>Propiedad, Cantidad de accesos fallidos en login</summary>
        public int AccessUnsuccessful
        {
            get { return _AccessUnsuccessful; }
            set { _AccessUnsuccessful = value; }
        }
        /// <summary>Propiedad, Ultima fecha en que modificó password</summary>
        public DateTime DateLastPassword
        {
            get { return _DateLastPassword; }
            set { _DateLastPassword = value; }
        }
        /// <summary>Propiedad, Indica si el password está bloqueado</summary>
        public bool Blocking
        {
            get { return _Blocking; }
            set { _Blocking = value; }
        }

        /*Inicio 18/01/10 MAHG Marco A. Herrera G.*/
        /// <summary>
        /// Propiedad, Indica si el password se encuentra encriptado con SHA256
        /// </summary>
        public bool NewPassword
        {
            get { return this._NewPassword; }
            set { this._NewPassword = value; }
        }

        public bool ExpiredPassword
        {
            get { return _ExpiredPassword; }
            set { _ExpiredPassword = value; }
        }

        public string PasswordCreated
        {
            get { return this._PasswordCreated; }
            set { this._PasswordCreated = value; }
        }

        /// <summary>
        /// Password expirado a las 72 horas. Se utiliza esta variable para saber cuando validar las 72 Horas.
        /// </summary>
        public string PasswordExpired
        {
            get { return this._PasswordExpired; }
            set { this._PasswordExpired = value; }
        }
        //Fin 18/01/10 MAHG
        /// <summary>
        /// Auto:Diego Montejano Avila
        /// Proyecto: Auditoria 2014
        /// Fecha: 2014/09/19
        /// Asigna un valor falso o verdadero para saber si el psw de los usuarios fue reseteado en la pantalla de loguin
        /// </summary>
        public bool ResetPassword
        {
            get;
            set;
        }
        /// <summary>
        /// Auto:Diego Montejano Avila
        /// Proyecto: Auditoria 2014
        /// Fecha: 2014/09/19
        /// Asigna un valor falso o verdadero para saber si el psw de los usuarios fue reseteado en la pantalla de loguin
        /// </summary>
        public bool GuardarHistoricoContrasena
        {
            get;
            set;
        }
        /// <summary>
        /// MFA code added Bhushan
        public string BypassMFA
        {
            get { return _BypassMfa; }
            set { _BypassMfa = value; }
        }
        /// </summary>
        /// 
        /// /// <summary>
        /// UserOption code added GAMM
        public bool MostrarOpcionUsuarios
        {
            get { return _MostrarOpcionUsuarios; }
            set { _MostrarOpcionUsuarios = value; }
        }
        /// </summary>

        #endregion

        #region Methods

        public Users()
        {
            this.GuardarHistoricoContrasena = true;
        }

        /// <summary>
        /// Método para la consulta
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet ConsultUsers()
        {
            DataSet dsList;
            try
            {
                dsList = this.List();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        /// <summary>
        /// Método para la inserción
        /// </summary>
        /// <returns>Id insertado</returns>
        /// 
        /*Proyecto: AMEX
                     Requerimiento: Permisos de crear usuarios
                     Funcionalidad: Permite asociar permisos y reportes a los usuarios
                     Autor: Ricardo Jose Silva Gomez
                     Fecha: 07/06/2011                     
                   */
        //Inicio RJSG 07/06/2011

        public int InsertUsers(ArrayList lstPermissions, ArrayList lstReportes, ArrayList lstTipoServicios)
        {
            int id;
            try
            {
                this.BeginTransaction();

                id = Convert.ToInt32(this.Insert());


                if (lstTipoServicios != null)
                {

                    foreach (int IdTipoServicios in lstTipoServicios)
                    {
                        this.Insert("UserTipoServicios", id, IdTipoServicios);
                    }
                }

                if (lstReportes != null)
                {

                    foreach (int idReporte in lstReportes)
                    {
                        this.Insert("UserReportes", id, idReporte);
                    }
                }


                if (lstPermissions != null)
                {

                    foreach (int idPermission in lstPermissions)
                    {
                        this.Insert("UserPermissions", id, idPermission);
                    }
                }

                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }
            return id;
        }

        //Fin RJSG 07/06/2011


        /// <summary>
        /// Método para la modificación
        /// </summary>
        public void UpdateUsers(ArrayList lstPermissions, ArrayList lstReportes, ArrayList lstTipoServicios)
        {
            try
            {
                this.BeginTransaction();

                this.Update();

                if (lstPermissions != null)
                    this.Delete("UserPermissions", this.IdUser);

                if (lstPermissions != null)
                {
                    foreach (int idPermission in lstPermissions)
                    {
                        this.Insert("UserPermissions", this.IdUser, idPermission);
                    }
                }

                //Recarga la lista de reportes seleccionados 
                if (lstReportes != null)
                    this.Delete("UserReportes", this.IdUser);

                if (lstReportes != null)
                {
                    foreach (int idReporte in lstReportes)
                    {
                        this.Insert("UserReportes", this.IdUser, idReporte);
                    }
                }

                //Recarga la lista de tipo servicio seleccionados 
                if (lstTipoServicios != null)
                    this.Delete("UserTipoServicios", this.IdUser);

                if (lstTipoServicios != null)
                {
                    foreach (int IdTipoServicios in lstTipoServicios)
                    {
                        this.Insert("UserTipoServicios", this.IdUser, IdTipoServicios);
                    }
                }

                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Método para la eliminación
        /// </summary>
        public void DeleteUsers()
        {
            try
            {
                this.Delete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para la carga de un objeto de este tipo
        /// </summary>
        public void GetUsers()
        {
            try
            {
                this.Consult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Método para la modificación
        /// </summary>
        public void UpdateUsersAccess(object p_AccessUnsuccessful, object p_DateLastPassword, object p_Blocking)
        {
            try
            {
                this.Update("UserAccess", this.IdUser, p_AccessUnsuccessful, p_DateLastPassword, p_Blocking);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para la modificación
        /// </summary>
        public void UpdatePasswordExpired()
        {
            try
            {
                this.BeginTransaction();

                this.Update("PasswordExpired");

                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }
        }
        /// <summary>
        /// Proyecto: AMEX
        /// Autor: Marco A. Herrera Gabriel
        /// Fecha: 22/02/2010
        /// Funcionalidad: Valida que la contraseña del usuario no sea igual a la contraseña temporal
        /// </summary>
        /// <returns>Booleano: Indica si la contraseña del usuario es igual a la contraseña temporal</returns>
        public bool PasswordInicial()
        {
            if (this.PasswordExpired != "" && this.PasswordExpired != null)//Validación para los usuarios ya existentes
            {
                return Security.VerifyHash(this.Password, this.PasswordExpired);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método para grabar session
        /// </summary>
        public void InsertUsersSession(object p_SessionId)
        {
            try
            {
                this.Insert("UserSession", this.IdUser, p_SessionId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para validar Session
        /// </summary>
        /// <returns>DataSet con los resultados de la consulta</returns>
        public DataSet InsertUserSession(object p_SessionId)
        {
            DataSet dsList;
            try
            {
                dsList = this.List("InsertUserSession", this.IdUser, p_SessionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        public void UsersSession(int idUser, string strSession, string strBrowser)
        {
            try
            {
                this.BeginTransaction();

                this.Insert("UsersSession", idUser, strSession, strBrowser);

                this.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw ex;
            }
        }

        public DataSet ConsultSession(int idUser, string Session)
        {
            DataSet dsList;

            try
            {
                dsList = this.List("UsersValidateSession", idUser, Session);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsList;
        }

        public void UpdateUsersSession(int idUser, string strSession, int origen)
        {
            try
            {
                this.Update("UsersSession", idUser, strSession, origen);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

    }
}


