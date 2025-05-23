﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CertificateManager
{
    public enum HashAlgorithm { SHA1 }
    public class DigitalSignature
    {

        public static byte[] Create(string message, HashAlgorithm hashAlgorithm, X509Certificate2 certificate)
        {
            /// Looks for the certificate's private key to sign a message
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)certificate.PrivateKey;

            if (csp == null)
            {
                throw new Exception("Valid certificate was not found.");
            }
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] data = encoding.GetBytes(message);
            byte[] hash = null;

            if (hashAlgorithm.Equals(HashAlgorithm.SHA1))
            {
                SHA1Managed sha1 = new SHA1Managed();
                hash = sha1.ComputeHash(data);
            }

            /// Use RSACryptoServiceProvider support to create a signature using a previously created hash value
            byte[] signature = csp.SignHash(hash, CryptoConfig.MapNameToOID(hashAlgorithm.ToString()));
            return signature;
        }


        public static bool Verify(string message, HashAlgorithm hashAlgorithm, byte[] signature, X509Certificate2 certificate)
        {
            /// Looks for the certificate's public key to verify a message
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)certificate.PublicKey.Key;

            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] data = encoding.GetBytes(message);
            byte[] hash = null;

            if (hashAlgorithm.Equals(HashAlgorithm.SHA1))
            {
                SHA1Managed sha1 = new SHA1Managed();
                hash = sha1.ComputeHash(data);
            }

            /// Use RSACryptoServiceProvider support to compare two - hash value from signature and newly created hash value
            return csp.VerifyHash(hash, CryptoConfig.MapNameToOID(hashAlgorithm.ToString()), signature);
        }
    }
}
