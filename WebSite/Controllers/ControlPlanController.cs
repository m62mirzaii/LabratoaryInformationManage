using Share.Generators;
using Core.Services.ControlPlans;
using Microsoft.AspNetCore.Mvc;

using Models.ViewModel;
using WebSite.Filters;

namespace WebSite.Controllers
{

    //[Authorize(systemName = "ControlPlan")]

    public class ControlPlanController : Controller
    {
        public IControlPlanRepository _controlPlanRepository;

        public ControlPlanController(IControlPlanRepository controlPlanRepository)
        {
            _controlPlanRepository = controlPlanRepository;
        }

        public IActionResult Index()
        {
            ViewBag.MyDate = DateTimeGenerator.GetShamsiDate(DateTime.Now.Date);
            return View();
        }

        public IActionResult GetControlPlans()
        {
            var result = _controlPlanRepository.GetControlPlans();
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public JsonResult GetControlPlanForSelect2(string searchTerm)
        {
            var result = _controlPlanRepository.GetControlPlanForSelect2(searchTerm);
            return Json(result);
        }

        #region ===== ControlPlan ======
        [HttpPost]
        public IActionResult Show_InsertPartial_ControlPlan()
        {
            ViewBag.MyDate = DateTimeGenerator.GetShamsiDate(DateTime.Now.Date);
            ControlPlanViewModel controlPlan = new ControlPlanViewModel();
            return PartialView("_InsertPartial_ControlPlan", controlPlan);
        }

        [HttpPost]
        public IActionResult AddControlPlan(int companyId, string planNumber, string createDate)
        {
            _controlPlanRepository.AddControlPlan(companyId, planNumber, createDate);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Show_UpdatePartial_ControlPlan([FromBody] ControlPlanViewModel controlPlanViewModel)
        {
            int id = controlPlanViewModel.Id;
            ControlPlanViewModel _controlPlan = _controlPlanRepository.GetControlPlanByIdAsync(id);
            return PartialView("_UpdatePartial_ControlPlan", _controlPlan);
        }

        [HttpPost]
        public IActionResult UpdateControlPlan(int id, int companyId, string planNumber, string createDate)
        {
            bool result = _controlPlanRepository.UpdateControlPlan(id, companyId, planNumber, createDate);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ===== Peice ======

        [HttpPost]
        public IActionResult GetControlPlanPieces(int ControlPlanId)
        {
            var result = _controlPlanRepository.GetControlPlanPiece(ControlPlanId);
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public IActionResult Show_InsertPartial_ControlPlanPiece()
        {
            ControlPlanPieceViewModel _controlPlan = new ControlPlanPieceViewModel();
            return PartialView("_InsertPartial_ControlPlanPiece", _controlPlan);
        }

        [HttpPost]
        public IActionResult AddControlPlanPiece(int ControlPlanId, List<int> Pieceids)
        {
            _controlPlanRepository.AddControlPlanPiece(ControlPlanId, Pieceids);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ===== Process ======

        [HttpPost]
        public IActionResult GetControlPlanProcess(int controlPlanId)
        {
            var result = _controlPlanRepository.GetControlPlanProcess(controlPlanId);
            var jsonData = new { data = result };

            return Ok(jsonData);
        }


        [HttpPost]
        public IActionResult Show_InsertPartial_ControlPlanProcess()
        {
            ControlPlanProcessViewModel controlPlan = new ControlPlanProcessViewModel();
            return PartialView("_InsertPartial_ControlPlanProcess", controlPlan);
        }

        public IActionResult AddControlPlanProcess(int ControlPlanId, List<int> ProcessIds)
        {
            _controlPlanRepository.AddControlPlanProcess(ControlPlanId, ProcessIds);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region ===== ProcessTest ======
        [HttpPost]
        public IActionResult GetControlPlanProcessTest(int ControlPlanProcessId)
        {
            var result = _controlPlanRepository.GetControlPlanProcessTest(ControlPlanProcessId);
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public IActionResult GetControlPlanProcessTestByControlPlanId(int controlPlanId)
        {
            var result = _controlPlanRepository.GetControlPlanProcessTestByControlPlanId(controlPlanId);
            var jsonData = new { data = result };

            return Ok(jsonData);
        }

        [HttpPost]
        public IActionResult Show_InsertPartial_ControlPlanProcessTest()
        {
            ControlPlanProcessTestViewModel controlPlan = new ControlPlanProcessTestViewModel();
            return PartialView("_InsertPartial_ControlPlanProcessTest", controlPlan);
        }

        public IActionResult AddControlPlanProcessTest(int ControlPlanProcessId, List<int> testIds, decimal min, decimal max, int testImportanceId, int measureId, int standardId, int testDescriptionId)
        {
            //ControlPlanProcessId = 6;
            _controlPlanRepository.AddControlPlanProcessTest(ControlPlanProcessId, testIds, min, max, testImportanceId, measureId, standardId, testDescriptionId);
            return RedirectToAction(nameof(Index));
        }
        #endregion


        public IActionResult DeleteControlPlan(int id)
        {
            _controlPlanRepository.DeleteControlPlan(id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult DeleteControlPlanPiece(int id)
        {
            _controlPlanRepository.DeleteControlPlanPiece(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteControlPlanProcess(int id)
        {
            _controlPlanRepository.DeleteControlPlanProcess(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteControlPlanProcessTest(int id)
        {
            _controlPlanRepository.DeleteControlPlanProcessTest(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Close()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}