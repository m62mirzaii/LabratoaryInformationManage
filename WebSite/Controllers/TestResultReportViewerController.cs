using BoldReports.Web.ReportViewer;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebSite.Controllers; 

[Route("api/[controller]/[action]")]
[Microsoft.AspNetCore.Cors.EnableCors("AllowAllOrigins")]
public class TestResultReportViewerController : Controller, IReportController
{
    private Microsoft.Extensions.Caching.Memory.IMemoryCache _cache;

    public TestResultReportViewerController(Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache)
    {
        _cache = memoryCache;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    } 

    [NonAction]
    public void OnInitReportOptions(ReportViewerOptions reportOption)
    {
        reportOption.ReportModel.ReportServerCredential = new NetworkCredential("Administrator", "@zinkhodro!#340");
        reportOption.ReportModel.DataSourceCredentials.Add(new BoldReports.Web.DataSourceCredentials("DataSource_Report", "BI_USER", "ms2Ndu4jz9")); 
    }

    [NonAction]
    public void OnReportLoaded(ReportViewerOptions reportOption)
    {
        List<BoldReports.Web.ReportParameter> userParameters = new List<BoldReports.Web.ReportParameter>();
        userParameters.Add(new BoldReports.Web.ReportParameter()
        {
            Name = "Result_Separation",
            Values = new List<string>() { "1" }
        });
        userParameters.Add(new BoldReports.Web.ReportParameter()
        {
            Name = "TestRequestIDs",
            Values = new List<string>() { "1" }
        });
        reportOption.ReportModel.Parameters = userParameters;

    }

    [HttpPost]
    public object PostReportAction([FromBody] Dictionary<string, object> jsonArray)
    {
        return ReportHelper.ProcessReport(jsonArray, this, this._cache);
    }

    [ActionName("GetResource")]
    [AcceptVerbs("GET")]
    public object GetResource(ReportResource resource)
    {
        return ReportHelper.GetResource(resource, this, _cache);
    }

    [HttpPost]
    public object PostFormReportAction()
    {
        return ReportHelper.ProcessReport(null, this, _cache);
    }
}
