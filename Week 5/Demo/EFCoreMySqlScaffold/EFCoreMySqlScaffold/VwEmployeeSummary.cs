using System;
using System.Collections.Generic;

namespace EFCoreMySqlScaffold;

public partial class VwEmployeeSummary
{
    public int EmployeeId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public decimal EmployeeSalary { get; set; }

    public string DepartmentName { get; set; } = null!;
}
