using Microsoft.AspNetCore.Mvc;
using MyExpenseApp.Data;
using MyExpenseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Extensions.Logging;
namespace MyExpenseApp.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly ApplicationDbContext _db;
        public ExpenseTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<ExpenseType> ExpensesList = _db.ExpenseTypes;
            return View(ExpensesList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                _db.ExpenseTypes.Add(expenseType);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseType);
        }
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.ExpenseTypes.Find(Id);
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
            var obj = _db.ExpenseTypes.Find(Id);
            if(obj == null)
            {
                return NotFound();
            }
                _db.ExpenseTypes.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");   
            
        }
       
        public IActionResult Update(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.ExpenseTypes.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseType obj)
        {                
            if (obj == null)
            {
                return NotFound();
            }
            _db.ExpenseTypes.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
          

        }
    }
}
