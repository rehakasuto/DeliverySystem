using System.ComponentModel.DataAnnotations;

namespace DeliverySystem.API.Models
{
    /// <summary>
    /// The product of the stores
    /// </summary>
    public class Product : Base
    {
        /// <summary>
        /// Name of the product
        /// </summary>
        [Required,MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// Unit price of the product
        /// </summary>
        public double UnitPrice { get; set; }
        /// <summary>
        /// Extra detail of the product
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
