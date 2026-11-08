namespace Hotel.Core.Domain.Infrastructure.Models
{
    public class OrganizationModel
    { 
        public Guid id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(200)]
        public string name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string code { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(2048)]
        public string? logo { get; set; }

        [System.ComponentModel.DataAnnotations.EmailAddress]
        [System.ComponentModel.DataAnnotations.StringLength(256)]
        public string? email { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(32)]
        public string? phoneNumber { get; set; }

        public bool isActive { get; set; } = true;

        public bool isEditMode { get; set; }

        
        public OrganizationModel() {
         
            id = Guid.NewGuid();
        }
    }
}
