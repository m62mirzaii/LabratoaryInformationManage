using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Core.Services.Companies
{
    public class CompanyService : ICompanyRepository
    {
        public DataBaseContext _dbContext;
        private readonly DbSet<Company> _company;

        public CompanyService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _company = dbContext.Set<Company>();
        }

        public async Task<Company> GetCompanyById_Async(int id)
        {
            return await _company.Where(x => x.Id == id).FirstOrDefaultAsync() ?? null;
        }

        public async Task<List<Company>> GetCompanys_Async()
        {
            return await _company.OrderByDescending(e => e.Id).ToListAsync();
        }

        public List<Company> GetCompanys()
        {
            return _company.OrderByDescending(e => e.Id).ToList();
        }

        public List<Company> GetCompanyForSelect2(string searchTerm)
        {
            var query = _company.AsQueryable();
            if (searchTerm != null)
                query = query.Where(e => e.Name.Contains(searchTerm));

            var result = query.ToList();
            return result;
        }

        public void AddCompany(Company company)
        {
            var _item = new Company
            {
                Code = company.Code,
                Name = company.Name
            };

            _company.Add(_item);
            _dbContext.SaveChanges();
        }

        public bool UpdateCompany(int id, Company company)
        {
            var _item = _company.Find(id);
            if (_item != null)
            {
                _item.Code = company.Code;
                _item.Name = company.Name;

                _dbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void DeleteCompany(int id)
        {
            var _item = _company.Find(id);
            if (_item != null)
            {
                _company.Remove(_item);
                _dbContext.SaveChanges();
            }
        }


    }
}
