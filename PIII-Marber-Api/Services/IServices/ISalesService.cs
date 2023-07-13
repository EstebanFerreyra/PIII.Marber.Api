using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ISalesService
    {
        List<BeerDTO> GetTopSoldProducts();
        List<UserDTO> GetBuyers();
    }
}
