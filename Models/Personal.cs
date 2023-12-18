using System;
using System.Collections.Generic;

namespace Databaser_Labb3_V2.Models;

public partial class Personal
{
    public int PersonalId { get; set; }

    public string PersonalNamn { get; set; } = null!;

    public byte PersonalBefattning { get; set; }

    public string? PersonalFörnamn { get; set; }

    public string? PersonalEfternamn { get; set; }

    public string PersonalSsn { get; set; } = null!;

    public string? PersonalKön { get; set; }

    public byte? PersonalÅlder { get; set; }

    public DateOnly? PersonalStartDatum { get; set; }

    public int? FkAvdelningId { get; set; }

    public decimal? PersonalLön { get; set; }

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();

    public virtual Avdelning? FkAvdelning { get; set; }
}
