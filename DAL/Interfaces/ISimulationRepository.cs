using finance.Models;

namespace finance.DAL.Interfaces
{
  public interface ISimulationRepository
  {
    Task<SimulationResult> FindSimulationResultByIdAsync(int resultId);
    Task<List<SimulationResult>> GetPastSimulationResultsAsync(string userId);
    Task<SimulationResult> RunSimulationAsync(FinancialProfile financialProfile, string userId);
    Task SaveSimulationResultAsync(SimulationResult result);
  }
}