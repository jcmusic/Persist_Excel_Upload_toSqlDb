using DAL;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using UploadExcelFile;

namespace CRUD_WebApp_SQL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IJCM_Repo _repo;
        private readonly ILogger<UploadController> _logger;

        public UploadController(IJCM_Repo repo, ILogger<UploadController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpPost(Name = "Orders/Excel")]
        public async Task<IActionResult> Post([FromForm] FileUploadRequest fileUploadRequest, bool testDeserializationOnly = false)
        {
            try
            {
                var orderList = ExcelDeserializer.Deserialize<OrderImportDto>(fileUploadRequest.File);
                if (orderList.Count == 0)
                {
                    return BadRequest("No orders found in the file");
                }

                if (testDeserializationOnly)
                {
                    return Ok(orderList);
                }
                else
                {
                    await _repo.AddOrdersAsync(orderList);
                    return Ok();
                }
            }
            catch (Exception e)
            {
                if (testDeserializationOnly)
                {
                    return BadRequest(e.Message);
                }
                _logger.LogError(e, "Error while processing the file");
                return StatusCode(500, "Error while processing the file");
            }
        }
    }
}