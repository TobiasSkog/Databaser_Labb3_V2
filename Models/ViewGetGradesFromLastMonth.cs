using System;
using System.Collections.Generic;

namespace Databaser_Labb3_V2.Models;

public partial class ViewGetGradesFromLastMonth
{
    public string Student { get; set; } = null!;

    public string Ämne { get; set; } = null!;

    public string Betyg { get; set; } = null!;

    public string Lärare { get; set; } = null!;

    public DateOnly Datum { get; set; }
}
