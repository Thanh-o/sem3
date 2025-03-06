using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;



namespace ATMapp.Models
{
    public enum AccountType
    {
        Checking, // tài khoản thanh toán
        Saving,   // tài khoản tiết kiệm
        Credit    // thẻ tín dụng
    }

    public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AccountId { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public decimal Balance { get; set; }

    // Sử dụng StringEnumConverter để chuyển đổi enum thành chuỗi
    [JsonConverter(typeof(StringEnumConverter))]
    public AccountType Type { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public float? InterestRate { get; set; }
    public List<Transaction>? Transactions { get; set; }
}
}