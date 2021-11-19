using System.ComponentModel.DataAnnotations;

namespace DeliverySystem.API.Models
{
    /// <summary>
    /// Logged in account info
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// The email of account
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// The password of account
        /// </summary>
        string Password { get; set; }
    }
}
