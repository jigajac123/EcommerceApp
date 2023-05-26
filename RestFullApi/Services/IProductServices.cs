using Microsoft.AspNetCore.Mvc;
using RestFullApi.Models;

namespace RestFullApi.Services
{
    public interface IProductServices
    {
        Task<List<Product>> AllProducts();
        Task<Product> SpecficProduct(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(int id, Product product);
        Task<Product> DeleteProduct(int id);
    }
}
