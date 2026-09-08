
namespace Pos_system.Models
{
    public class StoreSettings
    {

        public int Id { get; set; }
        public int BranchId { get; set; }
        public required string Storename { get; set; }
        public required string Address { get; set; }
        public required string Contact { get; set; }
        public string? Contact2 { get; set; }
        public string? Email { get; set; }
        public string? BusinessPin { get; set; }
        public string? LogoPath { get; set; }
    }
}
