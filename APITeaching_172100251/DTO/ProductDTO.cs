using System;
using System.Collections.Generic;

namespace APITeaching_172100251.DTO;

public partial class ProductDTO
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public int? CategoryId { get; set; }

    public int? SupplierId { get; set; }

    public string? QuantityPerUnit { get; set; }

    public decimal? UnitPrice { get; set; }

    public short? UnitsInStock { get; set; }

    public short? UnitsOnOrder { get; set; }

    public short? ReorderLevel { get; set; }

    public bool Discontinued { get; set; }

    public decimal? Weight { get; set; }

    public int? Status { get; set; }
}
