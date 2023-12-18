namespace Databaser_Labb3_V2.Models
{
    public class DepartmentPayoutInformation
    {
        public virtual Avdelning Avdelning { get; set; }
        public decimal TotalPayout { get; set; }
        public decimal AveragePayout { get; set; }
    }
}
