using System;
using System.Collections.Generic;

namespace APITeaching_172100251.Models;

public partial class District
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string DistrictCode { get; set; } = null!;

    public int ProvinceId { get; set; }

    public int? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
