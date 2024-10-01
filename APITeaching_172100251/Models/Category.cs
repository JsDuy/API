using System;
using System.Collections.Generic;

namespace APITeaching_172100251.Models;

public partial class Category
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public int? ParentId { get; set; }

    public string? Description { get; set; }

    public byte[]? Picture { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
