using System.ComponentModel.Design;

namespace RestFullApi.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string? OrderDate { get; set; }
        public string? Status { get; set; }
        public List<OrderedProduct>? OrderedProducts { get; set; } 
    }
}
