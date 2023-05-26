using RestFullApi.Models;

namespace RestFullApi.Services
{
    public interface IOrderServices
    {
        Task<List<Order>> AllOrders();
        Task<Order> SpecficOrder(int id);
        Task<Order> PlaceOrder(Order order);
        Task<Order> UpdateOrder(int id, Order order);
        Task<Order> DeleteOrder(int id); 
    }
}
