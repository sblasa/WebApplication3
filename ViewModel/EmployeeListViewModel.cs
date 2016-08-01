using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.ViewModels;

namespace WebApplication3.ViewModel
{
    public class EmployeeListViewModel:BaseViewModel
    {
        public List<EmployeeViewModel> Employees {get; set; }

    }
}
