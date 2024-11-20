using Core.Enums;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using BL.Services.Interfaces;
using System.Text;

namespace BL.Services
{
    public class PasswordService : IPasswordService
    {
        public PassStrength PasswordStrength(string password)
        {
            int score = 0;
            Dictionary<string, int> patterns = new Dictionary<string, int> { { @"\d", 5 },
                                                                         { @"[a-zA-Z]", 10 },
                                                                         { @"[!,@,#,\$,%,\^,&,\*,?,_,~]", 15 } }; 
            if (password.Length > 6)
                foreach (var pattern in patterns)
                    score += Regex.Matches(password, pattern.Key).Count * pattern.Value;

            PassStrength result;
            switch (score / 50)
            {
                case 0: result = PassStrength.Low; break;
                case 1: result = PassStrength.Medium; break;
                case 2: result = PassStrength.High; break;
                case 3: result = PassStrength.VeryHigh; break;
                default: result = PassStrength.Paranoid; break;
            }
            return result;
        }

        public string GetHashString(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);

            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

        public bool IsPasswordStrong(string password)
        {
            if (PasswordStrength(password) < PassStrength.Medium)
            {
                return false;
            }
            return true;
        }
    }
}
