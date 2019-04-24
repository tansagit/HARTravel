using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HARTravel.Models
{
    public class MySecurity
    {
        public static string EncryptedPassword(string pass)
        {
            SHA256 sha = SHA256.Create();
            byte[] data = Encoding.UTF8.GetBytes(pass);     //convert tu string qua byte array
            byte[] en_data = sha.ComputeHash(data);         //da encryted
            return BitConverter.ToString(en_data).Replace("-", "").ToLower();          //chuyen ra chuoi string
        }
    }
}