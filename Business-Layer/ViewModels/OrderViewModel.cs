//using Business_Layer.ViewModels;
using Data_Layer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerceSystem_API.ViewModels;
using System;
using System.Collections.Generic;

namespace E_commerceSystem_API.ViewModels
{
    public class OrderViewModel
    {
        public int orderID { get; set; }
   
        public int customerID { get; set; } // Foreign key
        public DateTime orderDate { get; set; }
        public List<ProductViewModel> products { get; set; }
    }
}


