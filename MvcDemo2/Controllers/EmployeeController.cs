
using Application.Employees;
using Application.Employees.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MvcDemo2.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var companies = await _employeeService.GetAllEmployeesAsync();

            return View(companies);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _employeeService.GetEmployeeByIdAsync(id);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return View(result.Value);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateEmployeeRequest request)
        {
            var result = await _employeeService.CreateEmployeeAsync(request);

            if (result.IsFailure)
            {
                ModelState.AddModelError(nameof(request.Email), result.Error);
                ModelState.AddModelError(nameof(request.CompanyId), result.Error);

                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee.IsFailure)
            {
                ModelState.AddModelError(nameof(id), employee.Error);
                return View();
            }

            return View(employee.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromForm] UpdateEmployeeRequest request)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _employeeService.UpdateEmployeeAsync(id, request);

            if (result.IsFailure)
            {
                ModelState.AddModelError(nameof(request.Email), result.Error);
                ModelState.AddModelError(nameof(request.CompanyId), result.Error);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _employeeService.GetEmployeeByIdAsync(id);

            if (result.IsFailure)
            {
                ModelState.AddModelError(nameof(id), result.Error);
                return View();
            }

            return View(result.Value);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _employeeService.DeleteEmployeeyAsync(id);

            if (result.IsFailure)
            {
                ModelState.AddModelError(nameof(id), result.Error);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
