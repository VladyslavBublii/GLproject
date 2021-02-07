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
            foreach(var item in users)
            {
                Console.WriteLine(item.Id + " " + item.Email);
            }
        }       
    }
}
