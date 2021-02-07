using Core.Models;
using DAL;

namespace BL
{
    public class StartUp
    {
        public StartUp()
        {
            using (var context = new UserContext())
            {

                //context.Database.EnsureCreated();
                var user = new User
                {
                    Role = 1,
                    Email = "trunovalexander8@gmail.com",
                    Password = "123456"
                };
                context.Users.Add(user);
                context.SaveChanges();
            }

        }       
    }
}
