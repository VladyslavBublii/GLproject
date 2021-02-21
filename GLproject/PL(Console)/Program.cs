using System;


namespace PL_Console_
{
    class Program
    {
        static void Main(string[] args)
        {
            UserOperationPL userOperationPL = new UserOperationPL();

            for (; ; )
            {
                Console.WriteLine("если хотите вийти нажмите q");
                if (Console.ReadLine() == "q") break;

                userOperationPL.RegistCustomer();
            }
        }
    }
}
