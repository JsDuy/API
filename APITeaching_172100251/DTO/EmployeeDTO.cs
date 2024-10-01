using System;
using System.Collections.Generic;

namespace APITeaching_172100251.DTO;

public partial class EmployeeDTO
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Title { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? HireDate { get; set; }

    public string? Phone { get; set; }

    public string? PhotoPath { get; set; }

    public int? AddressId { get; set; }

    public int? AccountId { get; set; }

    public int? Status { get; set; }
}
