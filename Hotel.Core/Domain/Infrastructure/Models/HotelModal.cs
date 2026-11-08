using System.ComponentModel.DataAnnotations;

namespace Hotel.Core.Domain.Infrastructure.Models
{
    public class HotelModal
    {
        public Guid id { get; set; }

        [Required]
        public Guid organizationId { get; set; }

        public string? organizationName { get; set; }

        [Required]
        [StringLength(200)]
        public string name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string code { get; set; } = string.Empty;

        [Required]
        public string address { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string email { get; set; } = string.Empty;

        [Required]
        [StringLength(32)]
        public string phoneNumber { get; set; } = string.Empty;

        public bool isActive { get; set; } = true;

        public bool isEditMode { get; set; }
    }
}
