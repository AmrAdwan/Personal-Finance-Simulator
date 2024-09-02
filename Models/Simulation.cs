using System.ComponentModel.DataAnnotations;

namespace finance.Models;

public class Simulation
{
  [Key]
  public int Id { get; set; }
  public int ProfileId { get; set; }
  public string? ScenarioDescription { get; set; }
  public string? ExpectedOutcome { get; set; }

  // Consider using navigation properties for related entities
}









// namespace finance.Models;

// public class Simulation
// {
//   private int Id;

//   private int ProfileId;
//   public string? ScenarioDescription;
//   public string? ExpectedOutcome;

//   // public SimulationResult RunSimulation()
//   // {

//   // }

//   public bool UpdateSimulation()
//   {
//     return false;
//   }

//   public bool DeleteSimulation()
//   {
//     return false;
//   }

//   // public Simulation GetSimulation()
//   // {

//   // }

// }
