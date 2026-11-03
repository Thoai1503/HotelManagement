using System;
using System.Collections.Generic;

namespace Hotel.Core.Domain.Entities;

public partial class HotelMembership
{
    public Guid Id { get; set; }

    public Guid OrganizationMemberId { get; set; }

    public Guid HotelId { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual OrganizationMember OrganizationMember { get; set; } = null!;

    public virtual ICollection<BusinessRole> Roles { get; set; } = new List<BusinessRole>();
}
