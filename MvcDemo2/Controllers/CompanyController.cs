using System;
using System.Threading.Tasks;
using Application.Companies;
using Application.Companies.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MvcDemo2.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<IActionResult> Index()
        {
            var companies = await _companyService.GetAllCompaniesAsync();

            return View(companies);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _companyService.GetCompanyByAsync(id);

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
        public async Task<IActionResult> Create([FromForm] CreateCompanyRequest request)
        {
            var result = await _companyService.CreateCompanyAsync(request);

            if (result.IsFailure)
            {
                ModelState.AddModelError(nameof(request.CompanyName), result.Error);
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

            var company = await _companyService.GetCompanyByAsync(id);

            if (company.IsFailure)
            {
                ModelState.AddModelError(nameof(id), company.Error);
                return View();
            }

            return View(company.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromForm] UpdateCompanyRequest request)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _companyService.UpdateCompanyAsync(id, request);

            if (result.IsFailure)
            {
                ModelState.AddModelError(nameof(request.CompanyName), result.Error);
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

            var result = await _companyService.GetCompanyByAsync(id);

            if (result.IsFailure)
            {
                ModelState.AddModelError(nameof(id), result.Error);
                return View();

            }

            return View(result.Value);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _companyService.DeleteCompanyAsync(id);

            if (result.IsFailure)
            {
                ModelState.AddModelError(nameof(id), result.Error);
                return View();
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
