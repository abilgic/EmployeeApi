using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmployeeApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RepositoryContext appDbContext;

        public EmployeeRepository(RepositoryContext _appDbContext)
        {
            this.appDbContext = _appDbContext;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await appDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            return await appDbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await appDbContext.Employees.AddAsync(employee);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task AddEmployees(List<Employee> employees)
        {
            await appDbContext.Employees.AddRangeAsync(employees);
            await appDbContext.SaveChangesAsync();
        }
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await appDbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == employee.Id);

            if (result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.Email = employee.Email;
                result.Birthdate = employee.Birthdate;
                result.Address = employee.Address;
                result.Phone = employee.Phone;

                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task DeleteEmployee(int employeeId)
        {
            var result = await appDbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == employeeId);
            if (result != null)
            {
                appDbContext.Employees.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}
