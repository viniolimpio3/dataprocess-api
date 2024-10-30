using System.Security.Cryptography;
using System.Text;

namespace data_process_api.Util {
    public static class Crypt {

        public static string HashPassword(string password) {
            byte[] hash = SHA256.HashData(Encoding.ASCII.GetBytes(password));

            return Encoding.Default.GetString(hash);
        }
    }
}
