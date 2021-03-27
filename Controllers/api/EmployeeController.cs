using EmployeeManagement.Models.Workers.Repository;
using System.Web.Http;

namespace EmployeeManagement.Controllers.api
{
    public class EmployeeController : ApiController
    {
        //private Models.Workers.ManageEmployee manageEmp = default;
     /*   public IList<Employee> GetAllEmp()
        {
            DatabaseTransact transact = new DatabaseTransact(new IQBusinessEntities());
            IList<Employee> empList = (IList<Employee>) transact.ManageEmployee.SelectAll();

            return empList;
        }

        public IList<Employee> GetAllEmp(int id)
        {
            DatabaseTransact transact = new DatabaseTransact(new IQBusinessEntities());
            IList<Employee> empList = (IList<Employee>)transact.ManageEmployee.SelectSingle(id);

            return empList;
        }*/
     [HttpPost]
        public void SaveEmp(Employee emp)
        {
            DatabaseTransact transact = new DatabaseTransact(new IQBusinessEntities());
            //Employee manage =  new Employee() { Gender = "f", Age = 25, CurrentProject = "dEV", IsDeleted = false, Name = "iRIE", Address = "158 Monthso street" };
            transact.ManageEmployee.Add(emp);
            transact.Save();
        }

        [HttpPut]
        public void Update(Employee emp)
        {
            DatabaseTransact transact = new DatabaseTransact(new IQBusinessEntities());
            transact.ManageEmployee.Update(emp);
            transact.Save();
        }

        [HttpDelete]
        public void Delete(Employee emp)
        {
            DatabaseTransact transact = new DatabaseTransact(new IQBusinessEntities());
            transact.ManageEmployee.Delete(emp);
            transact.Save();

            
        }
    }
}
