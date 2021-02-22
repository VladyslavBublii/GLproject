using Core.Enums;

namespace BL.Services.Interfaces
{
    interface IPassword
    {
        public Strength PasswordStrength(string password);
        string GetHashString(string password);
    }
}
