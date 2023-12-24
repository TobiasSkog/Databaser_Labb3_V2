namespace Databaser_Labb3_V2.Models;

public partial class Betyg
{
    public int BetygId { get; set; }

    public string Betyg1 { get; set; } = null!;

    public DateOnly BetygDatum { get; set; }

    public int FkÄmneId { get; set; }

    public int FkStudentId { get; set; }

    public int FkPersonalId { get; set; }

    public virtual Personal FkPersonal { get; set; } = null!;

    public virtual Studenter FkStudent { get; set; } = null!;

    public virtual Ämnen FkÄmne { get; set; } = null!;
}