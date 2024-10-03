using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        Connect conn = new();

        [HttpGet]
        public List<Product> Get()
        {
            List<Product> products = new List<Product>();
            string sql = "SELECT * FROM products";
            
            conn.Connection.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn.Connection);
            MySqlDataReader dr = cmd.ExecuteReader();

            dr.Read();

            do
            {
                Product product = new Product();

                product.Id = dr.GetGuid(0);
                product.Name = dr.GetString(1);
                product.Price = dr.GetInt32(2);
                product.CreatedTime = dr.GetDateTime(3);

                products.Add(product);

            } while (dr.Read());

            conn.Connection.Close();

            return products;
        }
    }
}
