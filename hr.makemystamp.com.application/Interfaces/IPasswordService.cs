using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hr.makemystamp.com.application.Interfaces
{
    public interface IPasswordService
    {
        (string Password, string Salt, string HashedPassword) HashPassword(string password);
        bool VerifyPassword(string password, string storedSalt, string storedHash);

    }
}
