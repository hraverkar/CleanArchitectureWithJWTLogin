using hr.makemystamp.com.core.Common;
using System.ComponentModel.DataAnnotations;

namespace hr.makemystamp.com.core.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid External_Id { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
