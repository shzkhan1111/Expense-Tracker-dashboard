using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Category? Category { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [NotMapped]
        public string CategoryTitleWithIcon { 
            get
            {
                return Category == null ? string.Empty : Category.Icon + " " + Category.Title;
            }
        }
        [NotMapped]
        public string FormattedAmount
        {
            get
            {
                return (Category == null || Category.Type=="Expense"? "- ": "+ ") + Amount.ToString("C0");
                //curreny withouut decimal
            }
        }

    }
}
