using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EmployeeManagement.Models.Workers
{
    public class ManageEmployee 
    {
        private static IQBusinessEntities _entities;

        public ManageEmployee(IQBusinessEntities entities)
        {
            _entities = entities;
        }

        public Employee SelectSingle(int id)
        {
            Employee emp = null;
            emp = _entities.Employees.Where(x => x.EmpID == id).FirstOrDefault();
            return emp;
        }
        public IList<Employee> GetAll()
        {
            IList<Employee> emps = _entities.Employees.ToList();
            return emps;
        }

        public void Update(Employee emp)
        {
            _entities.Set<Employee>().AddOrUpdate(emp);
            _entities.SaveChanges();
        }
        public void Create(EmployeeManagement.Employee emp)
        {
            _entities.Employees.Add(emp);
            _entities.SaveChanges();
        }

        public void Delete(int id)
        {

             Employee emp = _entities.Employees.Where(x => x.EmpID == id).FirstOrDefault();
            _entities.Entry(emp).State = System.Data.Entity.EntityState.Deleted;
            _entities.SaveChanges();
            
        }

    }
}