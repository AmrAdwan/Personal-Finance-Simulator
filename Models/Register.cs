using System.ComponentModel.DataAnnotations;

namespace finance.Models
{
  public class Register
  {
    [Required, MaxLength(100)]
    public string? Name { get; set; }

    [Required, EmailAddress, MaxLength(255)]
    public string? Email { get; set; }

    [Required, DataType(DataType.Password)]
    public string? Password { get; set; }

    [DataType(DataType.Password), Compare("Password", ErrorMessage = "The passwords do not match.")]
    public string? ConfirmPassword { get; set; }
  }
}
