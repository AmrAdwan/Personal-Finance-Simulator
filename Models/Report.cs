// Report.cs
using System.ComponentModel.DataAnnotations;

namespace finance.Models;

public class Report
{
    [Key]
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public DateTime GeneratedOn { get; set; }

    // Add properties or methods as needed
}






// namespace finance.Models;

// public class Report
// {
//   private int Id;
//   private int ProfileId;
//   public DateTime GeneratedOn;

//   public string GenerateReport()
//   {
//     return "";
//   }
// }
