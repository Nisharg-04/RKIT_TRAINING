using System;
using System.Collections.Generic;

namespace EFCoreMySqlScaffold;

public partial class Prlt01
{
    public int T03f01 { get; set; }

    public int? T03f02 { get; set; }

    public string T03f03 { get; set; } = null!;

    public decimal T03f04 { get; set; }

    public DateOnly? T03f05 { get; set; }

    public virtual Prlm02? T03f02Navigation { get; set; }
}
