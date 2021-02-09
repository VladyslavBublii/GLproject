using Core.Models;
using DAL;
using System;

namespace BL
{
    public class StartUp
    {
        public StartUp()
        {
            var user = new User
            {
                Role = 1,
                Email = "trunovalexander8@gmail.com",
                Password = "123456"
            };
            
            UnitOfWork unitOfWork = new UnitOfWork();
            
            unitOfWork.Users.Create(user);
            unitOfWork.Save();

            var users = unitOfWork.Users.GetAll();
            foreach (var item in users)
            {
                Console.WriteLine(item.Id + " " + item.Role + " " + item.Email + " " + item.Password);
            }

            var customer = new Customer
            {
                Name = "Alexander",
                SurName = "Trunov",
                City = "Kharkov",
                PostIndex = "Post123456789"
            };

            unitOfWork.Customers.Create(customer);
            unitOfWork.Save();

            var customers = unitOfWork.Customers.GetAll();
            foreach (var item in customers)
            {
                Console.WriteLine(item.Id + " " + item.Name + " " + item.SurName + " " + item.City + " " + item.PostIndex);
            }
        }
    }
}
