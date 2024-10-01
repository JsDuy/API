using System;
using System.Collections.Generic;

namespace APITeaching_172100251.DTO;

public partial class WardDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string WardCode { get; set; } = null!;

    public int DistrictId { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }
}
