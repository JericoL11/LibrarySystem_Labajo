//required libraries
using System;
using System.Security.Cryptography;
using System.Text;


namespace LibrarySystem_Labajo.Services
{
    public class HashingService
    {
        //HASH METHOD
        public static string HashData(string userData)
        {
            //CREATING SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                //incoding
                byte[] inputBytes = Encoding.UTF8.GetBytes(userData);

                //computing
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
               
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < 5; i++)
                {
                    
                    //convert each byte to its hexadecimal representation
                    //assigning hashcode
                    builder.Append(hashBytes[i]).ToString();

                }
                //passing to main
                return builder.ToString();
            }
        }
    }
}
