using System;
using System.Collections.Generic;

namespace APITeaching_172100251.DTO;

public partial class OrderDetailDTO
{
    public int? Id { get; set; }

    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public decimal UnitPrice { get; set; }

    public short Quantity { get; set; }

    public float Discount { get; set; }

    public int? Status { get; set; }
}
