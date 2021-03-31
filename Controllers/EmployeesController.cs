using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeManagement;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private static readonly IQBusinessEntities db = new IQBusinessEntities();

        public ActionResult Index()
        {
            IList<Employee> employees = null;
            HttpClient client = null;
            try
            {
                if (ModelState.IsValid)
                {
                    client = new HttpClient
                    {
                        BaseAddress = new Uri("https://localhost:44336/api/")
                    };

                    var getEmpResponse = client.GetAsync("employee");
                    getEmpResponse.Wait();

                    var result = getEmpResponse.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Employee>>();
                        readTask.Wait();

                        employees = readTask.Result;
                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        employees = null;

                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                client.Dispose();
            }
            return View(employees);
        }


        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(@"https://localhost:44336/api/")
                };

                var createResponse = client.PostAsJsonAsync<Employee>("employee", emp);
                createResponse.Wait();

                var result = createResponse.Result;

                if (result.IsSuccessStatusCode)
                {
                    return Redirect("Index");
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }

            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee emp = EmpById(id);

            return View(emp);
        }

        public Employee EmpById(int id)
        {
            Employee emp = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://localhost:44336/api/");
                //HTTP GET
                var editResponse = client.GetAsync
                    ($"employee?id={id}");
                editResponse.Wait();

                var result = editResponse.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Employee>();
                    readTask.Wait();

                    emp = readTask.Result;
                }
            }
            return emp;
        }

        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(@"https://localhost:44336/api/");

                //HTTP POST
                var putResponse = client.PutAsJsonAsync<Employee>("employee", emp);
                putResponse.Wait();

                var result = putResponse.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
                
            }
            return View(emp);
        }

       /* [HttpGet]
        public ActionResult Delete(int id)
        {
           
            var emp = EmpById(id);
            TempData["id"] = emp.EmpID;
            return View(emp);
        }*/
     

        public ActionResult Delete(int id)
        {
            var emp = EmpById(id);


            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(@"https://localhost:44336/api/")
            };

            var delqry = client.DeleteAsync($"employee?id={id}");
            var results = delqry.Result;

            if (results == null)
            {
                return HttpNotFound();
            }

            ViewBag.Message = $"Employee {emp.EmpID} {emp.Name} was successfully deleted";
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
