using System;
using System.Collections.Generic;

namespace APITeaching_172100251.DTO;

public partial class AccountDTO
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? ImageId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public int Status { get; set; }
}
