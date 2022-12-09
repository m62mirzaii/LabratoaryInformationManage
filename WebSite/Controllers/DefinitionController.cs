using Core.Repository; 
using Microsoft.AspNetCore.Mvc;
using Models.Model;

namespace WebSite.Controllers
{
    public class DefinitionController : Controller
    {
        public IDefinitionRepository _definitionService;

        public DefinitionController(IDefinitionRepository definitionService)
        {
            _definitionService = definitionService;
        }

        public async Task<IActionResult> Index()
        {
            var result =  await _definitionService.GetDefinitions();
            return View(result);
        }

        [HttpPost]
        public JsonResult GetAllDefinitions()
        {
            var result = _definitionService.GetAllDefinitions();
            return Json(result);
        } 

        [HttpPost]
        public IActionResult Show_InsertPartial()
        {
            Definition definition = new Definition();
            return PartialView("_InsertPartial", definition);
        }

        public IActionResult AddDefinition(Definition definition)
        {
            _definitionService.AddDefinition(definition);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Show_UpdatePartial([FromBody] Definition lab)
        {
            int id = lab.Id;
            Definition definition = await _definitionService.GetDefinitionById(id);
            return PartialView("_UpdatePartial", definition);
        }

        public IActionResult UpdateDefinition(Definition definition)
        {
            int id = definition.Id;
            bool result = _definitionService.UpdateDefinition(id, definition);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _definitionService.DeleteDefinition(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Close()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}