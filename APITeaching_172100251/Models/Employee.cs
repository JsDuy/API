using System;
using System.Collections.Generic;

namespace APITeaching_172100251.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Title { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? HireDate { get; set; }

    public string? Phone { get; set; }

    public byte[]? Photo { get; set; }

    public string? PhotoPath { get; set; }

    public int? AddressId { get; set; }

    public int? AccountId { get; set; }

    public int? Status { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
