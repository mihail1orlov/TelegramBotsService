using System;

namespace AvtoCarDriveBot.Services.AdminIdsServices
{
    /// <summary>
    /// Provides the list of admins
    /// </summary>
    public interface IAdminIdsProvider
    {
        /// <summary>
        /// Event that is invoked when the list of administrators was changed
        /// </summary>
        event Action AdminListUpdatedAction;

        /// <summary>
        /// The admin's identificators
        /// </summary>
        long[] AdminIds { get; }
    }
}