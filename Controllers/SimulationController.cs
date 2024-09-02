using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using finance.Models;
using finance.DAL.Interfaces;
using System.Security.Claims;
using System.Text.Json;

namespace finance.Controllers
{

  // [Authorize] // Ensure only logged-in users can access
  public class SimulationController : Controller
  {

    private readonly ISimulationRepository? _simulationRepository;

    public SimulationController(ISimulationRepository simulationRepository)
    {
      _simulationRepository = simulationRepository;
    }

    // GET: Simulation
    [HttpGet]
    public IActionResult Index()
    {
      FinancialProfile viewModel = null;

      if (TempData["FinancialProfileJson"] is string financialProfileJson)
      {
        viewModel = JsonSerializer.Deserialize<FinancialProfile>(financialProfileJson);
      }

      viewModel ??= new FinancialProfile();

      return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Simulate(FinancialProfile financialProfile)
    {
      if (ModelState.IsValid)
      {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Gets the current user's ID
        var result = await _simulationRepository.RunSimulationAsync(financialProfile, userId);
        await _simulationRepository.SaveSimulationResultAsync(result);

        var pastResults = await _simulationRepository.GetPastSimulationResultsAsync(userId);

        ViewBag.PastResults = pastResults; // Pass past results to the view

        TempData["FinancialProfileJson"] = JsonSerializer.Serialize(financialProfile);

        return View("SimulationResults", result);
      }

      return View("Index", financialProfile);
    }


    public IActionResult BackToSimulation()
    {
      if (TempData["FinancialProfileJson"] is string financialProfileJson)
      {
        var financialProfile = JsonSerializer.Deserialize<FinancialProfile>(financialProfileJson);
        return View("Index", financialProfile);
      }

      return RedirectToAction("Index");
    }

    public async Task<IActionResult> SaveResult(int resultId)
    {
      var result = await _simulationRepository.FindSimulationResultByIdAsync(resultId);
      if (result != null)
      {
        await _simulationRepository.SaveSimulationResultAsync(result);
      }
      return RedirectToAction("Index");
    }


  }
}
