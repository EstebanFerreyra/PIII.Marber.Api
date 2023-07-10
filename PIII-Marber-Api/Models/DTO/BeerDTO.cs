using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class BeerDTO
    {
        public int Id { get; set; }
        public string BeerName { get; set; }
        public string BeerStyle { get; set; }
        public decimal Price { get; set; }
    }
}
