using Core.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using BL.Services.Interfaces;
using System.Text;

namespace BL.Services;

public class PasswordService : IPasswordService
{
    public PassStrength PasswordStrength(string password)
    {
        var score = 0;
        var patterns = new Dictionary<string, int> { { @"\d", 5 },
            { @"[a-zA-Z]", 10 },
            { @"[!,@,#,\$,%,\^,&,\*,?,_,~]", 15 } };
        if (password.Length > 6)
            score += patterns.Sum(pattern => 
                Regex.Matches(password, pattern.Key).Count * pattern.Value);

        var result = (score / 50) switch
        {
            0 => PassStrength.Low,
            1 => PassStrength.Medium,
            2 => PassStrength.High,
            3 => PassStrength.VeryHigh,
            _ => PassStrength.Paranoid
        };
        return result;
    }

    public string GetHashString(string password)
    {
        var bytes = Encoding.Unicode.GetBytes(password);

        var CSP = new MD5CryptoServiceProvider();

        var byteHash = CSP.ComputeHash(bytes);

        return byteHash.Aggregate(string.Empty, (current, b) => current + $"{b:x2}");
    }

    public bool IsPasswordStrong(string password) => PasswordStrength(password) >= PassStrength.Medium;
}