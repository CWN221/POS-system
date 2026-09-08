
using Pos_system.Models;

namespace Pos_system.Core
{
    public static class AppContext
    {
        public static int CurrentUserId { get; set; }
        public static int CurrentBranchId { get; private set; }
        public static StoreBranch? CurrentBranch { get; private set; }
        public static UserRole CurrentUserRole { get; set; }

        public static List<int> AllowedBranches { get; set; } = new();

        public static event Action? OnBranchChanged;

        public static void SetBranch(StoreBranch branch)
        {
            if (!CanAccessBranch(branch.Id))
                throw new UnauthorizedAccessException("User cannot access this branch.");

            CurrentBranchId = branch.Id;
            CurrentBranch = branch;
            OnBranchChanged?.Invoke();
        }

        public static bool CanAccessBranch(int branchId)
        {
            if (CurrentUserRole == UserRole.Admin) return true;
            return AllowedBranches.Contains(branchId);
        }

        public static void EnsureBranchAccess(int branchId)
        {
            if (!CanAccessBranch(branchId))
                throw new UnauthorizedAccessException($"Branch {branchId} not permitted.");
        }

        public static void Reset()
        {
            CurrentUserId = 0;
            CurrentBranchId = 0;
            CurrentBranch = null;
            CurrentUserRole = default;
            AllowedBranches = new List<int>();
        }
    }
}
