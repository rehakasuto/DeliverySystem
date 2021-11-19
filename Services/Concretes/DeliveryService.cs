using DeliverySystem.API.Helpers;
using DeliverySystem.API.Models;
using DeliverySystem.API.Models.RequestModels;
using DeliverySystem.API.Models.ResponseModels;
using DeliverySystem.API.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliverySystem.API.Services.Concretes
{
    /// <summary>
    /// Service of the delivery
    /// </summary>
    public class DeliveryService : IDeliveryService
    {
        private readonly DeliveryContext _context;

        /// <summary>
        /// Service injection
        /// </summary>
        /// <param name="context">
        /// Db context
        /// </param>
        public DeliveryService(DeliveryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Generating delivery
        /// If order not eixsts return 
        /// Only partner can start this process
        /// Create delivery action log in created state and partner identifier
        /// </summary>
        /// <returns>
        /// Delivery response object
        /// </returns>
        public DeliveryResponse Create(DeliveryRequest model, Guid accountId, Enums.AccountType accountType)
        {
            try
            {
                if (accountType == Enums.AccountType.user) return null;

                var orderIsExists = _context.Orders.FirstOrDefault(x => x.Id == model.OrderId && x.PartnerId == accountId) != null;
                if (!orderIsExists) return null;

                var delivery = new Delivery()
                {
                    OrderId = model.OrderId,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    State = Enums.DeliveryState.created
                };
                _context.Deliveries.Add(delivery);

                _context.DeliveryActions.Add(new DeliveryAction()
                {
                    DeliveryState = Enums.DeliveryState.created,
                    PartnerId = accountId,
                    DeliveryId = delivery.Id
                });
                _context.SaveChanges();

                return MapToDeliveryResponse(delivery);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Updating the delivery
        /// If delivery is not exists return
        /// If end time of the delivery passed set state to expired
        /// User can change the state to approved if delivery in created state
        /// Partner can change the state to completed if delivery in approved state
        /// All action store in delivery action table with given identifier and account identifier
        /// </summary>
        public DeliveryResponse Update(DeliveryRequest model, Guid accountId, Enums.AccountType accountType)
        {
            try
            {
                var delivery = _context.Deliveries.SingleOrDefault(x => x.Id == model.DeliveryId);
                if (delivery == null) return null;

                if(DateTimeOffset.UtcNow > delivery.EndTime)
                {
                    delivery.State = Enums.DeliveryState.expired;
                    var deliveryAction = accountType == Enums.AccountType.partner ?
                        new DeliveryAction()
                        {
                            DeliveryState = Enums.DeliveryState.expired,
                            PartnerId = accountId,
                            DeliveryId = delivery.Id
                        } :
                        new DeliveryAction()
                        {
                            DeliveryState = Enums.DeliveryState.expired,
                            UserId = accountId,
                            DeliveryId = delivery.Id
                        };
                    _context.DeliveryActions.Add(deliveryAction);
                    _context.SaveChanges();
                    return MapToDeliveryResponse(delivery);
                }

                if (accountType == Enums.AccountType.user &&
                   model.DeliveryState == Enums.DeliveryState.approved &&
                   delivery.State == Enums.DeliveryState.created)
                {
                    delivery.State = Enums.DeliveryState.approved;
                    _context.DeliveryActions.Add(new DeliveryAction()
                    {
                        DeliveryState = Enums.DeliveryState.approved,
                        UserId = accountId,
                        DeliveryId = delivery.Id
                    });
                    _context.SaveChanges();
                    return MapToDeliveryResponse(delivery);
                }

                if (accountType == Enums.AccountType.partner &&
                    model.DeliveryState == Enums.DeliveryState.completed &&
                    delivery.State == Enums.DeliveryState.approved)
                {
                    delivery.State = Enums.DeliveryState.completed;
                    _context.DeliveryActions.Add(new DeliveryAction()
                    {
                        DeliveryState = Enums.DeliveryState.completed,
                        PartnerId = accountId,
                        DeliveryId = delivery.Id
                    });
                    _context.SaveChanges();
                    return MapToDeliveryResponse(delivery);
                }
                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Cancelling the delivery
        /// If delivery is not exists return
        /// User or partner can do this action
        /// This process can be valid if delivery only in approved or created states
        /// Create delivery action log for user or partner and in cancelled state 
        /// </summary>
        public DeliveryResponse Delete(Guid id, Guid accountId, Enums.AccountType accountType)
        {
            try
            {
                var delivery = _context.Deliveries.SingleOrDefault(x => x.Id == id);
                if (delivery == null) return null;

                if (delivery.State == Enums.DeliveryState.approved || delivery.State == Enums.DeliveryState.created)
                {
                    delivery.State = Enums.DeliveryState.cancelled;
                    var deliveryAction = accountType == Enums.AccountType.partner ?
                        new DeliveryAction()
                        {
                            DeliveryState = Enums.DeliveryState.cancelled,
                            PartnerId = accountId,
                            DeliveryId = delivery.Id
                        } :
                        new DeliveryAction()
                        {
                            DeliveryState = Enums.DeliveryState.cancelled,
                            UserId = accountId,
                            DeliveryId = delivery.Id
                        };
                    _context.DeliveryActions.Add(deliveryAction);
                    _context.SaveChanges();
                    return MapToDeliveryResponse(delivery);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

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
        public IEnumerable<DeliveryResponse> GetAll(Guid accountId, Enums.AccountType accountType)
        {
            try
            {
                var deliveries = _context.Deliveries
              .Where(x =>
                  x.Order != null &&
                  x.Order.Partner != null &&
                  x.Order.User != null);

                if (accountType == Enums.AccountType.user)
                {
                    deliveries.Where(x => x.Order.UserId == accountId);
                }
                else
                {
                    deliveries.Where(x => x.Order.PartnerId == accountId);
                }

                return deliveries.AsNoTracking().Select(x => MapToDeliveryResponse(x))
                .ToList();
            }
            catch (Exception)
            {
                return new List<DeliveryResponse>();
            }
        }

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
        public DeliveryResponse GetById(Guid id, Guid accountId, Enums.AccountType accountType)
        {
            try
            {
                var deliveries = _context.Deliveries.Where(x =>
               x.Order != null &&
               x.Order.Partner != null &&
               x.Order.User != null);

                if (accountType == Enums.AccountType.user)
                {
                    deliveries.Where(x => x.Order.UserId == accountId);
                }
                else
                {
                    deliveries.Where(x => x.Order.PartnerId == accountId);
                }
                var delivery = deliveries.FirstOrDefault(x => x.Id == id);

                if (delivery != null)
                {
                    return MapToDeliveryResponse(delivery);
                }
                return new DeliveryResponse();
            }
            catch (Exception)
            {
                return new DeliveryResponse();
            }
        }

        #region Helpers
        /// <summary>
        /// Convert delivery to delivery response
        /// </summary>
        /// <param name="delivery"></param>
        /// <returns></returns>
        private DeliveryResponse MapToDeliveryResponse(Delivery delivery)
        {
            return new DeliveryResponse()
            {
                state = delivery.State.ToString(),
                accessWindow = new AccessWindowResponse()
                {
                    startTime = delivery.State.ToString(),
                    endTime = delivery.EndTime.ToString()
                },
                recipient = new RecipientResponse()
                {
                    name = $"{delivery.Order.User.FirstName} {delivery.Order.User.LastName}",
                    address = delivery.Order.User.Address,
                    email = delivery.Order.User.Email,
                    phoneNumber = delivery.Order.User.PhoneNumber
                },
                order = new OrderResponse()
                {
                    orderNumber = delivery.Order.Id.ToString(),
                    sender = delivery.Order.Partner.Name
                }
            };
        }
        #endregion
    }
}
