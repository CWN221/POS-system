/*
 * BranchReport.cs
 * This class reperesents a branch comparison report encapsulating details such as Branch id, branch name, total sales, VAT, profit & transaction per branch
 * Properties include:
 * - 'BranchId': Unique identifier for a branch
 * - 'BranchName': Name of the branch
 * - 'Location': Location of the current branch
 * - 'TotalSales': Total amount of units sold made in a branch
 * - 'Totalvat': Total vat charged on items
 * - 'TotalProfit': Total profit made on items per branch
 * - 'TotalTransactions': Total transactions made in a branch
 * 
 */

public class BranchReport
{
    public int BranchId { get; set; }
    public string BranchName { get; set; } = "";
    public string Location { get; set; }
    public decimal TotalSales { get; set; }
    public decimal TotalVat { get; set; }
    public decimal TotalProfit { get; set; }
    public int TotalTransactions { get; set; }
}