using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string BeerName { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
    }
}
