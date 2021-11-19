using DeliverySystem.API.Helpers;
using DeliverySystem.API.Helpers.Jwt;
using DeliverySystem.API.Models;
using DeliverySystem.API.Models.RequestModels;
using DeliverySystem.API.Models.ResponseModels;
using DeliverySystem.API.Services.Abstracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DeliverySystem.API.Services.Concretes
{
    /// <summary>
    /// Service of the account
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly AppSettings _appSettings;
        private readonly DeliveryContext _context;

        /// <summary>
        /// Service injection
        /// </summary>
        /// <param name="appSettings">
        /// For jwt token settings
        /// </param>
        /// <param name="context">
        /// Db context
        /// </param>
        public AccountService(IOptions<AppSettings> appSettings, DeliveryContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

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
        public AuthenticateResponse Authenticate(AuthenticateRequest model, Enums.AccountType accountType)
        {
            switch (accountType)
            {
                case Enums.AccountType.partner:
                    var partner = _context.Partners.SingleOrDefault(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password));
                    if (partner == null) return null;
                    return new AuthenticateResponse()
                    {
                        Id = partner.Id.ToString(),
                        Email = partner.Email,
                        Token = generateJwtToken(partner),
                        AccountType = Enums.AccountType.partner
                    };
                case Enums.AccountType.user:
                    var user = _context.Users.SingleOrDefault(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password));
                    if (user == null) return null;
                    return new AuthenticateResponse()
                    {
                        Id = user.Id.ToString(),
                        Email = user.Email,
                        Token = generateJwtToken(user),
                        AccountType = Enums.AccountType.user
                    };
                default:
                    return null;
            }
        }

        /// <summary>
        /// Get account by account identifier
        /// </summary>
        /// <param name="Id">
        /// Identifier of the account
        /// </param>
        /// <returns>
        /// Authenticated account object
        /// </returns>
        public AuthenticateResponse GetById(Guid Id)
        {
            var partner = _context.Partners.SingleOrDefault(x => x.Id == Id);
            if (partner != null)
            {
                return new AuthenticateResponse()
                {
                    Email = partner.Email,
                    Id = partner.Id.ToString(),
                    Token = generateJwtToken(partner),
                    AccountType = Enums.AccountType.partner
                };
            }
            var user = _context.Users.SingleOrDefault(x => x.Id == Id);
            if (user != null)
            {
                return new AuthenticateResponse()
                {
                    Email = user.Email,
                    Id = user.Id.ToString(),
                    Token = generateJwtToken(user),
                    AccountType = Enums.AccountType.user
                };
            }
            return null;
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
        public AuthenticateResponse RegisterPartner(PartnerRequest model)
        {
            var partner = _context.Partners.SingleOrDefault(x =>
                x.Email.Equals(model.Email) ||
                x.Name.Equals(model.Name));

            if (partner != null) return null;

            partner = new Partner()
            {
                Description = model.Description,
                Email = model.Email,
                Name = model.Name,
                Password = model.Password
            };

            _context.Partners.Add(partner);
            _context.SaveChanges();
            return new AuthenticateResponse()
            {
                Id = partner.Id.ToString(),
                Email = partner.Email,
                Token = generateJwtToken(partner),
                AccountType = Enums.AccountType.partner
            };
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
        public AuthenticateResponse RegisterUser(UserRequest model)
        {
            var user = _context.Users.SingleOrDefault(x =>
                x.Email.Equals(model.Email));

            if (user != null) return null;

            user = new User()
            {
                Address = model.Address,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return new AuthenticateResponse()
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Token = generateJwtToken(user),
                AccountType = Enums.AccountType.user
            };
        }

        #region Helpers
        /// <summary>
        /// Generate token that is valid for 7 days
        /// </summary>
        /// <param name="account">
        /// Related account (user or partner)
        /// </param>
        /// <returns></returns>
        private string generateJwtToken(Base account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
