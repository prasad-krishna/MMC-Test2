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
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Mercer.Medicines.DataAccess
{
	/// <summary>
	/// Esta clase provee la funcionalidad para realizar el llamado a procedimientos y el dinamic allocation entre objetos y parámetros
	/// </summary>
	/// <remarks>Autor: Adriana Diazgranados</remarks>
	/// <remarks>Fecha de creación: 6 de Octubre</remarks>	
    [Serializable]
    public class GeneralProcess
    {
        #region Attributes

        /// <summary> Atributo, Arreglo con los valores de los parámetros de los procedimiento almacenados </summary>
        [NonSerialized]
        private SqlParameter[] _arParams;
        /// <summary> Atributo, Objeto transacción para la realización de varios procedimientos almacenados </summary>
        [NonSerialized]
        private SqlTransaction _objTransaction;
        /// <summary> Atributo, Objeto conexión para utilizar en procedimientos almacenados en transacción </summary>
        [NonSerialized]
        private Connection _objConnection;
		/// <summary> Atributo, Tipo de conexión del listado de conexiones que se va a utilizar </summary>
		[NonSerialized]
		private Connection.EnumConnections _typeConnection;

        #endregion Atributos        
       
        #region Properties

        /// <summary>Propiedad, objeto que maneja la transacción en la base de datos</summary>
        [System.Xml.Serialization.XmlIgnore]
        public SqlTransaction objTransaction
        {
            get { return _objTransaction; }
            set { _objTransaction = value; }
        }

		/// <summary>
		/// Propiedad, arreglo de valores de los parámetros del procedimiento almacendado
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public SqlParameter[] arParams
		{
			get { return _arParams; }
			set { _arParams = value; }
		}

		/// <summary>
		/// Propiedad, objeto conexión para utilizar en procedimientos almacenados en transacción
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public Connection objConnection
		{
			get { return _objConnection; }
			set { _objConnection = value; }
		}

		/// <summary>
		/// Propiedad, String que indica la conexión a la base de datos
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public Connection.EnumConnections typeConnection
		{
			get { return _typeConnection; }
			set { _typeConnection = value; }
		}

        #endregion Properties
        
        #region Initializing
        /// <summary>
        /// Constructora
        /// </summary>
        protected GeneralProcess()
        {
            this.objTransaction = null;
            this.objConnection = null;
			this.typeConnection = Connection.EnumConnections.ConnectionReembolsos;
        }
        
        #endregion

        #region Methods

        #region Methods Database

        /// <summary>
        /// Método, permite inicializar una transacción para la ejecución de varios procedimientos almacenados
        /// </summary>        
        protected void BeginTransaction()
        {
            this.objConnection = new Connection(this.typeConnection);            
            this.objConnection.Open();
            this.objTransaction = this.objConnection.objConnection.BeginTransaction();
        }

        /// <summary>
        /// Método, realiza el commit de la transacción ya inicializada
        /// </summary>
        protected void CommitTransaction()
        {
            this.objTransaction.Commit();
            this.objConnection.Close();
            this.objTransaction.Dispose();
            this.objTransaction = null;
        }

        /// <summary>
        /// Método, realiza el rollback de una transacción ya inicializada
        /// </summary>
        protected void RollbackTransaction()
        {
            this.objTransaction.Rollback();
            this.objConnection.Close();
            this.objTransaction.Dispose();
            this.objTransaction = null;
        }
        
		/// <summary> 
        /// Método, Ejecuta un procedimiento donde los parámetros son las propiedades de la clase heradada. 
        /// </summary>
        /// <param name="p_procedimiento">Nombre del procedimiento a ejecutar</param>
        /// <returns>Respuesta del procedimiento almacenado</returns>
		protected Object ExecuteProcedure(string p_procedimiento)
		{
			Object objReturn = null;
            bool beginTransaction = false;            

			try
			{
               
                //Consultar parámetros del procedimiento
				arParams = SqlHelperParameterCache.GetSpParameterSet(Connection.ConnectionString(this.typeConnection), p_procedimiento);
				arParams = setDynamicAllocation(arParams, this);

                objReturn = this.ExecuteProcedure(p_procedimiento, arParams);             			

			}
			catch (Exception ex)
			{
                if (beginTransaction)
                    this.objTransaction.Rollback();
                throw new Exception(ex.Message);
			}
            finally
            {
                if (beginTransaction)
                {
                    this.objTransaction = null;
                    this.objConnection.Close();
                }
            }
            return objReturn;
		}

        /// <summary> Método, ejecuta un procedimiento con los parámetros recibidos. </summary>
        /// <param name="p_procedimiento">Nombre del procedimiento a ejecutar</param>
		/// <param name="arParams">Parametros del procedimiento</param>
        /// <returns>Respuesta del procedimiento almacenado</returns>
        protected Object ExecuteProcedure(string p_procedimiento, params object[] p_arParams)
		{
            
            Object objReturn = null;
            bool beginTransaction = false;

			try
			{
                if (this.objTransaction == null)
                {
                    this.BeginTransaction();
                    beginTransaction = true;
                }

                using (System.Data.IDataReader reader = SqlHelper.ExecuteReader(this.objTransaction, p_procedimiento, p_arParams))
                {
                    if (reader.Read())
                        objReturn = reader[0];
                    while (reader.NextResult()) ;
                    reader.Close();
                }              

                if (beginTransaction)
                    this.objTransaction.Commit();						

			}
            catch (Exception ex)
            {
                if (beginTransaction)
                    this.objTransaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (beginTransaction)
                {
                    this.objTransaction = null;
                    this.objConnection.Close();                    
                }
            }
            return objReturn;
		}

        /// <summary>
        /// Método, Ejecuta un procedimiento donde los parámetros son las propiedades de la clase heradada recibiendo una Transaction 
        /// inicializada anteriormente
        /// </summary>
        /// <param name="p_procedimiento">Nombre del procedimiento a ejecutar</param>
        /// <param name="objTrans">Objeto de la transacción</param>
        /// <returns>Respuesta del procedimiento almacenado</returns>
        protected Object ExecuteProcedure(string p_procedimiento, SqlTransaction p_objTrans)
        {
            Object objReturn = null;

            try
            {
                this.objTransaction = p_objTrans;
                objReturn = this.ExecuteProcedure(p_procedimiento);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return objReturn;
        }

        /// <summary>
        /// Método, Ejecuta un procedimiento con los parametros recibidos utilzando una Transaction 
        /// inicializada anteriormente
        /// </summary>
        /// <param name="p_procedimiento">Nombre del procedimiento a ejecutar</param>
        /// <param name="objTrans">Objeto de la transacción</param>
        /// <returns>Respuesta del procedimiento almacenado</returns>
        protected Object ExecuteProcedure(string p_procedimiento, SqlTransaction p_objTrans, params object[] p_arParams)
        {
            Object objReturn = null;

            try
            {
                this.objTransaction = p_objTrans;
                objReturn = this.ExecuteProcedure(p_procedimiento, p_arParams);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return objReturn;
            
        }


       
        /// <summary>
        /// Método, ejecuta un procedimiento de consulta donde los parámetros son las propiedades de la clase heradada 
        /// </summary>
        /// <param name="p_procedimiento">Nombre del procedimiento a ejecutar</param>
        /// <returns>Resultados de la consulta dentro del tipo de dato asignado a T (DataSet, DataTable, List)</returns>    
        protected DataSet consultarProc(string p_procedimiento)
        {
            //Consultar parámetros del procedimiento
            arParams = SqlHelperParameterCache.GetSpParameterSet(Connection.ConnectionString(this.typeConnection), p_procedimiento);
            arParams = setDynamicAllocation(arParams, this);

            return this.consultarProc(p_procedimiento, arParams);

        }
        /// <summary>
        /// Método, ejecuta un procedimiento de consulta donde los parámetros son las propiedades de la clase heradada 
        /// </summary>
        /// <param name="p_procedimiento">Nombre del procedimiento a ejecutar</param>
        /// <param name="arParams">Parametros del procedimiento</param>
        /// <returns>Resultados de la consulta dentro del tipo de dato asignado a T (DataSet, DataTable, ArrayList)</returns>      
        protected string EjecutarExecutescalarString(string p_procedimiento, params object[] p_arParams)
        {

            string Escalar = null;
            bool beginTransaction = false;

            try
            {
                if (this.objTransaction == null)
                {
                    this.BeginTransaction();
                    beginTransaction = true;
                }
                //Ejecutar Procedimiento
                Escalar = Convert.ToString(SqlHelper.ExecuteScalar(this.objTransaction, p_procedimiento, p_arParams));

                if (beginTransaction)
                    this.objTransaction.Commit();
            }
            catch (Exception ex)
            {
                if (beginTransaction)
                    this.objTransaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (beginTransaction)
                {
                    this.objTransaction = null;
                    this.objConnection.Close();
                }
            }
            return Escalar;
        }        

        /// <summary>
        /// Método, ejecuta un procedimiento de consulta donde los parámetros son las propiedades de la clase heradada 
        /// </summary>
        /// <param name="p_procedimiento">Nombre del procedimiento a ejecutar</param>
        /// <param name="arParams">Parametros del procedimiento</param>
        /// <returns>Resultados de la consulta dentro del tipo de dato asignado a T (DataSet, DataTable, ArrayList)</returns>      
        protected DataSet consultarProc(string p_procedimiento, params object[] p_arParams) 
        {            
           
            DataSet dsDatos = null;
            bool beginTransaction = false;

            try
            {
                if (this.objTransaction == null)
                {
                    this.BeginTransaction();
                    beginTransaction = true;
                }
                //Ejecutar Procedimiento
                dsDatos = SqlHelper.ExecuteDataset(this.objTransaction, p_procedimiento, p_arParams);                      
                     
                if (beginTransaction)
                    this.objTransaction.Commit();		
            }
            catch (Exception ex)
            {
                if (beginTransaction)
                    this.objTransaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (beginTransaction)
                {
                    this.objTransaction = null;
                    this.objConnection.Close();
                }
            }
            return dsDatos;
        }        

		/// <summary>
        /// Método, ejecuta un procedimiento donde los parámetros son las propiedades de la clase heradada 
        /// y el resultado lo asigna a las propiedades del objeto
        /// </summary>
		/// <param name="p_procedimiento">Nombre del procedimiento a ejecutar</param>    
        protected int Get(string p_procedimiento)
        {
            //Consultar parámetros del procedimiento
            arParams = SqlHelperParameterCache.GetSpParameterSet(Connection.ConnectionString(this.typeConnection), p_procedimiento);
            arParams = setDynamicAllocation(arParams, this);
            //Ejecutar procedimiento
            return this.Get(p_procedimiento, arParams);
        }


        /// <summary>
        /// Método, ejecuta un procedimiento donde los parámetros son las propiedades de la clase heradada 
        /// y el resultado lo asigna a las propiedades del objeto
        /// </summary>
        /// <param name="p_procedimiento">Nombre del procedimiento a ejecutar</param>    
        protected int Get(string p_procedimiento, params object[] p_arParams)
        {
            //Ejecutar procedimiento
            int fieldCount = 0;
            bool beginTransaction = false;
            try
            {
                if (this.objTransaction == null)
                {
                    this.BeginTransaction();
                    beginTransaction = true;
                }
                using (System.Data.IDataReader objIDataReader = SqlHelper.ExecuteReader(this.objTransaction, p_procedimiento, p_arParams))
                {
                    while (objIDataReader.Read())
                    {
                        setDynamicAllocation(objIDataReader, this);
                        fieldCount++;
                    }
                    objIDataReader.Close();
                }
                if (beginTransaction)
                    this.objTransaction.Commit();	               			
            }
            catch (Exception ex)
            {
                if (beginTransaction)
                    this.objTransaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (beginTransaction)
                {
                    this.objTransaction = null;
                    this.objConnection.Close();
                }
            }
            return fieldCount;
        }       

        /// <summary> Método, edita un registro de la clase que hereda en el sistema. </summary>		
		protected Object Update()
		{			
			return ExecuteProcedure("Update" + this.GetType().Name);
		}

        /// <summary> Método, edita un registro de la clase recibida. </summary>
        /// <param name="strClase">Nombre de la clase</param>
        protected Object Update(string p_complemento)
        {
            return ExecuteProcedure("Update" + p_complemento);
        }

        /// <summary>Método, modifica un registro de la clase que hereda en el sistema.</summary> 
        /// <param name="arParams">Parametros del procedimiento</param>
        protected Object Update(params object[] p_arParams)
        {
            return ExecuteProcedure("Update" + this.GetType().Name, p_arParams);
        }
        /// <summary>Método, modifica un registro de la clase recibida.</summary>
        /// <param name="strClase">Nombre de la clase</param>
        /// <param name="arParams">Parametros del procedimiento</param>
        protected Object Update(string p_complemento, params object[] p_arParams)
        {
            return ExecuteProcedure("Update" + p_complemento, p_arParams);
        }

		/// <summary>Método, crea una nuevo registro de la clase que hereda en el sistema. </summary>		
		protected Object Insert()
		{
            return ExecuteProcedure("Insert" + this.GetType().Name);
		}

        /// <summary>Método, crea una nuevo registro de la clase recibida. </summary>	
        /// <param name="strClase">Nombre de la clase</param>
        protected Object Insert(string p_complemento)
        {
            return ExecuteProcedure("Insert" + p_complemento);
        }

        /// <summary>Método, adiciona un registro de la clase que hereda en el sistema.</summary> 
        /// <param name="arParams">Parametros del procedimiento</param>
        protected Object Insert(params object[] p_arParams)
        {
            return ExecuteProcedure("Insert" + this.GetType().Name, p_arParams);
        }

        /// <summary>Método, adiciona un registro de la clase recibida.</summary>
        /// <param name="strClase">Nombre de la clase</param>
        /// <param name="arParams">Parametros del procedimiento</param>
        protected Object Insert(string p_complemento, params object[] p_arParams)
        {
            return ExecuteProcedure("Insert" + p_complemento, p_arParams);
        }

		/// <summary>Método, elimina un registro de la clase que hereda en el sistema.</summary>        
		protected Object Delete()
		{
            return ExecuteProcedure("Delete" + this.GetType().Name);
		}

        /// <summary>Método, elimina un registro de la clase que hereda en el sistema.</summary> 
        /// <param name="arParams">Parametros del procedimiento</param>
        protected Object Delete(params object[] p_arParams)
        {
            return ExecuteProcedure("Delete" + this.GetType().Name, p_arParams);
        }

        /// <summary>Método, elimina un registro de la clase recibida.</summary>
        /// <param name="strClase">Nombre de la clase</param>
        protected Object Delete(string p_complemento)
        {
            return ExecuteProcedure("Delete" + p_complemento);
        }

        /// <summary>Método, elimina un registro de la clase recibida.</summary>
        /// <param name="strClase">Nombre de la clase</param>
        /// <param name="arParams">Parametros del procedimiento</param>
        protected Object Delete(string p_complemento, params object[] p_arParams)
        {
            return ExecuteProcedure("Delete" + p_complemento, p_arParams);
        }

        /// <summary>
        /// Método, consulta los registros de la clase que hereda en el sistema.
        /// </summary>
        /// <typeparam name="T">Tipo de retorno (DataSet, DataTable, List)</typeparam>
        /// <returns>Listado en el tipo asigando a T (DataSet, DataTable, List)</returns>  
        protected DataSet List() 
		{
            return consultarProc("List" + this.GetType().Name);
		}

        /// <summary>
        /// Método, consulta los registros de la clase que hereda en el sistema.
        /// </summary>
        /// <typeparam name="T">Tipo de retorno (DataSet, DataTable, List)</typeparam>
        /// <param name="arParams">Parametros del procedimiento</param>
        /// <returns>Listado en el tipo asigando a T (DataSet, DataTable, List)</returns>  
        protected DataSet List(params object[] p_arParams) 
        {
            return consultarProc("List" + this.GetType().Name, p_arParams);
        }

        /// <summary>
        /// Método, consulta los registros de la clase que hereda en el sistema.
        /// </summary>
        /// <typeparam name="T">Tipo de retorno (DataSet, DataTable, List)</typeparam>
        /// <param name="strClase">Nombre de la clase</param>
        /// <returns>Listado en el tipo asigando a T (DataSet, DataTable, List)</returns>       
        protected DataSet List(string p_complemento) 
        {
            return consultarProc("List" + p_complemento);
        }

        /// <summary>
        /// Método, consulta los registros de la clase que hereda en el sistema.
        /// </summary>
        /// <typeparam name="T">Tipo de retorno (DataSet, DataTable, List)</typeparam>
        /// <param name="strClase">Nombre de la clase</param>
        /// <param name="arParams">Parametros del procedimiento</param>
        /// <returns>Listado en el tipo asigando a T (DataSet, DataTable, List)</returns>       
        protected DataSet List(string p_complemento, params object[] p_arParams) 
        {
            return consultarProc("List" + p_complemento, p_arParams);
        }

		/// <summary>
		/// Método, ejecuta un procedimiento que obtiene un registro tomando como argumentos las
		/// propiedades de la clase
		/// </summary>
        protected int Consult()
		{
            return Get("Get" + this.GetType().Name);
		}

        /// <summary>
        /// Método, ejecuta un procedimiento que obtiene un registro recibiendo los argumentos
        /// </summary>
        /// <param name="arParams">Parametros del procedimiento</param>
        protected int Consult(params object[] p_arParams)
        {
            return Get("Get" + this.GetType().Name, p_arParams);
        }

        /// <summary>
        /// Método, ejecuta un procedimiento que obtiene un registro tomando como argumentos las
        /// propiedades de la clase recibida
        /// </summary>
        /// <param name="strClase">Nombre de la clase</param>
        protected int Consult(string p_complemento)
        {
            return Get("Get" + p_complemento);
        }

        /// <summary>
        /// Método, ejecuta un procedimiento que obtiene un registro tomando como argumentos las
        /// propiedades de la clase recibida
        /// </summary>
        /// <param name="strClase">Nombre de la clase</param>
        protected int Consult(string p_complemento, params object[] p_arParams)
        {
            return Get("Get" + p_complemento, p_arParams);
        }
		
		/// <summary>
		/// Método, ejecuta un procedimiento que obtiene un registro tomando como argumentos las
		/// propiedades de la clase
		/// </summary>
		protected int ConsultSpecific(string p_complemento, params object[] p_arParams)
		{
			return Get(p_complemento, p_arParams);
		}
		
		#endregion

        #region Methods Dynamic Allocation
        
        #region DataReader to Object
        /// <summary>
        /// Método, realiza la asignación de los datos de un IDataReader a un objeto, para que se realice exitosamente 
        /// esta operación el nombre de las columnas deben tener el mismo nombre que las propiedades del objeto para que los valores
        /// sean asignados correctamente.  
        /// </summary>
        /// <param name="p_reader">IDataReader con los datos que se van a asignar</param>
        /// <param name="p_objDestiny">Objeto que va a recibir los valores del IDataReader</param>
        public void setDynamicAllocation(System.Data.IDataReader p_reader, Object p_objDestiny)
        {
            Object objValue;
            for (int j = 0; j < p_reader.FieldCount; j++)
            {
                string Get = p_reader.GetName(j);
                System.Reflection.PropertyInfo objPropertyInfo = p_objDestiny.GetType().GetProperty(Get, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (objPropertyInfo != null && p_reader[Get] != System.DBNull.Value)
                {
                    Type objType = objPropertyInfo.PropertyType;                   
                    if (!objType.IsEnum)
                        objValue = Convert.ChangeType(p_reader[Get], objType);
                    else
                        objValue = Enum.Parse(objType, p_reader[Get].ToString());
                    objPropertyInfo.SetValue(p_objDestiny, objValue, null);
                }
            }
        }
        #endregion

        #region DataRow to Object
        /// <summary>
        /// Método, realiza la asignación de los datos de un DataRow a un objeto, para que se realice exitosamente 
        /// esta operación el nombre de las columnas deben tener el mismo nombre que las propiedades del objeto para que los valores
        /// sean asignados correctamente.  
        /// </summary>
        /// <param name="p_dataRow">DataRow con los datos que se van a asignar</param>
        /// <param name="p_objDestiny">Objeto que va a recibir los valores del DataRow</param>
        public void setDynamicAllocation(DataRow p_dataRow, Object p_objDestiny)
        {
            string ColumnName = "";
            Object objValue;
            try
            {
                for (int i = 0; i < p_dataRow.Table.Columns.Count; i++)
                {
                    // Obtener nombre de la columna
                    ColumnName = p_dataRow.Table.Columns[i].ColumnName.Trim();
                    if (!Convert.IsDBNull(p_dataRow[ColumnName]))
                    {
                        // Buscar el nombre de la columna como propiedad en el objeto
                        System.Reflection.PropertyInfo objPropertyInfo = p_objDestiny.GetType().GetProperty(ColumnName, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                        Type objType = objPropertyInfo.PropertyType;                        
                        objValue = Convert.ChangeType(p_dataRow[ColumnName], objType);
                        if (objPropertyInfo != null)
                            objPropertyInfo.SetValue(p_objDestiny, objValue, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " Columna : " + ColumnName + " setDynamicAllocation(System.Data.DataSet DSExamine, Object ObjExamine)");
            }
        }
        #endregion        
        
        #region DataRow to SpParameters
        /// <summary>
        /// Método, realiza la asignación de los datos de un de un DataRow a un arreglo de parámetos SqlParamenter[], para que se realice exitosamente 
        /// esta operación el nombre de las columnas de DataRow deben tener el mismo nombre que los parámetros del arreglo.
        /// </summary>
        /// <param name="p_spParameters">Arreglo de parámetros que recibe los valores del DataRow</param>
        /// <param name="p_dataRow">DataRow con los datos que se van a asignar</param>
        /// <returns>Arreglo de parámetros con valores asignados</returns>
        public SqlParameter[] setDynamicAllocation(SqlParameter[] p_spParameters, DataRow p_dataRow)
        {
            try
            {
                foreach (SqlParameter objParameter in p_spParameters)

                    foreach (DataColumn objColumn in p_dataRow.Table.Columns)

                        if (objParameter.ParameterName.Substring(1, objParameter.ParameterName.Length).Equals(objColumn.ColumnName.Trim()))

                            objParameter.Value = p_dataRow[objColumn.ColumnName];

                return p_spParameters;
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }
        #endregion

        #region Object to SpParameters
        /// <summary>
        /// Método, realiza la asignación de las propiedades de un objeto a un arreglo de parámetos SqlParamenter[], para que se realice exitosamente 
        /// esta operación el nombre de las propiedades del objeto deben tener el mismo nombre que los parámetros del arreglo.
        /// </summary>
        /// <param name="p_spParameters">Arreglo de parámetros que recibe los valores del objeto</param>
        /// <param name="p_objOrigin">Objeto con los valores que se van a asignar</param>
        /// <returns>Arreglo de parámetros con valores asignados</returns>
        public SqlParameter[] setDynamicAllocation(SqlParameter[] p_spParameters, object p_objOrigin)
        {
            PropertyDescriptorCollection Properties = TypeDescriptor.GetProperties(p_objOrigin);

            try
            {
                foreach(SqlParameter objParameter in p_spParameters)
                    foreach(PropertyDescriptor objProperty in Properties)
                        if (objParameter.ParameterName.Substring(1).ToUpper().Trim().Equals(objProperty.Name.ToUpper().Trim()))
                        {
                            objParameter.Value = objProperty.GetValue(p_objOrigin);
                            break;
                        }
                return p_spParameters;
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
        }
        #endregion        
        
        #endregion Dynamic Allocation

        #endregion
    }
}
