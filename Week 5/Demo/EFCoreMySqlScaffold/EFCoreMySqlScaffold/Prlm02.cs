using System;
using System.Collections.Generic;

namespace EFCoreMySqlScaffold;

public partial class Prlm02
{
    public int T02f01 { get; set; }

    public string T02f02 { get; set; } = null!;

    public int? T02f03 { get; set; }

    public decimal T02f04 { get; set; }

    public virtual ICollection<Prlt01> Prlt01s { get; set; } = new List<Prlt01>();

    public virtual Prlm01? T02f03Navigation { get; set; }
}
