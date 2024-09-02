using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace finance.Models
{
  // [Table("Users")]
  public class User: IdentityUser
  {
    [Key]
    public int Id { get; set; } 

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required, MaxLength(100)]
    public string? Password { get; set; } 

    // [Required]
    // [MaxLength(100)]
    // public string? PasswordHash { get; set; } 

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string? Email { get; set; } // Use data annotations for validation.
  }
}