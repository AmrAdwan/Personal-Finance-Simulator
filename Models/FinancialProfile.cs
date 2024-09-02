// FinancialProfile.cs
using System.ComponentModel.DataAnnotations;

namespace finance.Models;

public class FinancialProfile
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public double Income { get; set; }
    public double Expenses { get; set; }
    public double Savings { get; set; }
    public double Debt { get; set; }

    public double InterestRate { get; set; } // Annual interest rate
    public int SimulationYears { get; set; } // Number of years for the simulation
}