using CNOrderApi.Interfaces;
using CNOrderApi.Models;
using CNOrderApi.Repositories;
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

        //private IConfiguration _configuration;
        private readonly IOrderDetailRepository _OrderDetailRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="orderDetailRepo"></param>
        public OrderDetailController(IConfiguration configuration, IOrderDetailRepository orderDetailRepo)
        {
            
            _OrderDetailRepo = orderDetailRepo;
            //_configuration = configuration;
        }

        [HttpPost]
        [Route("GetOrderDetail")]
        public async Task<IActionResult> GetOrderDetail(int customerId)
        {
            try
            {
                var orderDetails = await _OrderDetailRepo.GetOrderDetail(customerId);
                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        [Route("GetCoustmerOrder")]
        public async Task<IActionResult> GetCoustmerOrder(int customerId)
        {
            try
            {
                var orderDetails = await _OrderDetailRepo.GetCustomerOrder(customerId);
                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
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


        //// POST api/<ProductController>
        //[HttpPost]
        //[Route("GetOrderDetail")]
        //public async Task<IActionResult> GetOrderDetail(CustomerInfo customerInfo)
        //{
        //    List<Products> products = new List<Products>();
        //    SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        //    SqlCommand cmd;
        //    //SqlCommand cmd = new SqlCommand("select * from Products", connection);
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter da;
        //    try
        //    {
        //        cmd = new SqlCommand("GetCustomerOrder", connection);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@CustomerID", customerInfo.CustomerId);
        //        da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        //SqlConnection connection = new SqlConnection(this.Configuration.GetConnectionString("DefaultConnection"));
        //        //SqlCommand cmd = new SqlCommand("insert into Products (ProductName, Colour, Size) values('" + product.ProductName + "','" + product.Colour + "','" + product.Size + "')", connection);
        //        //connection.Open();
        //        //cmd.ExecuteNonQuery();
        //        //connection.Close();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            Products prod = new Products();
        //            prod.ProductId = int.Parse(dt.Rows[i]["ProductID"].ToString());
        //            prod.ProductName = dt.Rows[i]["ProductName"].ToString();
        //            prod.Colour = dt.Rows[i]["Colour"].ToString();
        //            prod.Size = dt.Rows[i]["Size"].ToString();

        //            products.Add(prod);

        //        }
        //    }
        //    catch
        //    {

        //    }

        //    return Ok(products);
        //}



    }
}
