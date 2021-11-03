using Microsoft.AspNetCore.Mvc;
using MyExpenseApp.Data;
using MyExpenseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyExpenseApp.Models.ViewModels;

namespace MyExpenseApp.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly ApplicationDbContext _db;
        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Expense> ExpensesList = _db.Expenses;

            foreach (var obj in ExpensesList) {
                obj.ExpenseTypeName = _db.ExpenseTypes.FirstOrDefault(x => x.Id == obj.ExpenseTypeId);
            }
            return View(ExpensesList);
        }
        public IActionResult Create()
        {
         
            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.ExpenseTypeName,
                    Value = i.Id.ToString()
                })
            };
            return View(expenseVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Add(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }       
            return View(obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? Id)
        {
            var obj = _db.Expenses.Find(Id);
            if(obj == null)
            {
                return NotFound();
            }
                _db.Expenses.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");   
            
        }
       
        public IActionResult Update(int? Id)
        {
            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.ExpenseTypeName,
                    Value = i.Id.ToString()
                })
            };
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            expenseVM.Expense = _db.Expenses.Find(Id);
            if (expenseVM.Expense == null)
            {
                return NotFound();
            }
            return View(expenseVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseVM obj)
        {                
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
          

        }
    }
}
