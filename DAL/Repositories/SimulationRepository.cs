using finance.DAL.Interfaces;
using finance.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SimulationRepository : ISimulationRepository
{
  private readonly FinanceDbContext _context;

  public SimulationRepository(FinanceDbContext context)
  {
    _context = context;
  }

  public async Task<SimulationResult> RunSimulationAsync(FinancialProfile financialProfile, string userId)
  {
    // Ensure these properties exist in your FinancialProfile class
    double interestRateAnnual = financialProfile.InterestRate; // Annual interest rate
    int simulationYears = financialProfile.SimulationYears; // Number of years for the simulation

    double monthlyContribution = (financialProfile.Income - financialProfile.Expenses);
    double futureValue = CompoundInterest(financialProfile.Savings, monthlyContribution, interestRateAnnual / 12, simulationYears * 12);

    var recommendations = new List<string>();

    var result = new SimulationResult
    {
      UserId = userId,
      ProjectedBalance = futureValue - financialProfile.Debt,
      Success = futureValue >= 0,
      Message = futureValue >= 0 ? "Simulation successful." : "Simulation indicates financial distress.",
      Recommendations = recommendations
    };
    // Generate recommendations based on the result
    result.GenerateRecommendations();

    return result;
  }

  private double CompoundInterest(double principal, double monthlyAddition, double monthlyInterestRate, int timesCompounded)
  {
    double futureValue = principal * Math.Pow(1 + monthlyInterestRate, timesCompounded);

    for (int i = 0; i < timesCompounded; i++)
    {
      futureValue += monthlyAddition * Math.Pow(1 + monthlyInterestRate, timesCompounded - i);
    }

    return futureValue;
  }

  // public async Task SaveSimulationResultAsync(SimulationResult result)
  // {
  //   // _context.SimulationResults.Add(result);
  //   // await _context.SaveChangesAsync();
  //   try
  //   {
  //     _context.SimulationResults.Add(result);
  //     await _context.SaveChangesAsync();
  //   }
  //   catch (Exception ex)
  //   {
  //     // Log the exception details
  //     Console.WriteLine(ex.Message);
  //   }
  // }

  public async Task SaveSimulationResultAsync(SimulationResult result)
  {
    if (!_context.SimulationResults.Any(r => r.Id == result.Id))
    {
      _context.SimulationResults.Add(result);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<SimulationResult> FindSimulationResultByIdAsync(int id)
  {
    return await _context.SimulationResults.FindAsync(id);
  }


  public async Task<List<SimulationResult>> GetPastSimulationResultsAsync(string userId)
  {
    return await _context.SimulationResults
        .Where(sr => sr.UserId == userId)
        .OrderByDescending(sr => sr.SimulationDate)
        .ToListAsync();
  }
}
