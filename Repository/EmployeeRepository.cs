using System;
using Contracts;
using Entities.Models;

namespace Repository;

public class EmployeeRepository : RepositoryBase<Company>, IEmployeeRepository
{
    public EmployeeRepository(CompanyDbContext companyDbContext)
    : base(companyDbContext)
    {

        
    }
}
