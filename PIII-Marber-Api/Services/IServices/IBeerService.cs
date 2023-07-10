using Models.DTO;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IBeerService
    {
        List<BeerDTO> GetListBeer();
        BeerDTO GetBeerById(int id);
        bool ModifyPriceBeerById(int id, decimal newPrice);
        BeerDTO AddBeer(AddBeerViewModel beer);
        bool DeleteBeerById(int id);
    }
}
