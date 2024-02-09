using Models.Model;

namespace Core.Services.Companies
{
    public interface ICompanyRepository
    {
        Task<Company> GetCompanyById_Async(int id);
        Task<List<Company>> GetCompanys_Async();
        List<Company> GetCompanys();
        List<Company> GetCompanyForSelect2(string searchTerm);
        void AddCompany(Company company);
        bool UpdateCompany(int id, Company company);
        void DeleteCompany(int id);
    }
}
