namespace Hotel.Core.Sercurity.Cryptography
{
    public class PasswordHasher
    {
        public string MixPassword(string password, string salt)
        {
            return salt + password;
        }

        public string HashPassword(string password, string salt)
        {
            return SHA256Helper.Enscrypt(MixPassword(password, salt));
        }

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword, string salt)
        {
            return hashedPassword == HashPassword(providedPassword, salt);
        }
    }
}
