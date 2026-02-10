
namespace Pos_system.Models
{
    public class StoreSettings
    {

        public int Id { get; set; }
        public required string Storename { get; set; }
        public required string Address { get; set; }
        public required string Contact { get; set; }
        public string LogoPath { get; set; }
    }
}
