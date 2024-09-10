using Core.Enums;
using System;

namespace Core.Extantion
{
    public static class RoleExtensions
    {
        public static string ParseRoleToString(this Role role)
        {
            return ((int)role).ToString();
        }

        public static Role ParseStringToRole(this string roleString)
        {
            if (Enum.TryParse(roleString, true, out Role parsedRole))
            {
                return parsedRole;
            }
            else
            {
                // ToDo:
                // return Content($"Incorrect role value: {roleString}");

                return Role.Guest;
            }
        }
    }
}
