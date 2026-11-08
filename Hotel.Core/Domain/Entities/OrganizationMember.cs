using System;
using System.Collections.Generic;

namespace Hotel.Core.Domain.Entities;

public partial class OrganizationMember
{
    public Guid Id { get; set; }

    public Guid OrganizationId { get; set; }

    public Guid UserId { get; set; }

    // public MembershipStatus Status { get; set; }
    public int Status { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    public virtual ICollection<HotelMembership> HotelMemberships { get; set; } = new List<HotelMembership>();

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<BusinessRole> Roles { get; set; } = new List<BusinessRole>();
}
