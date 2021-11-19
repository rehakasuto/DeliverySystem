using DeliverySystem.API.Helpers;
using DeliverySystem.API.Models.RequestModels;
using DeliverySystem.API.Models.ResponseModels;
using System;

namespace DeliverySystem.API.Services.Abstracts
{
    /// <summary>
    /// Interface of the service layer of accounts
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Login of account 
        /// </summary>
        /// <param name="model">
        /// Email
        /// Password
        /// </param>
        /// <param name="accountType">
        /// Partner or user
        /// </param>
        /// <returns>
        /// Authenticated account object
        /// </returns>
        AuthenticateResponse Authenticate(AuthenticateRequest model, Enums.AccountType accountType);

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
        AuthenticateResponse RegisterUser(UserRequest model);

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
        AuthenticateResponse RegisterPartner(PartnerRequest model);

        /// <summary>
        /// Get account by account identifier
        /// </summary>
        /// <param name="Id">
        /// Identifier of the account
        /// </param>
        /// <returns>
        /// Authenticated account object
        /// </returns>
        AuthenticateResponse GetById(Guid Id);
    }
}
