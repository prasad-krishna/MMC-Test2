using System;
using System.Data;
using System.Text;
using Mercer.Medicines.DataAccess;

namespace Mercer.Medicines.Logic
{
    /// <summary>
    /// Esta clase provee la funcionalidad para registrar si el paciente firmo el aviso de privacidad
    /// </summary>
    /// <remarks>Autor: Ricardo Silva</remarks>
    /// <remarks>Fecha de creación: 05/10/2011</remarks>
    public class SIC_PRIVACIDAD : GeneralProcess
    {
        #region Attributes

            /// <summary>Atributo, id de el registro de la firma de privacidad </summary>
            private int _Id_privacidad;
            /// <summary>Atributo, id del empleado que firmará el aviso de seguridad</summary>
            private int _Id_empleado;
            /// <summary>Atributo, id del paciente beneficiario que firmará el aviso de seguridad </summary>
            private int _Beneficiario_id;
            /// <summary>Atributo, bit que indica si el paciente firmo el aviso </summary>
            private bool _Firma;
            /// <summary>Atributo, fecha en la que el paciente firmo el aviso de privacidad</summary>
            private DateTime _Fecha_firma;
            /// <summary>Atributo, Ultima fecha en la que el paciente firmo el aviso de privacidad</summary>
            private String _fechaUltimaFirma;

        #endregion

        #region Properties

            /// <summary>Propiedad, id de el registro de la firma de privacidad</summary>
            public int Id_privacidad
            {
                get { return _Id_privacidad; }
                set { _Id_privacidad = value; }
            }
            /// <summary>Propiedad, id del empleado que firmará el aviso de seguridad</summary>
            public int Id_empleado
            {
                get { return _Id_empleado; }
                set { _Id_empleado = value; }
            }
            /// <summary>Propiedad, id del paciente beneficiario que firmará el aviso de seguridad</summary>
            public int Beneficiario_id
            {
                get { return _Beneficiario_id; }
                set { _Beneficiario_id = value; }
            }
            /// <summary>Propiedad, bit que indica si el paciente firmo el aviso</summary>
            public bool Firma
            {
                get { return _Firma; }
                set { _Firma = value; }
            }
            /// <summary>Propiedad, fecha en la que el paciente firmo el aviso de privacidad</summary>
            public DateTime Fecha_firma
            {
                get { return _Fecha_firma; }
                set { _Fecha_firma = value; }
            }
            /// <summary>Propiedad, Ultima fecha en la que el paciente firmo el aviso de privacidad</summary>
            public String fechaUltimaFirma
            {
                get { return _fechaUltimaFirma; }
                set { _fechaUltimaFirma = value; }
            }

        #endregion 

        #region Methods

            /// <summary>
            /// Método para la inserción
            /// </summary>
            /// <returns>Id insertado</returns>
            public int InsertSIC_PRIVACIDAD()
            {
                int id;
                try
                {
                    id = Convert.ToInt32(this.Insert());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return id;
            }


            /// <summary>
            /// Método para la carga de un objeto de este tipo
            /// </summary>
            public void GetSIC_PRIVACIDAD(int p_id_beneficiario, int p_id_empleado)
            {
                try
                {
                    this.Consult(p_id_beneficiario, p_id_empleado);
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            /// <summary>
            /// Método para la eliminación
            /// </summary>
            public void DeleteSIC_PRIVACIDAD(int p_id_beneficiario, int p_id_empleado)
            {
                try
                {
                    this.Delete(p_id_beneficiario, p_id_empleado);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        #endregion
    }
}
