using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NETCore_Demo.Models;

namespace NETCore_Demo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }


        // GET: Employees/Create
        public IActionResult AddOrEdit(int id=0)
        {
	        var ts = _context.Employees.FromSqlInterpolated($"select * from Employees where EmployeeId={id}").FirstOrDefault();
	        return View(id == 0 ? new Employee() : ts);
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("EmployeeId,FullName,EmpCode,Position,OfficeLocation")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if(employee.EmployeeId==0)
                    _context.Add(employee);
                else
                
	                _context.Update(employee);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
	        var employee = _context.Employees.FromSqlInterpolated($"select * from Employees where EmployeeId={id}").FirstOrDefault();
	        _context.Employees.Remove(employee);
	        await _context.SaveChangesAsync();

	        return RedirectToAction(nameof(Index));
        }


        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
