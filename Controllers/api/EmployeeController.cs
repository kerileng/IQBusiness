using EmployeeManagement.Models.Workers.Repository;
using System.Collections.Generic;
using System.Web.Http;

namespace EmployeeManagement.Controllers.api
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        //private Models.Workers.ManageEmployee manageEmp = default;
       public IList<Employee> GetAllEmp()
        {
            DatabaseTransact transact = new DatabaseTransact(new IQBusinessEntities());
            IList<Employee> empList = (IList<Employee>)transact.ManageEmployee.GetAll();

            return empList;
        }

        [HttpGet]
        public Employee GetEmp(int id)
           {
               DatabaseTransact transact = new DatabaseTransact(new IQBusinessEntities());
               Employee empList = transact.ManageEmployee.SelectSingle(id);

               return empList;
           }
        [HttpPost]
        public void Create(Employee emp)
        {
            DatabaseTransact transact = new DatabaseTransact(new IQBusinessEntities());
            transact.ManageEmployee.Create(emp);
       
        }

       [HttpPut]
        public void Update(Employee emp)
        {
            DatabaseTransact transact = new DatabaseTransact(new IQBusinessEntities());
            transact.ManageEmployee.Update(emp);

        }

       [HttpDelete]
        public void Delete(int id)
        {
            DatabaseTransact transact = new DatabaseTransact(new IQBusinessEntities());
            transact.ManageEmployee.Delete(id);
         

            
        }
    }
}
