using Core.Services.Standards;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Moq;
using WebSite.Controllers;

namespace TestProject.Mesear;

public class StandardControllerTest
{
    //private readonly IStandardRepository _standardRepository;
    //private readonly StandardController _standardController;

    //public StandardControllerTest(IStandardRepository standardRepository, StandardController standardController)
    //{
    //    _standardRepository = standardRepository;
    //    _standardController = standardController;
    //}


    //[Fact]
    //public void GetStandards()
    //{
    //    var result = _standardController.GetStandards();

    //    Assert.IsType<OkObjectResult>(result);


    //}




    private readonly Mock<IStandardRepository> _mock;
    private readonly StandardController _standardController;

    public StandardControllerTest()
    {
        _mock = new Mock<IStandardRepository>();
     _standardController = new StandardController(_mock.Object);
    }


    [Fact]
    public void GetStandard_Should_Return_ListOfStandards()
    {
        _mock.Setup(x => x.GetStandards()).Returns(new List<Standard>()); 
        var result = _standardController.GetStandards();

        Assert.IsType<OkObjectResult>(result);
    }

    //public List<Standard> GetSampleStandards()
    //{
    //    List<Standard> standards = new List<Standard>();
    //    standards.Add(new Standard { Id = 1, IsActive = true, Name = "Test1" });
    //    standards.Add(new Standard { Id = 2, IsActive = true, Name = "Test2" });
    //    standards.Add(new Standard { Id = 3, IsActive = true, Name = "Test3" });

    //    return standards;
    //}
}


