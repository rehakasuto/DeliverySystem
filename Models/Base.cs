using System;
using System.ComponentModel.DataAnnotations;

namespace DeliverySystem.API.Models
{
    /// <summary>
    /// Store common properties of an object
    /// </summary>
    public class Base
    {
        /// <summary>
        /// Default value while creating a new object
        /// </summary>
        public Base()
        {
            CreatedDate = DateTimeOffset.UtcNow;
            ModifiedDate = DateTimeOffset.UtcNow;
        }

        /// <summary>
        /// Identifier of an object
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Date of creation
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }
        /// <summary>
        /// Date of update
        /// </summary>
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
