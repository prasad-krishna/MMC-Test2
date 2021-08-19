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
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Mercer.Medicines.Logic
{
	/// <summary>
	/// Esta clase provee la funcionalidad para encriptar datos
	/// </summary>
	/// <remarks>Autor: Adriana Diazgranados</remarks>
	/// <remarks>Fecha de creación: Octubre 3 de 2008</remarks>
	public class Security
	{
		public Security()
		{			
		}

		/// <summary>
		/// Método, ejecuta el algoritmo de encripción sobre el texto
		/// </summary>
		/// <param name="p_text">Texto a encriptar</param>
		/// <returns>Texto encriptado</returns>
		public string EncryptString(string p_text)
		{
			Byte[] dataToHash = ConvertStringToByteArray(p_text);    
			//crear hash
			byte[] hashValue = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(dataToHash);


			return BitConverter.ToString(hashValue);
		}

		/// <summary>
		/// Método, retorna el texto en un arreglo de bytes
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static Byte[] ConvertStringToByteArray(string p_text)
		{
			return (new UnicodeEncoding()).GetBytes(p_text);
		}
        
        #region Algoritmos Hash + Salt

        #region Referencia Microsoft http://msdn.microsoft.com/en-us/library/ms998372.aspx

        public static string SHA256_Encrypt(string plainText, byte[] salt)
        {
            if (salt == null)
            {
                salt = new byte[32];
                System.Security.Cryptography.RNGCryptoServiceProvider.Create().GetBytes(salt);
            }

            // Convert the plain string password into bytes
            byte[] plainTextBytes = UnicodeEncoding.Unicode.GetBytes(plainText);

            // Append salt to password before hashing
            byte[] combinedBytes = new byte[plainTextBytes.Length + salt.Length];
            System.Buffer.BlockCopy(plainTextBytes, 0, combinedBytes, 0, plainTextBytes.Length);
            System.Buffer.BlockCopy(salt, 0, combinedBytes, plainTextBytes.Length, salt.Length);

            // Create hash for the password+salt
            System.Security.Cryptography.HashAlgorithm hashAlgo = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = hashAlgo.ComputeHash(combinedBytes);

            // Append the salt to the hash 
            byte[] hashPlusSalt = new byte[hash.Length + salt.Length];
            System.Buffer.BlockCopy(hash, 0, hashPlusSalt, 0, hash.Length);
            System.Buffer.BlockCopy(salt, 0, hashPlusSalt, hash.Length, salt.Length);

            string hashValue = Convert.ToBase64String(hashPlusSalt);

            return hashValue;
        }


        private static byte[] getSalt(string hashWithSalt)
        {
            // Convert base64-encoded hash value into a byte array.

            byte[] hashWithSaltBytes = Convert.FromBase64String(hashWithSalt);

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[32];

            // Copy salt from the end of the hash to the new array.

            System.Buffer.BlockCopy(hashWithSaltBytes, hashWithSaltBytes.Length - saltBytes.Length,
                saltBytes, 0, saltBytes.Length);

            return saltBytes;
        }

        /// <summary>
        /// Autor: Marco A. Herrera Gabriel MAHG 
        /// Fecha: 24/09/09
        /// Función: Encripta un texto plano y compara si este es igual al texto encriptado
        /// </summary>
        /// <param name="plainText">Texto original</param>
        /// <param name="hashWithSalt">Texto hash con salt</param>
        /// <returns></returns>
        public static bool VerifyHash(string plainText, string hashWithSalt)
        {
            return SHA256_Encrypt(plainText, Security.getSalt(hashWithSalt)) == hashWithSalt;
        }
        
        #endregion

        #endregion               

	}
}
