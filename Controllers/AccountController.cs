using DeliverySystem.API.Helpers;
using DeliverySystem.API.Models.RequestModels;
using DeliverySystem.API.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    /// <summary>
    /// In this controller you can find all account actions
    /// </summary>
    [ApiController]
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        /// <summary>
        /// Injection of service layer
        /// </summary>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Login of user 
        /// </summary>
        /// <param name="model">
        /// Email
        /// Password
        /// </param>
        /// <returns>
        /// Authenticated user object
        /// </returns>
        [HttpPost("users/authenticate")]
        public IActionResult AuthenticateUser(AuthenticateRequest model)
        {
            var response = _accountService.Authenticate(model, Enums.AccountType.user);
            if (response == null)
            {
                return BadRequest(new { message = "Email or password is incorrect" });
            }
            return Ok(response);
        }

        /// <summary>
        /// Login of partner
        /// </summary>
        /// <param name="model">
        /// Email
        /// Password
        /// </param>
        /// <returns>
        /// Authenticated partner object
        /// </returns>
        [HttpPost("partners/authenticate")]
        public IActionResult AuthenticatePartner(AuthenticateRequest model)
        {
            var response = _accountService.Authenticate(model, Enums.AccountType.partner);
            if (response == null)
            {
                return BadRequest(new { message = "Email or password is incorrect" });
            }
            return Ok(response);
        }

        /// <summary>
        /// Signup of user
        /// </summary>
        /// <param name="model">
        /// FirstName
        /// LastName 
        /// PhoneNumber
        /// Address
        /// Email
        /// Password
        /// </param>
        /// <returns>
        /// Registered user object
        /// </returns>
        [HttpPost("users/register")]
        public IActionResult RegisterUser(UserRequest model)
        {
            var response = _accountService.RegisterUser(model);
            if (response == null)
            {
                return BadRequest(new { message = "User is already exists" });
            }
            return Ok(response);
        }

        /// <summary>
        /// Signup of partner
        /// </summary>
        /// <param name="model">
        /// Name
        /// Description
        /// Email
        /// Password
        /// </param>
        /// <returns>
        /// Registered partner object
        /// </returns>
        [HttpPost("partners/register")]
        public IActionResult RegisterPartner(PartnerRequest model)
        {
            var response = _accountService.RegisterPartner(model);
            if (response == null)
            {
                return BadRequest(new { message = "Partner is already exists" });
            }
            return Ok(response);
        }
    }
}
