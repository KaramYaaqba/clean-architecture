using AutoMapper;
using Contracts;
using Entities.ErrorModel;
using Service.Contracts;
using Shared.DataTransferObjects;
namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employeesFromDb = _repository.Employee.GetEmployees(companyId, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            
            return employeesDto;
        }

        public EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges){
            var employee = _repository.Employee.GetEmployee(companyId, employeeId, trackChanges);
            if(employee is null)
                throw new EmployeeNotFoundException(employeeId);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }
        
    }
}