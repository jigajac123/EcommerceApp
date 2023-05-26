using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestFullApi.Authentication;
using RestFullApi.DataBase;
using RestFullApi.Models;
using System.Diagnostics;

namespace RestFullApi.Services
{
    public class ProductServices : IProductServices
    {
        private readonly DataBaseContext _dbContext;
        public ProductServices(DataBaseContext dbContext)
        {
                _dbContext= dbContext;
        }

        //Get all products
        public async Task<List<Product>> AllProducts()
        {

            if (_dbContext.Products == null)
            {
                return null;
            }
            return await _dbContext.Products.ToListAsync();

        }


        //Get method with {id}
        public async Task<Product> SpecficProduct(int id)
        {

            if (_dbContext.Products == null)
            {
                return null;
            }

            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }
            return product;

        }

       // Post 
        public async Task<Product> CreateProduct(Product product)
        {
            try
            {
                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"EXCEPTION:{ex}");
            }
            return null;
        }


       //Update

        public async Task<Product> UpdateProduct(int id, Product product)
        {

            if (id != product.ProductID)
            {
                return null;
            }

            _dbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productAvailable(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return product;
        }

        private bool productAvailable(int id)
        {
            return (_dbContext.Products?.Any(x => x.ProductID == id)).GetValueOrDefault();
        }


        //Delete 
        public async Task<Product> DeleteProduct(int id)
        {
            if (_dbContext.Products == null)
            {
                return null;
            }

            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            try
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
            return product;
        }
    }
}
