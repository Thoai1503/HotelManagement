using System.ComponentModel.DataAnnotations;

namespace Hotel.Core.Domain.Infrastructure.Models
{
    public class UserModel
    {
         
      public Guid id { get; set; }
        [Required]
        public string userName { get; set; }

        [Required]
        public string passwordHash { get; set; }


        [EmailAddress]
        [StringLength(256)]
        public string email { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        public string avatarUrl { get; set; }
        [Required]
        public bool isActive { get; set; } = true;
        [Required]
        public DateTime createdAtUtc { get; set; }

    }
}
