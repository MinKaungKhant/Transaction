using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core;
using Application;
using Infrastructure;

namespace WebMvc.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {

        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionService _transactionService;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult GetTransactions()
        {
            var data = _transactionService.GetTransactions();

            return Ok(data);
        }

        [HttpGet("getbycurrency")]
        public IActionResult GetByCurrency([FromQuery(Name = "currency")] string currency)
        {
            var data = _transactionService.GetTransactionsByCurrency(currency);

            return Ok(data);
        }

        [HttpGet("getbydate")]
        public IActionResult GetByDateRange([FromQuery(Name = "startdate")] string startdate, [FromQuery(Name = "enddate")] string enddate)
        {
            var data = _transactionService.GetTransactionsByDateRange(startdate, enddate);

            return Ok(data);
        }

        [HttpGet("getbystatus")]
        public IActionResult GetByStatus([FromQuery (Name = "status")] string status)
        {
            var data = _transactionService.GetTransactionsByStatus(status);

            return Ok(data);
        }

        //[HttpGet(Name = "GetByCurrency")]
        //public IEnumerable<Transaction> GetByCurrency()
        //{
        //    return _transactionService.GetTransactions();
        //}

        //[HttpGet("GetByName")]
        //public async Task<IActionResult> GetByName()
        //{
        //    return Ok();
        //}
    }
}
