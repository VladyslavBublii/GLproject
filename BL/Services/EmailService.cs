using BL.Services.Interfaces;
using System.Text.RegularExpressions;

namespace BL.Services;

public class EmailService : IEmailService
{
    public bool ValidateEmail(string email)
    {
        const string EMAIL_PATTERN = @"[.\-_a-z0-9]+@([a-z0-9][\-a-z0-9]+\.)+[a-z]{2,6}";
        var isMatch = Regex.Match(email, EMAIL_PATTERN, RegexOptions.IgnoreCase);
        return isMatch.Success;
    }
}