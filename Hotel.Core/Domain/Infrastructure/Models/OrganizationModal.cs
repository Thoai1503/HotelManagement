namespace Hotel.Core.Domain.Infrastructure.Models
{
    public class OrganizationModal
    { 
        public Guid id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public OrganizationModal() {
         
            id = Guid.NewGuid();
        }
    }
}
