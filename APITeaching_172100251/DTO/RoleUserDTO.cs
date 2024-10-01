using System;
using System.Collections.Generic;

namespace APITeaching_172100251.DTO;

public partial class RoleUserDTO
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int AccountId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public int Status { get; set; }
}
