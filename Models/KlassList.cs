namespace Databaser_Labb3_V2.Models;

public partial class KlassList
{
    public int FkStudentId { get; set; }

    public int FkKlassId { get; set; }

    public virtual Klasser FkKlass { get; set; } = null!;

    public virtual Studenter FkStudent { get; set; } = null!;
}
