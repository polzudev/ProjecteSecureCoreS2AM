using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace ProjecteSecureCoreS2AM
{
    public static class GestorPasswords
    {
        public const string PasswordInicial = "12345aA";
        public const int LongitudMinima = 8;

        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int Iteraciones = 10101;

        public static byte[] GenerarSalt()
        {
            using (RNGCryptoServiceProvider generador = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltByteSize];
                generador.GetBytes(salt);
                return salt;
            }
        }

        public static byte[] CalcularHash(string password, byte[] salt)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iteraciones))
            {
                return pbkdf2.GetBytes(HashByteSize);
            }
        }

        public static string ATexto(byte[] bytes)
        {
            return BitConverter.ToString(bytes);
        }

        public static byte[] ABytes(string texto)
        {
            return texto.Split('-').Select(b => Convert.ToByte(b, 16)).ToArray();
        }

        public static bool VerificarPassword(string password, byte[] salt, byte[] hashGuardado)
        {
            return CalcularHash(password, salt).SequenceEqual(hashGuardado);
        }

        public static List<string> RequisitosIncumplidos(string password)
        {
            List<string> incumplidos = new List<string>();
            if (password.Length < LongitudMinima)
            {
                incumplidos.Add("Mínim " + LongitudMinima + " caràcters");
            }
            if (password.Length == 0 || !char.IsDigit(password[0]))
            {
                incumplidos.Add("Ha de començar per un número");
            }
            if (!password.Any(c => !char.IsLetterOrDigit(c)))
            {
                incumplidos.Add("Ha de tenir algun caràcter especial (.@,...)");
            }
            if (password == PasswordInicial)
            {
                incumplidos.Add("No pot ser la paraula de pas inicial");
            }
            return incumplidos;
        }
    }
}
