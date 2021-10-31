namespace AvtoCarDriveBot.Services.AdminIdsServices
{
    /// <summary>
    /// Provides processing of the list of admins
    /// </summary>
    public interface IAdminIdsService
    {
        /// <summary>
        /// Ads the new admin
        /// </summary>
        /// <param name="id">The admin's id</param>
        /// <returns>True if admin was added successfully, otherwise - false</returns>
        bool SetAdminId(long id);

        /// <summary>
        /// Removes the admin
        /// </summary>
        /// <param name="id">The admin's id</param>
        /// <returns>True if admin was removed successfully, otherwise - false</returns>
        bool RemoveAdminId(long id);
    }
}