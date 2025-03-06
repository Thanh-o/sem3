using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ATMapp.Models
{
    public class Transaction
    {
        [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public Account? Account { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public bool IsSuccessful { get; set; } = true;
        public string? Description { get; set; }
    }
}