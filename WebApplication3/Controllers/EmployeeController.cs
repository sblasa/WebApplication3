using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.ViewModels;
using WebApplication3.Filters;
using WebApplication3.ViewModel;
using OldViewModel = WebApplication3.ViewModels;


namespace WebApplication3.Controllers
{


    public class Customer
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return this.CustomerName + "|" + this.Address;
        }


    }
    public class EmployeeController : Controller
    {
        // GET: Test

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel employeeListViewModel = new CreateEmployeeViewModel();
            employeeListViewModel.FooterData = new FooterViewModel();
            employeeListViewModel.FooterData.CompanyName = "KajukenboInfo.com";
            employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
            employeeListViewModel.UserName = User.Identity.Name;
            return View("CreateEmployee", employeeListViewModel);

        }
        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            } 
            else
            {
                return new EmptyResult();
            }
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult SaveEmployee(Employee e, string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save Employee":
                    if (ModelState.IsValid)
                    {
                        EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
                        empBal.SaveEmployee(e);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        CreateEmployeeViewModel vm = new CreateEmployeeViewModel();
                        vm.FirstName = e.FirstName;
                        vm.LastName = e.LastName;
                        vm.FooterData = new FooterViewModel();
                        vm.FooterData.CompanyName = "KajkenboInfo.com";
                        vm.FooterData.Year = DateTime.Now.ToString();
                        vm.UserName = User.Identity.Name;
                        if (e.Salary > 0)
                        {
                            vm.Salary = e.Salary.ToString();

                        } 
                        else
                        {
                            vm.Salary = ModelState["Salary"].Value.AttemptedValue;

                        }

                        return View("CreateEmployee", vm);
                    }
               
                case "Cancel":
                    return RedirectToAction("Index");
            }
            return new EmptyResult();
        }


        [Authorize]
        [HeaderFooterFilter]
        [Route("Employee/List")]
        public ActionResult Index()
        {

            EmployeeBusinessLayer business = new EmployeeBusinessLayer();
            var employees = business.GetEmployees();

            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            employeeListViewModel.UserName = User.Identity.Name;
            List<EmployeeViewModel> employeeModelList = new List<EmployeeViewModel>();
            employeeListViewModel.Employees = employeeModelList;



            foreach (var employee in business.GetEmployees())
            {
                EmployeeViewModel temp = new EmployeeViewModel();
                temp.EmployeeName = employee.FirstName + " " + employee.LastName;
                temp.Salary = employee.Salary.ToString("C");
                if (employee.Salary > 150000)
                {
                    temp.SalaryColor = "yellow";
                }
                else
                {
                    temp.SalaryColor = "green";
                }
                

                employeeListViewModel.Employees.Add(temp);

            }
            employeeListViewModel.FooterData = new FooterViewModel();
            employeeListViewModel.FooterData.CompanyName = "S Blasa";
            employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index", employeeListViewModel);

        }


           // return View("MyView", emp);
        }


    }