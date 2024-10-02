
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Business_Layer.ViewModels;
using Data_Layer.DataModel;
using E_commerceSystem_API.ViewModels;

namespace E_commerceSystem_API.ViewModels
{
    public class CustomerViewModel
    {
        public int customerID { get; set; }
      
        public string customerName { get; set; }
        public ICollection<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
    }
}

