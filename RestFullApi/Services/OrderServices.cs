using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestFullApi.Authentication;
using RestFullApi.DataBase;
using RestFullApi.Models;
using System.Diagnostics;

namespace RestFullApi.Services
{
    public class OrderServices: IOrderServices
    {
        private readonly DataBaseContext _dbContext;
        public OrderServices(DataBaseContext dbContext)
        {
            _dbContext= dbContext;
        }


        // Get all Orders
        public async Task<List<Order>> AllOrders()
        {

            if (_dbContext.Orders == null)
            {
                return null;
            }
            return await _dbContext.Orders.ToListAsync();

        }

        // Get Order with {id}
        public async Task<Order> SpecficOrder(int id)
        {

            if (_dbContext.Orders == null)
            {
                return null;
            }

            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return null;
            }
            return order;

        }

      // Place Order

        public async Task<Order> PlaceOrder(Order order)
        {
            try
            {

                _dbContext.Orders.Add(order);

                foreach (var product in order.OrderedProducts)
                {
                    _dbContext.OrderedProducts.Add(product);
                    await _dbContext.SaveChangesAsync();
                }

                await _dbContext.SaveChangesAsync();
                return order;


            }
            catch (Exception ex)
            {

                Debug.WriteLine($"EXCEPTION:{ex}");
            }
            return null;
        }


        //Update Order
        public async Task<Order> UpdateOrder(int id, Order order)
        {

            if (id != order.OrderID)
            {
                return null;
            }

            foreach (var product in order.OrderedProducts)
            {
                if (!productAvailable(product.ProductID, order))
                {
                    return null;
                }
                _dbContext.Entry(product).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }

            _dbContext.Entry(order).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderAvailable(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }



            return order;
        }

        private bool productAvailable(int id, Order order)
        {
            return (order.OrderedProducts?.Any(x => x.ProductID == id)).GetValueOrDefault();
        }
        private bool OrderAvailable(int id)
        {
            return (_dbContext.Orders?.Any(x => x.OrderID == id)).GetValueOrDefault();
        }


      // Cancel Order
        public async Task<Order> DeleteOrder(int id)
        {
            if (_dbContext.Orders == null)
            {
                return null;
            }

            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return null;
            }


            var products = await _dbContext.OrderedProducts.ToListAsync();

            if (products != null)
            {
                foreach (var product in products)
                {
                    if (product.OrderID == id)
                    {
                        _dbContext.OrderedProducts.Remove(product);
                        await _dbContext.SaveChangesAsync();
                    }

                }

            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }
    }
}
