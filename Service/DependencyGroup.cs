using Core.CacheMemory;
using Core.Services.Companies;
using Core.Services.ControlPlans;
using Core.Services.LabratoryTools;
using Core.Services.Measures;
using Core.Services.Pieces;
using Core.Services.PieceUsages;
using Core.Services.Processes;
using Core.Services.ProcessTypes;
using Core.Services.RequestCompanies;
using Core.Services.RequestUnits;
using Core.Services.Standards;
using Core.Services.System;
using Core.Services.SystemMethods;
using Core.Services.SystemMethodUsers;
using Core.Services.SystemUsers;
using Core.Services.TestAccepts;
using Core.Services.TestConditions;
using Core.Services.TestDescriptions;
using Core.Services.TestImportances;
using Core.Services.TestLabratoryTools;
using Core.Services.TestRequests;
using Core.Services.Tests;
using Core.Services.User;
using Microsoft.Extensions.DependencyInjection;
using Service.Forms;
using Service.Reports;
using Service.User;

namespace DataAccessLayer.Config
{
    public static  class DependencyGroup
    {    
        public static IServiceCollection AddMyDependencyGroup(this IServiceCollection service)
        {
            service.AddScoped<IPieceUsageRepository, PieceUsageService>();
            service.AddScoped<IPieceRepository, PieceService>();
            service.AddScoped<ITestRepository, TestService>();
            service.AddScoped<ILabratoaryToolRepository, LabratoaryToolService>();
            service.AddScoped<IProcessRepository, ProcessService>();
            service.AddScoped<ITestRepository, TestService>();
            service.AddScoped<IUserRepository, UserService>();
            service.AddScoped<IMeasureRepository, MeasureService>();
            service.AddScoped<IProcessTypeRepository, ProcessTypeService>();
            service.AddScoped<IStandardRepository, StandardService>();
            service.AddScoped<ITestImportanceRepository, TestImportanceService>();
            service.AddScoped<IControlPlanRepository, ControlPlanService>();
            service.AddScoped<ITestConditionRepository, TestConditionService>();
            service.AddScoped<ICompanyRepository, CompanyService>();
            service.AddScoped<ISystemRepository, SystemService>();
            service.AddScoped<ISystemMethodRepository, SystemMethodService>();
            service.AddScoped<ISystemMethodUserRepository, SystemMethodUserService>();
            service.AddScoped<IUserAccessCacheMemory, UserAccessCacheMemory>();
            service.AddScoped<ITestDescriptionRepository, TestDescriptionService>();
            service.AddScoped<ITestRequestRepository, TestRequestService>();
            service.AddScoped<ITestAcceptRepository, TestAcceptService>();
            service.AddScoped<ISystemUserRepository, SystemUserService>();
            service.AddScoped<ITestLabratoaryToolRepository, TestLabratoryToolService>();
            service.AddScoped<IRequestUserRepository, RequestUserService>();
            service.AddScoped<IRequestUnitRepository, RequestUnitService>();
            service.AddScoped<IUserAccessService, UserAccessService>();
            service.AddScoped<IReportService, ReportdService>();
            return service;
        }
    }
}