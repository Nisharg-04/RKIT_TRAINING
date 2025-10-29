using System;
using System.Collections.Generic;

namespace EFCoreMySqlScaffold;

public partial class Prlm01
{
    public int T01f01 { get; set; }

    public string T01f02 { get; set; } = null!;

    public virtual ICollection<Prlm02> Prlm02s { get; set; } = new List<Prlm02>();
}
