using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        //CategoryId
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

    }
}
