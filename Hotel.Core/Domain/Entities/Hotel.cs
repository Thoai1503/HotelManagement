using System;
using System.Collections.Generic;

namespace Hotel.Core.Domain.Entities;

public partial class Hotel
{
    public Guid Id { get; set; }

    public Guid OrganizationId { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;
                                                                                                                                    
    public string Address { get; set; } = null!;
   
   public string PhoneNumber { get; set; } = null!;

   public string Email { get; set; } = null!;

   public bool IsActive { get; set; } = true;

    public DateTime CreatedAtUtc { get; set; }

    public virtual ICollection<HotelMembership> HotelMemberships { get; set; } = new List<HotelMembership>();

    public virtual Organization Organization { get; set; } = null!;
}
