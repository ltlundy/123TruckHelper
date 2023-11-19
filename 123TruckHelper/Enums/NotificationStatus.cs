namespace _123TruckHelper.Enums
{
    public enum NotificationStatus
    {
        /// <summary>
        /// No response to notification yet
        /// </summary>
        Sent = 0,

        /// <summary>
        /// Load accepted by carrier
        /// </summary>
        Accepted = 1,

        /// <summary>
        /// Load declined by carrier
        /// </summary>
        Declined = 2,

        /// <summary>
        /// Notification expired with no response
        /// </summary>
        Expired = 3,
    }
}
