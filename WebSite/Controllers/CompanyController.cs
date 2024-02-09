
using Core.Services.Companies;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using WebSite.Filters;

namespace WebSite.Controllers
{
    [Authorize(systemName = "Company")]
    public class CompanyController : Controller
    {
        public ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        } 
       
        public async Task<IActionResult> Index()
        {
            var result =  await _companyRepository.GetCompanys_Async();
            return View(result);
        }

        [HttpPost]
        public JsonResult GetCompanys()
        {
            var result = _companyRepository.GetCompanys();
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetCompanyForSelect2(string searchTerm)
        {
            var result = _companyRepository.GetCompanyForSelect2(searchTerm);
            return Json(result);
        }

        [HttpPost]
        public IActionResult GetCompanyssss()
        {
            var result = _companyRepository.GetCompanys();

            var recordsTotal = result.Count();
            var jsonData = new { recordsTotal = recordsTotal, data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public IActionResult Show_InsertPartial()
        {
            Company company = new Company();
            return PartialView("_InsertPartial", company);
        }

        public IActionResult AddCompany(Company company)
        {
            _companyRepository.AddCompany(company);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Show_UpdatePartial([FromBody] Company lab)
        {
            int id = lab.Id;
            Company company = await _companyRepository.GetCompanyById_Async(id);
            return PartialView("_UpdatePartial", company);
        }

        public IActionResult UpdateCompany(Company company)
        {
            int id = company.Id;
            bool result = _companyRepository.UpdateCompany(id, company);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _companyRepository.DeleteCompany(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Close()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}