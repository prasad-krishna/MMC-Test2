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
using System.Data.OleDb;
using System.Configuration;
using System.Web.Mail;
using Mercer.Medicines.DataAccess;


namespace Mercer.Medicines.Logic
{
    /// <summary>
    /// Clase que provee la funcionalidad para el envio de correos
    /// </summary>}
	/// <remarks>Autor: Adriana Diazgranados</remarks>
	/// <remarks>Fecha de creación: 17 de octubre de 2008</remarks>
    public class Mails : GeneralProcess
    {
        #region Atributos
		/// <summary>Atributo, identificador del correo.</summary>
		private long _IdMail;
		/// <summary>Atributo, Subject del correo.</summary>
		private string _Subject;
		/// <summary>Atributo, cuerpo del correo.</summary>
		private string _Content;
		/// <summary>Atributo, NameMail del correo.</summary>
		private string _NameMail;		
		/// <summary>Atributo, destinatario del correo.</summary>
		private string _MailTo;
		/// <summary>Atributo, destinatario copia del correo.</summary>
		private string _MailCc;		
		/// <summary>Atributo, archivo anexo del correo.</summary>
		private string _Attachment;
		
		#endregion

        #region Enumeraciones

        /// <summary>
        /// Enumeración, lista los mails que se envian a los usuarios
        /// </summary>
        public enum enumMail : long
        {
            /// <summary>
            /// Especifica si el usuario olvido su contraseña
            /// </summary>
            OlvidoContrasena = 1
            
        }

        #endregion 
				
		#region Propiedades
        /// <summary>Propiedad, identificador del correo en el sistema.</summary>	
		public long IdMail
		{
			get { return _IdMail;}
			set { _IdMail = value; }
		}
		/// <summary>Propiedad, Asunto del correo.</summary>	
		public string Subject
		{
			get { return _Subject;}
			set { _Subject = value; }
		}
		/// <summary>Propiedad, Nombre para identificar el correo.</summary>	
		public string NameMail
		{
			get { return _NameMail;}
			set { _NameMail = value; }
		}
		/// <summary>Propiedad, Contenido del correo.</summary>	
		public string Content
		{
			get { return _Content;}
			set { _Content = value; }
		}
		/// <summary>Propiedad, email del destinatario del correo.</summary>	
		public string MailTo
		{
			get { return _MailTo;}
			set { _MailTo = value; }
		}
		/// <summary>Propiedad, email para enviar copia del correo.</summary>	
		public string MailCc
		{
			get { return _MailCc;}
			set { _MailCc = value; }
		}
		/// <summary>Propiedad, Nombbre del archivo adjunto que lleva el correo.</summary>	
		public string Attachment
		{
			get { return _Attachment;}
			set { _Attachment = value; }
		}		
		#endregion
		
		#region Métodos

		public Mails()
		{
		}

		/// <summary>Método, envia un correo electrónico.</summary>		
		public void sendMail()
		{
			try
			{
                MailMessage objMail = new MailMessage();							
				objMail.To = this.MailTo;
				objMail.Body = this.Content;
				objMail.Subject = this.Subject;
                objMail.BodyFormat = MailFormat.Html;
                
				if (this.MailCc.Length > 0)
				{
					objMail.Cc = this.MailCc;
				}

				if(this.Attachment.Length > 0)
				{
					objMail.Attachments.Add(this.Attachment);
				}
                
				System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
				string smptServer = ((string)(configurationAppSettings.GetValue("mail.ServerMail", typeof(string))));

				SmtpMail.SmtpServer = smptServer;	
				System.Web.Mail.SmtpMail.Send(objMail);


			}
			catch(Exception ex)
			{
                throw ex;
			}
		}		

		/// <summary>
		/// Método para la carga de un objeto de este tipo
		/// </summary>
		public void GetMails()
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

		#endregion
    }
}
