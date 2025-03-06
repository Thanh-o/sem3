using System.ComponentModel.DataAnnotations;

namespace ATMApp.Models{
    public class User {
        [Key]
        public int UserId { get; set; }
        public string? Name { get; set; }
        public String? Password { get; set; }
        public List<Account>? Accounts { get; set; }
    }
}