using Core.Enums;

namespace BL.Services.Interfaces
{
    interface IPassword
    {
        public Strength PasswordStrength(string password);

        public string GetHashString(string password);
    }
}
