using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.ViewModel.SPA;
using WebApplication3.ViewModels;

namespace WebApplication3.ViewModel.SPA
{
    public class MainViewModel
    {
        public string UserName { get; set; }
        public FooterViewModel FooterData { get; set; } //New property

    }
}

