using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication3.Filters;
using WebApplication3.ViewModels;
using WebApplication3.ViewModel;
using OldViewModel = WebApplication3.ViewModel.SPA;
using WebApplication3.Models;

namespace WebApplication3.Areas.SPA.Controllers
{
    public class MainController: Controller
    {
        public ActionResult Index()
        {
            OldViewModel.MainViewModel v = new OldViewModel.MainViewModel();
            v.UserName = User.Identity.Name;
            v.FooterData = new FooterViewModel();
            v.FooterData.CompanyName = "KajukenboInfo.com";
            v.FooterData.Year = DateTime.Now.Year.ToString();
            return View("Index", v);

        }

        [AdminFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel v = new CreateEmployeeViewModel();
            return PartialView("CreateEmployee", v);

        }

        public ActionResult EmployeeList()
        {
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            List<Employee> employees = empBal.GetEmployees();

            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();

            foreach (Employee emp in employees)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.ToString("C");
                if(emp.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";

                }
                else
                {
                    empViewModel.SalaryColor = "green";

                }
                empViewModels.Add(empViewModel);

            }
            employeeListViewModel.Employees = empViewModels;
            return View("EmployeeList", employeeListViewModel);

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
    }
}