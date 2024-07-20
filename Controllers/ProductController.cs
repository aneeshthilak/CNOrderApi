using CNOrderApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Dapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CNOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {


        private IConfiguration Configuration;
        private IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            Configuration = configuration;
            _configuration = configuration;
        }

        // GET: api/<ProductController>
        [HttpGet]
        [Route("GetProducts")]
        public async Task<ActionResult<List<Products>>> GetProdcts()
        {

               using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                IEnumerable<Products> Producs = await SelectAllHeroes(connection);
                return Ok(Producs);
 
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ActionResult<List<Products>>> CreateProduct(Products product)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("insert into Products (ProductName, Colour, Size) values (@ProductName, @Colour, @Size)", product);
            return Ok(await SelectAllHeroes(connection));
        }

        private static async Task<IEnumerable<Products>> SelectAllHeroes(SqlConnection connection)
        {
            return await connection.QueryAsync<Products>("select * from Products");
        }

     
        [HttpGet]
        [Route("GetProductsAll")]
        public List<Products> GetProdctsAll()
        {
            List<Products> products = new List<Products>();
            SqlConnection connection = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd;
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("select * from Products", connection);
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Products prod = new Products();
                    prod.ProductId = int.Parse(dt.Rows[i]["ProductID"].ToString());
                    prod.ProductName = dt.Rows[i]["ProductName"].ToString();
                    prod.Colour = dt.Rows[i]["Colour"].ToString();
                    prod.Size = dt.Rows[i]["Size"].ToString();

                    products.Add(prod);

                }
                
            }
            catch (Exception ef)
            {
                
            }
            finally
            {
                connection.Close();
            }
            return products;
        }

        // POST api/<ProductController>
        [HttpPost]
        [Route("SaveProduct")]
        public string Post(Products product)
        {
            SqlConnection connection = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("insert into Products (ProductName, Colour, Size) values('" + product.ProductName + "','" + product.Colour + "','" + product.Size + "')", connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            return "Product added successfully";
        }

     
    }
}
