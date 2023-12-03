using System;
using System.Collections.Generic;

namespace Databaser_Labb3_V2.Models;

public partial class Ämnen
{
    public int ÄmneId { get; set; }

    public string ÄmneNamn { get; set; } = null!;

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();
}
