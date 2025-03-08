using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hr.makemystamp.com.core.EventHandler
{
    public class UserCreationEmailHandler : INotification
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }

        public UserCreationEmailHandler(Guid id, string firstName, string email, string roleName)
        {
            Id = id;
            FirstName = firstName;
            Email = email;
            RoleName = roleName;
        }
    }
}
