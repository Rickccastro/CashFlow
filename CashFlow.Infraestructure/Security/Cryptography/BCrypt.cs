using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace CashFlow.Infraestructure.Security.Cryptography;

public class BCrypt : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }
}

