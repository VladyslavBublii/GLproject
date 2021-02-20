using System;
using BL;

namespace PL_Console_
{
    class Program
    {

        static void Main(string[] args)
        {
            UserOperationPL userOperationPL = new UserOperationPL();
            UserBL startUp = new UserBL();

            for (; ; )
            {
                Console.WriteLine("если хотите вийти нажмите q");
                if (Console.ReadLine() == "q") break;

                userOperationPL.RegistCustomer();
                userOperationPL.RegistUser();
            }
        }
    }
}
