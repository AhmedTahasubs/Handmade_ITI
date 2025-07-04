using HandmadeITI.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HandmadeITI.Repos
{
    public interface IOrdersRepo
    {
        List<Order> GetAllOrders();
        Order GetOrder(int id);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
        void Save();
        bool OrderExists(int id);

    }
}
