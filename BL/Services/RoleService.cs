using Core.Enums;
using BL.Services.Interfaces;

namespace BL.Services;

public class RoleService : IRoleService
{
    public Role RoleSpecificator(string role)
    {
        var score = role switch
        {
            "admin" => 2,
            "user" => 1,
            _ => 0
        };

        var result = score switch
        {
            1 => Role.User,
            2 => Role.Admin,
            _ => Role.Guest
        };
        return result;
    }

    public bool IsAdmin(string role) => RoleSpecificator(role) == Role.Admin;

    public bool IsUser(string role) => RoleSpecificator(role) == Role.User;
}