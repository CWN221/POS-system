/*
 * 
 * Model for getter & setter methods for VAT calculation
 * 
 * This model is used in VatDAO.cs
 * 
 */


namespace Pos_system.Models
{
    public class VatSettings
    {
        public int Id { get; set; }
        public decimal VatPercentage { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
