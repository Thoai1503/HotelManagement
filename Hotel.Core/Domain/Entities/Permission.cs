using System;
using System.Collections.Generic;

namespace Hotel.Core.Domain.Entities;

public partial class Permission
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAtUtc { get; set; }

    public virtual ICollection<BusinessRole> Roles { get; set; } = new List<BusinessRole>();
}
