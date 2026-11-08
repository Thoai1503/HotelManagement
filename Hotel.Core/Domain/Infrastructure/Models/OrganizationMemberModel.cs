namespace Hotel.Core.Domain.Infrastructure.Models
{
    public class OrganizationMemberModel
    {
        public OrganizationMemberModel() { }


        public Guid id { get; set; }
        public Guid organizationId { get; set; }
        public Guid userId { get; set; }

    }
}
