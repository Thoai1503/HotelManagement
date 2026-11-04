namespace Hotel.Core.Domain.Infrastructure.Models
{
    public class OrganizationModal
    { 
        public Guid id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(200)]
        public string name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string code { get; set; }

        public string description { get; set; }
        public OrganizationModal() {
         
            id = Guid.NewGuid();
        }
    }
}
