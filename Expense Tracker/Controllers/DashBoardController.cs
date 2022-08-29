using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
//using System.Transactions;


namespace Expense_Tracker.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly ApplicationDBContext context;

        public DashBoardController(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            //last 7 days
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Transaction> selectedTransactions = await context.Transactions.Include(x => x.Category)
                .Where(y => y.Date >= StartDate && y.Date <= EndDate)
                .ToListAsync();

            //total income 
            int totalIncome = selectedTransactions
                .Where(i => i.Category.Type == "Income")
                .Sum(j => j.Amount);

            ViewBag.TotalIncome = totalIncome.ToString("C0");

            //total Expense
            int totalExpense = selectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .Sum(j => j.Amount);

            ViewBag.TotalExpense = totalExpense.ToString("C0");

            //Balance 

            int Balance = totalIncome - totalExpense;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Balance = String.Format(culture, "{0:C0}", Balance);

            Balance.ToString("C0");

            //donut chart \
            ViewBag.DonutChartData = selectedTransactions
                .Where(x => x.Category.Type == "Expense")
                .GroupBy(x => x.Category.CategoryId).
                Select(x => new
                {
                    categoryTitleWithIcon = x.First().Category.Icon +  " " + x.First().Category.Title,
                    amount = x.Sum(j => j.Amount),
                    formatterAmount = x.Sum(j => j.Amount).ToString("C0")
                })
                .ToList()
                ;


            return View();
        }
    }
}
