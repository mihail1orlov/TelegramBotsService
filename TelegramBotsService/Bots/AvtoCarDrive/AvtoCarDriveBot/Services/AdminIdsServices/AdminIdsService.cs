using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;

namespace AvtoCarDriveBot.Services.AdminIdsServices
{
    /// <summary>
    /// Provides processing of the list of admins
    /// </summary>
    [SuppressMessage("ReSharper", "ConvertToUsingDeclaration")]
    public class AdminIdsService : IAdminIdsProvider, IAdminIdsService
    {
        // private fields
        private readonly List<long> _adminIds;
        private readonly string _dataPath;

        /// <summary>
        /// Creates instance of <see cref="AdminIdsService"/>
        /// </summary>
        public AdminIdsService(string dataPath = default)
        {
            _dataPath = dataPath ?? Environment.CurrentDirectory + "/Data";

            _adminIds = GetAdminIds();

            AdminListUpdatedAction += UpdateAdminIdsFile;
        }

        /// <summary>
        /// Event that is invoked when the list of administrators was changed
        /// </summary>
        public event Action AdminListUpdatedAction;

        /// <summary>
        /// The admin's identificators
        /// </summary>
        public long[] AdminIds => _adminIds.ToArray();

        /// <summary>
        /// Ads the new admin
        /// </summary>
        /// <param name="id">The admin's id</param>
        /// <returns>True if admin was added successfully, otherwise - false</returns>
        public bool SetAdminId(long id)
        {
            if (!_adminIds.Contains(id))
            {
                _adminIds.Add(id);
                AdminListUpdatedAction?.Invoke();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes the admin
        /// </summary>
        /// <param name="id">The admin's id</param>
        /// <returns>True if admin was removed successfully, otherwise - false</returns>
        public bool RemoveAdminId(long id)
        {
            if (_adminIds.Contains(id))
            {
                _adminIds.Remove(id);
                AdminListUpdatedAction?.Invoke();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets admin's ids from the file
        /// </summary>
        /// <returns>The admin's ids</returns>
        private List<long> GetAdminIds()
        {
            var pathApp = _dataPath + "/AdminIds.txt";
            var adminIds = new List<long>();
            Directory.CreateDirectory(_dataPath);
            using (var fileStream = new FileStream(pathApp, FileMode.OpenOrCreate, FileAccess.Read))
            {
                var buffer = new byte[100];
                var bufferSize = fileStream.Read(buffer, 0, 100);
                if (bufferSize == 0)
                {
                    return adminIds;
                }

                using (var memoryStream = new MemoryStream())
                {
                    memoryStream.Write(buffer, 0, bufferSize);
                    memoryStream.Position = 0;
                    using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress, true))
                    using (var binaryReader = new BinaryReader(deflateStream))
                    {
                        var adminIdsCount = binaryReader.ReadInt32();

                        for (var i = 0; i < adminIdsCount; i++)
                        {
                            adminIds.Add(binaryReader.ReadInt64());
                        }
                    }
                }
            }

            return adminIds;
        }

        /// <summary>
        /// Updates file with admin's ids
        /// </summary>
        private void UpdateAdminIdsFile()
        {
            var pathApp = _dataPath + "/AdminIds.txt";
            Directory.CreateDirectory(_dataPath);

            using (var fileStream = new FileStream(pathApp, FileMode.OpenOrCreate, FileAccess.Write))
            using (var memoryStream = new MemoryStream())
            {
                using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
                using (var binaryWriter = new BinaryWriter(deflateStream))
                {
                    binaryWriter.Write(_adminIds.Count);

                    foreach (var adminId in _adminIds)
                    {
                        binaryWriter.Write(adminId);
                    }
                }

                var buffer = memoryStream.GetBuffer();
                fileStream.Write(buffer, 0, buffer.Length);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = default;

            foreach (var adminId in _adminIds)
            {
                result += adminId + "\n";
            }

            return result;
        }
    }
}