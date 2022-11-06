using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laba_5.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public string Date { get; set; }
        
        public int User_Id { get; set; }
        public virtual User User { get; set; }
    }
}
