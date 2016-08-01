using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.DataAccessLayer;



namespace WebApplication3.Models
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            return salesDal.Employees.ToList();

        }


        public void UploadEmployees(List<Employee> employees)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.AddRange(employees);
            salesDal.SaveChanges();
        }
    public UserStatus GetUserValidity(UserDetails u)
        {

            if (u.UserName == "Admin" && u.Password =="Admin")
        {
            return UserStatus.AuthenticatedAdmin;
        }
        else if (u.UserName == "Sue" && u.Password == "wow")
        {
            return UserStatus.AuthenticatedUser;

        } else
        {
                return UserStatus.NonAuthenticatedUser;
        }

   }

        public Employee SaveEmployee(Employee e)
        {
            SalesERPDAL salesDal = new SalesERPDAL();
            salesDal.Employees.Add(e);
            salesDal.SaveChanges();
            return e;
        }
    }
}