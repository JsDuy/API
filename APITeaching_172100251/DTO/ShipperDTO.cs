using System;
using System.Collections.Generic;

namespace APITeaching_172100251.DTO;

public partial class ShipperDTO
{
    public int Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Phone { get; set; }

    public int? AddressId { get; set; }

    public int? Status { get; set; }
}
