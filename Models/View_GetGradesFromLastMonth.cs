namespace Databaser_Labb3_V2.Models;

public class View_GetGradesFromLastMonth
{
    public string Student { get; set; }
    public string Ämne { get; set; }
    public Char Betyg { get; set; }
    public DateOnly Datum { get; set; }
    public string Lärare { get; set; }
}
