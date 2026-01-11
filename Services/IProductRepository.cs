using Inventory.API.Data;
using Inventory.API.Models;
using System;
using System.Data;


namespace Inventory.API.Services
{
    public interface IProductRepository
    {
        //Task<long> DeleteProductAsync();
        //Task<Product> UpdateProductAsync();
        //Task<Product> AddProductAsync();
        //Task<long> GetProductByIdAsync();
        //Task<IEnumerable<Product>> GetProductsAsync();
        //Task BulkInsertProductsAsync(DataTable productTable);

        // -------- BASIC CRUD --------
        Task<long> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(long productId);
        Task<Product?> GetProductByIdAsync(long productId);
        Task<IEnumerable<Product>> GetProductsAsync();

        // -------- HIGH VOLUME READ --------
        Task<IEnumerable<Product>> GetProductsPagedAsync(
            int pageNo,
            int pageSize);//not implemented

        // -------- BULK OPERATIONS --------
        Task BulkInsertProductsAsync(DataTable productTable);

    }
}
