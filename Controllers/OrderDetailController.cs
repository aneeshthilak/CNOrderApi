using CNOrderApi.Interfaces;
using CNOrderApi.Models;
using CNOrderApi.Repositories;
using Dapper;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Net;

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
        public async Task<IActionResult> GetOrderDetail(CustomerInfo custInfo)
        {
            try
            {
                string emailId = string.Empty;
                var httpContext = HttpContext;
                //httpContext.Request.Headers..TryGetValue(UserContext, out var contextApiKey)
                httpContext.Request.Headers.TryGetValue("UserEmail", out var contextEmail);
                emailId = contextEmail;

                if (contextEmail.Equals(string.Empty))
                {
                    return StatusCode(401, "Request header not contains email Id");
                }
                if (custInfo.UserEmail != emailId)
                {
                    return StatusCode(401, "Customer is not the logged in user");
                }

                List<Customer> customerList = _OrderDetailRepo.GetCustomer().Result;

                Customer customer = customerList.Find(item => item.Email == emailId);

                if (customer == null)
                {
                    return StatusCode(401, "Customer not found with this email id");
                }
                var orderDetails = await _OrderDetailRepo.GetOrderDetail(customer.CustomerId);

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






    }
}
