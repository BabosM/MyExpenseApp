using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyExpenseApp.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Expense Name")]
        [Required]
        public string ExpenseName { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount has to be grater than 0")]
        public int Amount { get; set; }
        [DisplayName("Expense Type")]
        public int ExpenseTypeId { get; set; }
        [ForeignKey ("ExpenseTypeId")]
        public virtual ExpenseType ExpenseTypeName { get; set; }
    }
}
