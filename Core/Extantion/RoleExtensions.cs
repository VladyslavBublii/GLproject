using Core.Enums;
using System;

namespace Core.Extantion
{
    public static class RoleExtensions
    {
        public static Role ParseRole(this string roleString)
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
