using System;
using System.Net.Mail;
using System.Collections;
using System.Text;
using System.IO;
using System.Net.Mime;
using System.Data;
using System.Web;
using Mercer.Medicines.DataAccess;
/// <summary>
///Auto:Diego Montejano Avila
///Proyecto: Auditoria 2014
///Fecha: 2014/09/17
///Observaciones: Se copio de SICAM para enviar el mail de reseteo
/// </summary>
public class Mail : GeneralProcess
{
    #region Atributos

    private string mTxtEmailTo;  // Emailaddres for user to send
    private string mTxtEmailCc;
    private string mTxtEmailFrom;// Emailaddres for user from 
    private string mTxtMailSubj;
    private string mTxtMailBody;
    private string mTxtMailFileAttach;
    private object objAttachment;
    private string mTxtServerName;
    private string strSMTPServer;
    private string strNameAttachment;
    private string strDominio;



    #endregion
    #region Enumerados
    public enum TiposMail
    {
        ResetPassword = 1,
        SendUser = 2,
        SendPassword = 3
    }
    #endregion
    #region Propiedades

    public string Dominio
    {
        get { return this.strDominio; }
        set { this.strDominio = value; }
    }
    public string EmailFrom
    {
        set
        {
            mTxtEmailFrom = value;
        }
        get
        {
            return mTxtEmailFrom;
        }
    }
    public string EmailTo
    {
        set
        {
            mTxtEmailTo = value;
        }
        get
        {
            return mTxtEmailTo;
        }
    }
    public string EmailCc
    {
        set
        {
            mTxtEmailCc = value;
        }
        get
        {
            return mTxtEmailCc;
        }
    }
    public string MailSubject
    {
        set
        {
            mTxtMailSubj = value;
        }
        get
        {
            return mTxtMailSubj;
        }
    }
    public string MailBody
    {
        set
        {
            mTxtMailBody = value;
        }
        get
        {
            return mTxtMailBody;
        }
    }
    public string PathFileAttach
    {
        set
        {
            mTxtMailFileAttach = value;
        }
        get
        {
            return mTxtMailFileAttach;
        }
    }
    public Object Attachment
    {
        get { return objAttachment; }
        set { this.objAttachment = value; }
    }
    public string NameAttachment
    {
        get { return this.strNameAttachment; }
        set { this.strNameAttachment = value; }
    }
    public TiposMail TipoMail { get; set; }
    #endregion

    public Mail()
    {
        this.strSMTPServer = System.Configuration.ConfigurationManager.AppSettings["ServidorSmtp"].ToString();
    }
    /// <summary>
    /// Obtiene los parametros de la base de datos
    /// </summary>
    public void CargaParametros()
    {
        try
        {
            DataSet dsParametrosMail = this.List();
            DataTable dtParametrosMail = new DataTable();

            if (dsParametrosMail.Tables.Count == 0)
                throw new Exception("No se han configurado los parametros del correo");
            if (dsParametrosMail.Tables[0].Rows.Count == 0)
                throw new Exception("No se han configurado los parametros del correo");
            dtParametrosMail = dsParametrosMail.Tables[0];

            this.MailSubject = dtParametrosMail.Rows[0]["emailSubject"].ToString();
            this.MailBody = dtParametrosMail.Rows[0]["emailBody"].ToString();
            this.EmailFrom = dtParametrosMail.Rows[0]["MailFrom"].ToString();

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public void SendNow()
    {

        #region Declaración de variables

        SmtpClient smtp;
        MailMessage msg;
        MemoryStream stream;


        #endregion

        #region Inicialización de variables

        msg = new MailMessage();
        smtp = new SmtpClient();
        smtp.Host = this.strSMTPServer;
        //msg.BodyEncoding = Encoding.GetEncoding("8859-1");

        #endregion

        #region Destinatarios

        msg.From = new MailAddress(mTxtEmailFrom);

        foreach (string strMail in mTxtEmailTo.Split(';'))
        {
            if (strMail.Trim() != "")
            {
                msg.To.Add(new MailAddress(strMail));
            }
        }

        if (mTxtEmailCc != null)
        {
            foreach (string strMail in mTxtEmailCc.Split(';'))
            {
                if (strMail.Trim() != "")
                {
                    msg.CC.Add(strMail);
                }
            }
        }

        #endregion|

        #region Asunto y cuerpo del mensaje

        msg.Subject = mTxtMailSubj;

        msg.Body = mTxtMailBody;

        #endregion

        #region Attachment

        if (this.objAttachment != null)
        {
            stream = new MemoryStream(GetData(this.objAttachment));

            msg.Attachments.Add(new Attachment(stream, this.strNameAttachment, MediaTypeNames.Application.Rtf));
        }

        //MAHG
        //Se realiza un split a la variable que contiene el nombre de los archivos que serán adjuntados al correo
        //
        //<--

        if (mTxtMailFileAttach != "" && mTxtMailFileAttach != null)
        {
            foreach (string strFile in mTxtMailFileAttach.Split(';'))
            {
                if (strFile != "")
                {
                    msg.Attachments.Add(new Attachment(strFile));
                }
            }
        }

        //-->

        #endregion

        msg.IsBodyHtml = true;
        //smtp.EnableSsl = true;
        smtp.Send(msg);
    }
    public static byte[] GetData(object objAttachment)
    {
        Encoding objCodificacion = Encoding.GetEncoding("iso-8859-1");


        byte[] data = objCodificacion.GetBytes(objAttachment.ToString());
        return data;
    }
}