using DAL;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_WebApp_SQL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        #region Ctor/Fields
        private readonly IJCM_Repo _repo;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IJCM_Repo repo, ILogger<OrdersController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        #endregion

        // Just GET Endpoints to validate upload persistance


        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders(int pageNumber = 0, int pageSize = 15)
        {
            try
            {
                var (orderslist, paginationMetadata) = await _repo.GetOrdersAsync(pageNumber, pageSize);

                Response.Headers.Append("X-Pagination",
                    System.Text.Json.JsonSerializer.Serialize(paginationMetadata));

                return Ok(orderslist);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while processing the file");
                return StatusCode(500, "Error while processing the file");
            }
        }


        [HttpGet("{orderId}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> GetOrderById(int orderId)
        {
            try
            {
                var order = await _repo.GetOrderAsync(orderId);

                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while processing the file");
                return StatusCode(500, "Error while processing the file");
            }
        }
    }
}