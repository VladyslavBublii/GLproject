using BL;
using BL.DTO;
using BL.Services;
using PL_Console_.Models;
using System;

namespace PL_Console_
{
    public class UserOperationPL
    {
        UserService _userService;
        UserDTO _userDTO;
        CustomerDTO _customerDTO;

        public UserOperationPL()
        {
            _userService = new UserService();
        }
        
        public void RegistCustomer()
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


            CustomerDTO customerDTO = new CustomerDTO
            {
                Name = name,
                SurName = surname,
                City = city,
                PostIndex = postIndex
            };

            UserDTO RegistUser()
            {
                string email;
                string password;

                Console.WriteLine("Введите свой емейл:");
                email = Console.ReadLine();

                Console.WriteLine("Введите свой пароль:");
                password = Console.ReadLine();


                UserDTO userDTO = new UserDTO
                {
                    Email = email,
                    Password = password
                };

                return userDTO;
            }

            _userService.SaveUser(RegistUser(), customerDTO);

        }

    }
}
//testtttt