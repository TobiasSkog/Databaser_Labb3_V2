namespace Databaser_Labb3_V2.Models;

public partial class Studenter
{
    public int StudentId { get; set; }

    public string StudentNamn { get; set; } = null!;

    public string StudentSsn { get; set; } = null!;

    public string? StudentFörnamn { get; set; }

    public string? StudentEfternamn { get; set; }

    public string? StudentKön { get; set; }

    public byte? StudentÅlder { get; set; }

    public DateTime? StudentFödelsedag { get; set; }

    public DateOnly? StudentStartDatum { get; set; }
    //public string? TestarLite { get; set; }

    public virtual ICollection<Betyg> Betygs { get; set; } = new List<Betyg>();
}
