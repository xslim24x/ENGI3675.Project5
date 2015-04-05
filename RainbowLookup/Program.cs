using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RainbowLookup
{

    class Program
    {
        
        static void Main(string[] args)
        {
            MD5 crypter = MD5.Create();
            string hash = string.Empty;
            Console.WriteLine("Enter a password hash:");
            while(hash.Length==0)
                hash = Console.ReadLine();

            string[] passdictionary = System.IO.File.ReadAllLines(@"C:\Users\Slim\Documents\Visual Studio 2013\Projects\Project 5 - ENGI3675\ENGI3675.Project5\RainbowLookup\passwordlist.txt");
            bool found = false;
            foreach (string word in passdictionary)
            {
                Console.WriteLine("Trying: " + word);
                byte [] test = crypter.ComputeHash(System.Text.Encoding.Default.GetBytes(word));
                string encrypted = BitConverter.ToString(test).Replace("-","");
                if (hash.ToUpper() == encrypted.ToUpper()){
                    Console.Write("\n\nFound word!\nPassword is: "+word);
                    found = true;
                    break;
                }
            }

            if (!found)
                Console.WriteLine("Sorry password is not in the dictionary!");
            
            Console.ReadKey();
        }
    }
}
