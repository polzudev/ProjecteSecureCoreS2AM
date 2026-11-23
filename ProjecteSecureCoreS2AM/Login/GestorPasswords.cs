using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace ProjecteSecureCoreS2AM
{
    /// <summary>
    /// Gestiona la seguretat de les paraules de pas: generació de salts aleatoris, càlcul de
    /// hash PBKDF2 i verificació. Totes les operacions criptogràfiques fan servir
    /// <see cref="System.Security.Cryptography.Rfc2898DeriveBytes"/> amb 10 101 iteracions.
    /// </summary>
    public static class GestorPasswords
    {
        /// <summary>Paraula de pas inicial assignada als nous usuaris.</summary>
        public const string PasswordInicial = "12345aA";

        /// <summary>Longitud mínima en caràcters que ha de tenir una paraula de pas.</summary>
        public const int LongitudMinima = 8;

        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int Iteraciones = 10101;

        /// <summary>
        /// Genera un salt criptogràficament segur de 24 bytes.
        /// </summary>
        /// <returns>Array de bytes aleatoris apte per usar com a salt PBKDF2.</returns>
        public static byte[] GenerarSalt()
        {
            using (RNGCryptoServiceProvider generador = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltByteSize];
                generador.GetBytes(salt);
                return salt;
            }
        }

        /// <summary>
        /// Calcula el hash PBKDF2 d'una paraula de pas amb el salt indicat.
        /// </summary>
        /// <param name="password">Paraula de pas en text pla.</param>
        /// <param name="salt">Salt criptogràfic de 24 bytes.</param>
        /// <returns>Array de 24 bytes amb el hash resultant.</returns>
        public static byte[] CalcularHash(string password, byte[] salt)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iteraciones))
            {
                return pbkdf2.GetBytes(HashByteSize);
            }
        }

        /// <summary>
        /// Converteix un array de bytes a la seva representació hexadecimal separada per guions.
        /// </summary>
        /// <param name="bytes">Array de bytes a convertir.</param>
        /// <returns>Cadena hexadecimal, per exemple "1A-2B-3C".</returns>
        public static string ATexto(byte[] bytes)
        {
            return BitConverter.ToString(bytes);
        }

        /// <summary>
        /// Converteix una cadena hexadecimal separada per guions a un array de bytes.
        /// </summary>
        /// <param name="texto">Cadena en format "1A-2B-3C" (sortida de <see cref="ATexto"/>).</param>
        /// <returns>Array de bytes equivalent.</returns>
        public static byte[] ABytes(string texto)
        {
            return texto.Split('-').Select(b => Convert.ToByte(b, 16)).ToArray();
        }

        /// <summary>
        /// Comprova si una paraula de pas en text pla coincideix amb el hash emmagatzemat.
        /// </summary>
        /// <param name="password">Paraula de pas que l'usuari ha introduït.</param>
        /// <param name="salt">Salt emmagatzemat a la base de dades per a aquest usuari.</param>
        /// <param name="hashGuardado">Hash emmagatzemat a la base de dades per a aquest usuari.</param>
        /// <returns><c>true</c> si la paraula de pas és correcta; <c>false</c> en cas contrari.</returns>
        public static bool VerificarPassword(string password, byte[] salt, byte[] hashGuardado)
        {
            return CalcularHash(password, salt).SequenceEqual(hashGuardado);
        }

        /// <summary>
        /// Retorna la llista de requisits de seguretat que la paraula de pas no compleix.
        /// </summary>
        /// <param name="password">Paraula de pas a avaluar.</param>
        /// <returns>
        /// Llista amb els missatges dels requisits incomplerts. La llista és buida si la
        /// paraula de pas és vàlida.
        /// </returns>
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
