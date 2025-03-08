using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hr.makemystamp.com.core.Entities
{
    public class Identity
    {
        [Key]
        public Guid IdentityId { get; set; }
        [Required]
        public string HashedPassword { get; set; }
        [Required]
        public string Salt { get; set; }

        // Navigation Property
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
