using DeliverySystem.API.Models.RequestModels;
using DeliverySystem.API.Models.ResponseModels;
using DeliverySystem.API.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DeliverySystem.API.Controllers
{
    /// <summary>
    /// In this controller you can find all delivery CRUD actions
    /// As you see I am using account for each methods to decompose partner and user behaviours
    /// </summary>
    [ApiController]
    [Route("deliveries")]
    public class DeliveryController : ControllerBase
    {
        private IDeliveryService _deliveryService;
        /// <summary>
        /// Injection of service layer
        /// </summary>
        /// <param name="deliveryService"></param>
        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        /// <summary>
        /// Generating delivery
        /// If order not eixsts return 
        /// Only partner can start this process
        /// Create delivery action log in created state and partner identifier
        /// </summary>
        /// <param name="model">
        /// OrderId
        /// StartTime
        /// EndTime
        /// </param>
        /// <returns>
        /// Related delivery object
        /// </returns>
        [Authorize]
        [HttpPost]
        public IActionResult Create(DeliveryRequest model)
        {
            var account = (AuthenticateResponse)HttpContext.Items["Account"];
            var response = _deliveryService.Create(model, Guid.Parse(account.Id), account.AccountType);
            if (response == null)
            {
                return BadRequest(new { message = "An error occured while creating the delivery" });
            }
            return Ok(response);
        }

        /// <summary>
        /// Updating the delivery
        /// If delivery is not exists return
        /// If end time of the delivery passed set state to expired
        /// User can change the state to approved if delivery in created state
        /// Partner can change the state to completed if delivery in approved state
        /// All action store in delivery action table with given identifier and account identifier
        /// </summary>
        /// <param name="model">
        /// DeliveryId
        /// DeliveryState
        /// </param>
        /// <returns>
        /// Related delivery object
        /// </returns>
        [Authorize]
        [HttpPut]
        public IActionResult Update(DeliveryRequest model)
        {
            var account = (AuthenticateResponse)HttpContext.Items["Account"];
            var response = _deliveryService.Update(model, Guid.Parse(account.Id), account.AccountType);
            if (response == null)
            {
                return BadRequest(new { message = "An error occured while updating the delivery" });
            }
            return Ok(response);
        }

        /// <summary>
        /// Cancelling the delivery
        /// If delivery is not exists return
        /// User or partner can do this action
        /// This process can be valid if delivery only in approved or created states
        /// Create delivery action log for user or partner and in cancelled state 
        /// </summary>
        /// <param name="id">
        /// Identifier of the delivery
        /// </param>
        /// <returns>
        /// Related delivery object
        /// </returns>
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var account = (AuthenticateResponse)HttpContext.Items["Account"];
            var response = _deliveryService.Delete(id, Guid.Parse(account.Id), account.AccountType);
            if (response == null)
            {
                return BadRequest(new { message = "An error occured while deleting the delivery" });
            }
            return Ok(response);
        }

        /// <summary>
        /// Getting related delivery by delivery id and user or partner info
        /// </summary>
        /// <param name="id">
        /// Identifier of the delivery
        /// </param>
        /// <returns>
        /// Related delivery object
        /// </returns>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var account = (AuthenticateResponse)HttpContext.Items["Account"];
            var response = _deliveryService.GetById(id, Guid.Parse(account.Id), account.AccountType);
            if (response == null)
            {
                return BadRequest(new { message = "Invalid delivery identifier" });
            }
            return Ok(response);
        }

        /// <summary>
        /// Getting all related deliveries by user or partner
        /// </summary>
        /// <returns>
        /// List of delivery object
        /// </returns>
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var account = (AuthenticateResponse)HttpContext.Items["Account"];
            var response = _deliveryService.GetAll(Guid.Parse(account.Id), account.AccountType);
            return Ok(response);
        }
    }
}
