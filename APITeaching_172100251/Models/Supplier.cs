using System;
using System.Collections.Generic;

namespace APITeaching_172100251.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? ContactTitle { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? HomePage { get; set; }

    public int? AddressId { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
