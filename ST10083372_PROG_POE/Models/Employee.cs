using System;
using System.Collections.Generic;

namespace ST10083372_PROG_POE.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}
