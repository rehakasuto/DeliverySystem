namespace DeliverySystem.API.Helpers
{
    public class Enums
    {
        /// <summary>
        /// Status of delivery
        /// </summary>
        public enum DeliveryState
        {
            /// <summary>
            /// Created by partner
            /// </summary>
            created, 
            /// <summary>
            /// Approved by user
            /// </summary>
            approved, 
            /// <summary>
            /// Completed by user or partner
            /// </summary>
            completed, 
            /// <summary>
            /// Cancelled by user or partner
            /// </summary>
            cancelled, 
            /// <summary>
            /// Expired by date
            /// </summary>
            expired
        }

        /// <summary>
        /// Type of authenticated accounts
        /// </summary>
        public enum AccountType
        {
            /// <summary>
            /// Customer
            /// </summary>
            user, 
            /// <summary>
            /// Seller
            /// </summary>
            partner
        }
    }
}
