using System;
using System.Collections.Generic;

namespace APITeaching_172100251.DTO;

public partial class CategoryDTO
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public int? ParentId { get; set; }

    public string? Description { get; set; }

    public int? Status { get; set; }

}
