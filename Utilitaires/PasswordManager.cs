using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security;
using System.Runtime.InteropServices;

namespace PokeStat.Utilitaires
{
    public class PasswordManager
    {
        // Méthode pour hacher un mot de passe et générer un sel aléatoire
        public static (string hash, string salt) HashPassword(string password)
        {
            if (password == null)
            {               
                password = string.Empty;
                string key = string.Empty;
                return (password, key);
            }
            else
            {
                // Génére un sel aléatoire sous forme de tableau d'octets
                byte[] saltBytes = GenerateSalt();
                // Conversion du mot de passe en tableau d'octets
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Crée une instance de l'algorithme de hachage SHA256
                using (var sha256 = SHA256.Create())
                {
                    // Combinaison du sel et du mot de passe en un seul tableau d'octets
                    byte[] saltedPassword = new byte[saltBytes.Length + passwordBytes.Length];
                    Buffer.BlockCopy(saltBytes, 0, saltedPassword, 0, saltBytes.Length);
                    Buffer.BlockCopy(passwordBytes, 0, saltedPassword, saltBytes.Length, passwordBytes.Length);

                    // Calcule le hachage du mot de passe et du sel combinés
                    byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                    // Converti les tableaux d'octets en chaînes Base64 pour le stockage
                    return (Convert.ToBase64String(hashedBytes), Convert.ToBase64String(saltBytes));
                }
          
            }
        }

        // Méthode pour vérifier si un mot de passe saisi correspond au hachage stocké
        public static bool VerifyPassword(SecureString secureEnteredPassword, SecureString secureStoredHash, SecureString secureSalt)
        {
            string enteredPassword = ToInsecureString(secureEnteredPassword);
            string storedHash = ToInsecureString(secureStoredHash);
            string salt = ToInsecureString(secureSalt);

            // Converti le sel en tableau d'octets
            byte[] saltBytes = Convert.FromBase64String(salt);
            // Conversion du mot de passe saisi en tableau d'octets
            byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

            // Crée une instance de l'algorithme de hachage SHA256
            using (var sha256 = SHA256.Create())
            {
                // Combinaison du sel et du mot de passe saisi en un seul tableau d'octets
                byte[] saltedEnteredPassword = new byte[saltBytes.Length + enteredPasswordBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, saltedEnteredPassword, 0, saltBytes.Length);
                Buffer.BlockCopy(enteredPasswordBytes, 0, saltedEnteredPassword, saltBytes.Length, enteredPasswordBytes.Length);

                // Calcule le hachage du mot de passe saisi et du sel combinés
                byte[] hashedEnteredPassword = sha256.ComputeHash(saltedEnteredPassword);

                // Converti le hachage saisi en chaîne Base64 pour la comparaison
                string enteredPasswordHash = Convert.ToBase64String(hashedEnteredPassword);

                // Compare le hachage saisi avec le hachage stocké
                return enteredPasswordHash == storedHash;
            }
        }

        // Méthode pour générer un sel aléatoire
        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];
            // Utilise RNGCryptoServiceProvider pour générer un sel aléatoire sécurisé
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static string ToInsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return null;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}