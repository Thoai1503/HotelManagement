using System;
using System.Collections.Generic;

namespace Hotel.Core.Domain.Entities;

public partial class BusinessRole
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int Scope { get; set; }

    public Guid? OrganizationId { get; set; }

    public Guid? HotelId { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public virtual ICollection<HotelMembership> HotelMemberships { get; set; } = new List<HotelMembership>();

    public virtual ICollection<OrganizationMember> OrganizationMembers { get; set; } = new List<OrganizationMember>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
