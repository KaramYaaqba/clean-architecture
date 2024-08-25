using System;
using Contracts;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly CompanyDbContext _companyDbContext;
    private readonly Lazy<ICompanyRepository> _companyRepository;
    private readonly Lazy<IEmployeeRepository> _employeeRepository;
    public RepositoryManager(CompanyDbContext companyDbContext)
    {
        _companyDbContext = companyDbContext;
        _companyRepository = new Lazy<ICompanyRepository>(() => new
        CompanyRepository(companyDbContext));
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new
        EmployeeRepository(companyDbContext));
    }
    public ICompanyRepository Company => _companyRepository.Value;
    public IEmployeeRepository Employee => _employeeRepository.Value;
    public void Save() => _companyDbContext.SaveChanges();
}
