using System;
using System.IO;
using BL;

namespace PL_Console_
{
    class Program
    {

        static void Main(string[] args)
        {
            //string fileName = @"pathdatabase\DatabaseConnection.json";
            //string s = (Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
            //Console.WriteLine("Path - " + s);
            ////s.Replace('\GLproject\PL(Console)\bin\Debug\netcoreapp3.1\', ""); //?
            //Console.WriteLine("Path - " + s);
            StartUp st = new StartUp();
            /*
            UserOperationPL userOperationPL = new UserOperationPL();

            for (; ; )
            {
                Console.WriteLine("если хотите вийти нажмите q");
                if (Console.ReadLine() == "q") break;

                userOperationPL.RegistCustomer();
                userOperationPL.RegistUser();

            }
            */

        }
    }
}
