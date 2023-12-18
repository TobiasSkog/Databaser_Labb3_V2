using System;
using System.Collections.Generic;

namespace Databaser_Labb3_V2.Models;

public partial class Avdelning
{
    public int AvdelningId { get; set; }

    public string AvdelningNamn { get; set; } = null!;

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
