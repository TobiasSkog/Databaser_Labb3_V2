namespace Databaser_Labb3_V2.Models
{
    public class StudentInClass
    {
        public int StudentId { get; set; }

        public string StudentNamn { get; set; } = null!;

        public string StudentSsn { get; set; } = null!;

        public string? StudentFörnamn { get; set; }
        public string? StudentEfternamn { get; set; }
        public int KlassId { get; set; }
        public string KlassNamn { get; set; } = null!;
    }
}
