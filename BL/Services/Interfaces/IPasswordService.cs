using Core.Enums;

namespace BL.Services.Interfaces
{
    public interface IPasswordService
    {
        public PassStrength PasswordStrength(string password);

        public string GetHashString(string password);
    }
}
