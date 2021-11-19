using DeliverySystem.API.Helpers;
using DeliverySystem.API.Models.RequestModels;
using DeliverySystem.API.Models.ResponseModels;
using System;
using System.Collections.Generic;

namespace DeliverySystem.API.Services.Abstracts
{
    /// <summary>
    /// Interface of the service layer of deliveries
    /// </summary>
    public interface IDeliveryService
    {
        /// <summary>
        /// Get deliveries by account
        /// </summary>
        /// <param name="accountId">
        /// User or partner Identifier
        /// </param>
        /// <param name="accountType">
        /// User or partner
        /// </param>
        /// <returns>
        /// Delivery response object
        /// </returns>
        public IEnumerable<DeliveryResponse> GetAll(Guid accountId, Enums.AccountType accountType);

        /// <summary>
        /// Get delivery by delivery identifier and account
        /// </summary>
        /// <param name="id">
        /// Delivery Identifier
        /// </param>
        /// <param name="accountId">
        /// User or partner Identifier
        /// </param>
        /// <param name="accountType">
        /// User or partner
        /// </param>
        /// <returns>
        /// Delivery response object
        /// </returns>
        public DeliveryResponse GetById(Guid id, Guid accountId, Enums.AccountType accountType);

        /// <summary>
        /// Generating delivery
        /// If order not eixsts return 
        /// Only partner can start this process
        /// Create delivery action log in created state and partner identifier
        /// </summary>
        /// <returns>
        /// Delivery response object
        /// </returns>
        DeliveryResponse Create(DeliveryRequest model, Guid accountId, Enums.AccountType accountType);

        /// <summary>
        /// Updating the delivery
        /// If delivery is not exists return
        /// If end time of the delivery passed set state to expired
        /// User can change the state to approved if delivery in created state
        /// Partner can change the state to completed if delivery in approved state
        /// All action store in delivery action table with given identifier and account identifier
        /// </summary>
        DeliveryResponse Update(DeliveryRequest model, Guid accountId, Enums.AccountType accountType);

        /// <summary>
        /// Cancelling the delivery
        /// If delivery is not exists return
        /// User or partner can do this action
        /// This process can be valid if delivery only in approved or created states
        /// Create delivery action log for user or partner and in cancelled state 
        /// </summary>
        DeliveryResponse Delete(Guid id, Guid accountId, Enums.AccountType accountType);
    }
}
