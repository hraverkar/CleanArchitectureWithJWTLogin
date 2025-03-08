using System.ComponentModel.DataAnnotations;

namespace hr.makemystamp.com.core.Entities
{
    public class RolesUser
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public long RoleId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
