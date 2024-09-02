using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace finance.Models;

public class SimulationResult
{
  [Key]
  public int Id { get; set; }

  public string UserId { get; set; }

  // public string UserId { get; set; }
  public bool Success { get; set; }
  public string? Message { get; set; }
  public double ProjectedBalance { get; set; }
  public List<string>? Recommendations { get; set; }
  public DateTime SimulationDate { get; set; } = DateTime.UtcNow;
  // Navigation property to User
  public User? User { get; set; }


  

  // Method to generate recommendations
  public void GenerateRecommendations()
  {
    Recommendations = new List<string>();

    if (ProjectedBalance < 0)
    {
      Recommendations.Add("Consider reducing your expenses to improve your financial health.");
      Recommendations.Add("Review and adjust your savings contributions.");
    }
    else if (ProjectedBalance > 0 && ProjectedBalance < 5000)
    {
      Recommendations.Add("Your financial health is good, but there's room for improvement.");
      Recommendations.Add("Consider investing a portion of your savings to grow your wealth.");
    }
    else
    {
      Recommendations.Add("Excellent financial health! Consider diversifying your investments for greater returns.");
    }
  }
}

