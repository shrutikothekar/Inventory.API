using Inventory.API.Data;
using Inventory.API.Models;
using Inventory.API.Services;
using Inventory.API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Data;

namespace Inventory.API.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(IDbConnectionFactory dbFactory)
            : base(dbFactory)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int pageNo, int pageSize)
        {
            List<Product> Product = new();

            using var con = GetConnection();
            using var cmd = new SqlCommand("dbo.usp_GetProductsPaged", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PageNo", SqlDbType.Int).Value = pageNo;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;

            await con.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Product.Add(new Product
                {
                    ProductId = reader.GetInt64(reader.GetOrdinal("productId")),
                    ProductCode = reader.GetString(reader.GetOrdinal("productCode")),
                    ProductName = reader.GetString(reader.GetOrdinal("productName")),
                    Price = reader.GetDecimal(reader.GetOrdinal("price"))
                });
            }

            return Product;
        }

    

        //// 🔹 GET ALL PRODUCTS (Reader – FAST)
        //public async Task<IEnumerable<Product>> GetProductsAsync()
        //{
        //    var products = new List<Product>();

        //    using var con = GetConnection();
        //    using var cmd = new SqlCommand(
        //        "SELECT ProductId, ProductCode, ProductName, Price FROM dbo.Product", con);

        //    await con.OpenAsync();
        //    using var reader = await cmd.ExecuteReaderAsync();

        //    if (!reader.HasRows)
        //    {
        //        Console.WriteLine("NO ROWS FOUND");
        //        return products;
        //    }

        //    while (await reader.ReadAsync())
        //    {
        //        products.Add(new Product
        //        {
        //            ProductId = reader.GetInt64(reader.GetOrdinal("ProductId")),
        //            ProductCode = reader.GetString(reader.GetOrdinal("ProductCode")),
        //            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
        //            Price = reader.GetDecimal(reader.GetOrdinal("Price"))
        //        });
        //    }

        //    return products;
        //}


        // 🔹 GET BY ID (Parameterized Query)
        public async Task<Product?> GetProductByIdAsync(long id)
        {
            using var con = GetConnection();
            using var cmd = new SqlCommand(
                "SELECT ProductId, ProductCode, ProductName, Price FROM Product WHERE ProductId=@Id", con);

            cmd.Parameters.AddWithValue("@Id", id);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Product
                {
                    ProductId = reader.GetInt64(0),
                    ProductCode = reader.GetString(1),
                    ProductName = reader.GetString(2),
                    Price = reader.GetDecimal(3)
                };
            }

            return null;
        }

        // 🔹 INSERT (Transaction + Parameters)
        public async Task AddProductAsync(Product product)
        {
            using var con = GetConnection();
            await con.OpenAsync();

            using var tran = con.BeginTransaction();
            try
            {
                var cmd = new SqlCommand(
                    @"INSERT INTO Product (ProductCode, ProductName, Price) 
                      VALUES (@Code, @Name, @Price)", con, tran);

                cmd.Parameters.AddWithValue("@Code", product.ProductCode);
                cmd.Parameters.AddWithValue("@Name", product.ProductName);
                cmd.Parameters.AddWithValue("@Price", product.Price);

                await cmd.ExecuteNonQueryAsync();
                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        // 🔹 UPDATE
        public async Task UpdateProductAsync(Product product)
        {
            using var con = GetConnection();
            using var cmd = new SqlCommand(
                @"UPDATE Product 
                  SET ProductCode=@Code, ProductName=@Name, Price=@Price 
                  WHERE ProductId=@Id", con);

            cmd.Parameters.AddWithValue("@Id", product.ProductId);
            cmd.Parameters.AddWithValue("@Code", product.ProductCode);
            cmd.Parameters.AddWithValue("@Name", product.ProductName);
            cmd.Parameters.AddWithValue("@Price", product.Price);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        // 🔹 DELETE
        public async Task DeleteProductAsync(long id)
        {
            using var con = GetConnection();
            using var cmd = new SqlCommand(
                "DELETE FROM Product WHERE ProductId=@Id", con);

            cmd.Parameters.AddWithValue("@Id", id);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task BulkInsertProductsAsync(DataTable productTable)
        {
            using var con = GetConnection();
            await con.OpenAsync();

            using var tran = con.BeginTransaction();

            try
            {
                using var bulkCopy = new SqlBulkCopy(con,SqlBulkCopyOptions.TableLock,tran);

                bulkCopy.DestinationTableName = "Product";
                bulkCopy.BatchSize = 5000;
                bulkCopy.BulkCopyTimeout = 60;

                bulkCopy.ColumnMappings.Add("ProductCode", "ProductCode");
                bulkCopy.ColumnMappings.Add("ProductName", "ProductName");
                bulkCopy.ColumnMappings.Add("Price", "Price");

                await bulkCopy.WriteToServerAsync(productTable);

                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }

        public Task<long> DeleteProductAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProductAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> AddProductAsync()
        {
            throw new NotImplementedException();
        }

        public Task<long> GetProductByIdAsync()
        {
            throw new NotImplementedException();
        }

        Task<long> IProductRepository.AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        Task<bool> IProductRepository.UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        Task<bool> IProductRepository.DeleteProductAsync(long productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsPagedAsync(int pageNo, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
