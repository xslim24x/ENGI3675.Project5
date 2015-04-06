//-----------------------------------------------------------------------
// <copyright file="Rainbow.cs" company="LakeheadU">
//     Copyright ENGI-3675. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace RainbowLookup
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Rainbow Attack Class
    /// </summary>
    public class Rainbow
    {
        /// <summary>
        /// Beginning of console application used to perform a rainbow attack against MD5 hashes
        /// User is asked for MD5 hash string and the program uses password list of english words to generate hashes
        /// </summary>
        private static void Main()
        {
            MD5 crypter = MD5.Create();
            string hash = string.Empty;
            Console.WriteLine("Enter a password hash:");
            while (hash.Length == 0)
            {
                hash = Console.ReadLine();
            }

            string[] passdictionary = System.IO.File.ReadAllLines(@"C:\Users\Slim\Documents\Visual Studio 2013\Projects\Project 5 - ENGI3675\ENGI3675.Project5\RainbowLookup\passwordlist.txt");
            bool found = false;
            foreach (string word in passdictionary)
            {
                Console.WriteLine("Trying: " + word);
                byte[] test = crypter.ComputeHash(System.Text.Encoding.Default.GetBytes(word));
                string encrypted = BitConverter.ToString(test).Replace("-", string.Empty);
                if (hash.ToUpper() == encrypted.ToUpper())
                {
                    Console.Write("\n\nFound word!\nPassword is: " + word);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Sorry password is not in the dictionary!");
            }
            
            Console.ReadKey();
        }
    }
}
