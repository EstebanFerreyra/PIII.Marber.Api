using Models.DTO;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IOrderService
    {
        List<OrderDTO> GetOrders();
        bool AddOrder(int id, List<OrderViewModel> orders, int userId);
        bool UpdatePriceById(int id, ModifyPriceViewModel modifyPriceViewModel);   
        bool DeleteOrderById(int id);
    }
}
