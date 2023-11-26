using Core.Enums;
using BL.Services.Interfaces;

namespace BL.Services
{
    public class RoleService : IRoleService
    {
        public Role RoleSpecificator(string role)
        {
            int score = 0;
            if(role == "admin")
            {
                score = 2;
            }
            if (role == "user")
            {
                score = 1;
            }
            Role result;
            switch (score)
            {
                case 0: result = Role.Guest; break;
                case 1: result = Role.User; break;
                case 2: result = Role.Admin; break;
                default: result = Role.Guest; break;
            }
            return result;
        }

        public bool IsAdmin(string role)
        {
            if (RoleSpecificator(role) != Role.Admin)
            {
                return false;
            }
            return true;
        }

        public bool IsUser(string role)
        {
            if (RoleSpecificator(role) != Role.User)
            {
                return false;
            }
            return true;
        }
    }
}
