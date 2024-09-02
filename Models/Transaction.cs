using System.ComponentModel.DataAnnotations;

namespace finance.Models;

public class Transaction
{
  [Key]
  public int Id { get; set; }
  public int ProfileId { get; set; }
  public double Amount { get; set; }
  public DateTime Date { get; set; }
  public string? Type { get; set; } // Consider using an enum for Type (Income/Expense)
  public string? Category { get; set; }

  // Additional methods or properties
}






// namespace finance.Models;

// public class Transaction
// {
//   private int id;
//   private int ProfileId;
//   public double amount;
//   public DateTime date;
//   public string? type; // (income/expense)
//   public string? category;


//   public bool AddTransaction()
//   {
//     return false;
//   }

//   public bool UpdateTransaction()
//   {
//     return false;
//   }

//   public bool DeleteTransaction()
//   {
//     return false;
//   }

//   // public Transaction GetTransaction()
//   // {

//   // }

//   // public List<Transaction> SearchTransaction()
//   // {

//   // }
// }
