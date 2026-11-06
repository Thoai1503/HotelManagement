

namespace Hotel.Core.Domain.Entities
{
    public partial class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string AvatarUrl { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAtUtc { get; set; }

     
    }
}