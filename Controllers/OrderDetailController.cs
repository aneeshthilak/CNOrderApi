using CNOrderApi.Interfaces;
using CNOrderApi.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CNOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {

        private IConfiguration _configuration;

        private readonly IOrderDetailRepository _OrderDetailRepo;

        public OrderDetailController(IConfiguration configuration, IOrderDetailRepository orderDetailRepo)
        {
            _configuration = configuration;
            _OrderDetailRepo = orderDetailRepo;
        }

        [HttpGet]
        [Route("GetCustomer")]
        public async Task<IActionResult> GetCustomer()
        {
            try
            {
                var customer = await _OrderDetailRepo.GetCustomer();
                return Ok(customer);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        //// POST api/<OrderDetailController>
        //[HttpPost]
        //[Route("GetCustomer")]

        //public async Task<IEnumerable<Customer>> GetCustomer(CustomerInfo customerInfo)
        //{
        //    //string connString = _configuration.GetConnectionString("DefaultConnection");
        //    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        //Set up DynamicParameters object to pass parameters  
        //        DynamicParameters parameters = new DynamicParameters();
        //        parameters.Add("id", 1);

        //        //Execute stored procedure and map the returned result to a Customer object  
        //        var customer = connection.QuerySingleOrDefault<Customer>("GetCustomerOrder", parameters, commandType: CommandType.StoredProcedure);
        //        return customer;
        //    }

        //}

        //public async Task<IActionResult> GetEmployee(int id)
        //{
        //    var result = await _employeeService.GetEmployee(id);

        //    return Ok(result);
        //}


        // POST api/<ProductController>
        [HttpPost]
        [Route("GetOrderDetail")]
        public async Task<IActionResult> GetOrderDetail(CustomerInfo customerInfo)
        {
            List<Products> products = new List<Products>();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd;
            //SqlCommand cmd = new SqlCommand("select * from Products", connection);
            DataTable dt = new DataTable();
            SqlDataAdapter da;
            try
            {
                cmd = new SqlCommand("GetCustomerOrder", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", customerInfo.CustomerId);
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                //SqlConnection connection = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection"));
                //SqlCommand cmd = new SqlCommand("insert into Products (ProductName, Colour, Size) values('" + product.ProductName + "','" + product.Colour + "','" + product.Size + "')", connection);
                //connection.Open();
                //cmd.ExecuteNonQuery();
                //connection.Close();
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
            catch
            {

            }

            return Ok(products);
        }


        // GET: api/<OrderDetailController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderDetailController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderDetailController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrderDetailController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderDetailController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
