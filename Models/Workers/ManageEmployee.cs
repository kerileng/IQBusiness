namespace EmployeeManagement.Models.Workers
{
    public class ManageEmployee 
    {
        private static IQBusinessEntities _entities;

        public ManageEmployee(IQBusinessEntities entities)
        {
            _entities = entities;
        }

        public void Update(EmployeeManagement.Employee emp)
        {
         
            _entities.Entry(emp).State = System.Data.Entity.EntityState.Modified;
        }
        public void Add(EmployeeManagement.Employee emp)
        {
            _entities.Employees.Add(emp);
        }

        public void Delete(EmployeeManagement.Employee emp)
        {
            _entities.Employees.Attach(emp);
            _entities.Employees.Remove(emp);
        }

     /*   public Employee SelectSingle(int id)
        {
            Employee results = (Employee)_entities.Employees.Where(x => x.EmpID == id);

            return results;
        }

        public List<EmployeeManagement.Employee> SelectAll()
        {
            var list = _entities.Employees.ToList();
            return list;
        }*/
    }
}