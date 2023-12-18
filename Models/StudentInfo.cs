namespace Databaser_Labb3_V2.Models;

public class StudentInfo
{
    public Studenter Student { get; set; }
    public KlassList KlassList { get; set; }
    public Klasser Klasser { get; set; }
    public Betyg Betyg { get; set; }
    public Ämnen Ämnen { get; set; }
    public Personal Personal { get; set; }
    public Avdelning Avdelning { get; set; }
}
