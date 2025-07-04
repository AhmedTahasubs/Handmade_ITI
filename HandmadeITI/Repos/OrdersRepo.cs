using HandmadeITI.Core.Models;
using HandmadeITI.Data;
using Microsoft.EntityFrameworkCore;

namespace HandmadeITI.Repos
{
    public class OrdersRepo : IOrdersRepo
    {
        private readonly ApplicationDbContext db;

        public OrdersRepo(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void AddOrder(Order order)
        {
            db.Add(order);
        }

        public void DeleteOrder(int id)
        {
            Order? o = db.Order.Find(id);
            if (o == null)
            {
                throw new Exception("order not found");
            }
            db.Order.Remove(o);
        }

        public List<Order> GetAllOrders()
        {
           return db.Order.Include(o => o.User).ToList();
        }

        public Order GetOrder(int id)
        {
            return db.Order.Include(o => o.User).FirstOrDefault(o => o.OrderId == id);
        }

        public void UpdateOrder(Order order)
        {
             db.Order.Update(order);
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public bool OrderExists(int id)
        {
            return db.Order.Any(e => e.OrderId == id);
        }
    }
}
