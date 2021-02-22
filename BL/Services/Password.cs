using Core.Enums;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace BL.Services
{
    class Password
    {
        public Strength PasswordStrength(string password)
        {
            int score = 0;
            Dictionary<string, int> patterns = new Dictionary<string, int> { { @"\d", 5 }, //включает цифры
                                                                         { @"[a-zA-Z]", 10 }, //буквы
                                                                         { @"[!,@,#,\$,%,\^,&,\*,?,_,~]", 15 } }; //символы
            if (password.Length > 6)
                foreach (var pattern in patterns)
                    score += Regex.Matches(password, pattern.Key).Count * pattern.Value;

            Strength result;
            switch (score / 50)
            {
                case 0: result = Strength.Low; break;
                case 1: result = Strength.Medium; break;
                case 2: result = Strength.High; break;
                case 3: result = Strength.VeryHigh; break;
                default: result = Strength.Paranoid; break;
            }
            return result;
        }

        string GetHashString(string password)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(password);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
}
