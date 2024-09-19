using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.ViewModels
{
    public class ShoppingCardVM
    {

       public IEnumerable<ShoppingCard> ShoppingCards { get; set; }

        public double TotalOrder {  get; set; }
    }
}
