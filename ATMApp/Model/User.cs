// User.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ATMapp.Models
{
    public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public List<Account>? Accounts { get; set; }
}
}