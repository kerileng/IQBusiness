namespace EmployeeManagement.Models.Workers.Repository
{
    public class DatabaseTransact 
    {
        private static ManageEmployee _manageEmployee = default;
        private static IQBusinessEntities _entities = default;

        public DatabaseTransact(IQBusinessEntities entities)
        {
            _entities = entities;
        }

        public ManageEmployee ManageEmployee
        {
            get
            {
                //lock(_manageEmployee)
               
                if (_manageEmployee == null)
                {
                    _manageEmployee = new ManageEmployee(_entities);
                }

                return _manageEmployee;
            }
        }

        
    }
}