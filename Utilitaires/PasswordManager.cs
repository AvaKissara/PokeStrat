using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;



namespace PokeStat.Utilitaires
{
    public class PasswordManager
    {
        // Méthode pour hacher un mot de passe et générer un sel aléatoire
        public static (string hash, string salt) HashPassword(string password)
        {
            // Générer un sel aléatoire sous forme de tableau d'octets
            byte[] saltBytes = GenerateSalt();
            // Conversion du mot de passe en tableau d'octets
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Créer une instance de l'algorithme de hachage SHA256
            using (var sha256 = SHA256.Create())
            {
                // Combinaison du sel et du mot de passe en un seul tableau d'octets
                byte[] saltedPassword = new byte[saltBytes.Length + passwordBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, saltedPassword, 0, saltBytes.Length);
                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, saltBytes.Length, passwordBytes.Length);

                // Calculer le hachage du mot de passe et du sel combinés
                byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

                // Convertir les tableaux d'octets en chaînes Base64 pour le stockage
                return (Convert.ToBase64String(hashedBytes), Convert.ToBase64String(saltBytes));
            }
        }

        // Méthode pour vérifier si un mot de passe saisi correspond au hachage stocké
        public static bool VerifyPassword(string enteredPassword, string storedHash, string salt)
        {
            // Convertir le sel en tableau d'octets
            byte[] saltBytes = Convert.FromBase64String(salt);
            // Conversion du mot de passe saisi en tableau d'octets
            byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

            // Créer une instance de l'algorithme de hachage SHA256
            using (var sha256 = SHA256.Create())
            {
                // Combinaison du sel et du mot de passe saisi en un seul tableau d'octets
                byte[] saltedEnteredPassword = new byte[saltBytes.Length + enteredPasswordBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, saltedEnteredPassword, 0, saltBytes.Length);
                Buffer.BlockCopy(enteredPasswordBytes, 0, saltedEnteredPassword, saltBytes.Length, enteredPasswordBytes.Length);

                // Calculer le hachage du mot de passe saisi et du sel combinés
                byte[] hashedEnteredPassword = sha256.ComputeHash(saltedEnteredPassword);

                // Convertir le hachage saisi en chaîne Base64 pour la comparaison
                string enteredPasswordHash = Convert.ToBase64String(hashedEnteredPassword);

                // Comparer le hachage saisi avec le hachage stocké
                return enteredPasswordHash == storedHash;
            }
        }

        // Méthode pour générer un sel aléatoire
        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];
            // Utiliser RNGCryptoServiceProvider pour générer un sel aléatoire sécurisé
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}