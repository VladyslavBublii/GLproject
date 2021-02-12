using PL_Console_.Models;
using System;

namespace PL_Console_
{
    public class UserOperationPL
    {
        
        public CustomerPL RegistCustomer()
        {
            string name;
            string surname;
            string city;
            string postIndex;

            Console.WriteLine("Введите свое имя:");
            name = Console.ReadLine();

            Console.WriteLine("Введите свою фамилию:");
            surname = Console.ReadLine();

            Console.WriteLine("Введите свой город:");
            city = Console.ReadLine();

            Console.WriteLine("Введите свой почтовый индекс:");
            postIndex = Console.ReadLine();


            CustomerPL customerPL = new CustomerPL
            {
                Name = name,
                SurName = surname,
                City = city,
                PostIndex = postIndex
            };

            return customerPL;
        }

        public UserPL RegistUser()
        {
            string email;
            string password;

            Console.WriteLine("Введите свой емейл:");
            email = Console.ReadLine();

            Console.WriteLine("Введите свой пароль:");
            password = Console.ReadLine();


            UserPL userPL = new UserPL
            {
                Email = email,
                Password = password
            };

            return userPL;
        }
    }
}
