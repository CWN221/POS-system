namespace Pos_system.Models
{
    public class StoreBranch
    {
        public int Id { get; set; }
        public required string BranchName { get; set; }
        public required string Location { get; set; }

        public override string ToString()
        {
            return $"{BranchName} - {Location}";
        }
    }
}
