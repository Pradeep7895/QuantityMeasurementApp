using QuantityMeasurementApp.Repository.Entities;
using QuantityMeasurementApp.Repository.Interfaces;

namespace QuantityMeasurementApp.Repository.Implementations
{
    public class QuantityHistoryRepository : IQuantityHistoryRepository
    {
        private readonly QuantityHistoryDatabaseRepository _databaseRepo;
        private readonly QuantityHistoryRedisCache _cache;
        private readonly bool _cacheEnabled = true;

        public QuantityHistoryRepository(string sqlConnectionString, string redisConnectionString)
        {
            _databaseRepo = new QuantityHistoryDatabaseRepository(sqlConnectionString);
            _cache = new QuantityHistoryRedisCache(redisConnectionString);
        }

        public void AddRecord(QuantityHistoryRecord record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            // Write to SQL Database
            _databaseRepo.AddRecord(record);

            // Update Redis cache
            if (_cacheEnabled && record.Id > 0)
            {
                _cache.SetRecord(record);
            }
        }

        public List<QuantityHistoryRecord> GetAllRecords()
        {
            if (!_cacheEnabled)
                return _databaseRepo.GetAllRecords();

            // Try Redis first
            var cachedRecords = _cache.GetAllRecords();
            if (cachedRecords != null && cachedRecords.Any())
                return cachedRecords;

            // Cache miss - get from SQL
            var dbRecords = _databaseRepo.GetAllRecords();

            // Update Redis
            if (_cacheEnabled && dbRecords != null)
            {
                foreach (var record in dbRecords)
                {
                    _cache.SetRecord(record);
                }
            }

            return dbRecords ?? new List<QuantityHistoryRecord>();
        }

        // FIXED: Match the interface exactly
        public QuantityHistoryRecord? GetRecordById(int id)
        {
            if (!_cacheEnabled)
                return _databaseRepo.GetRecordById(id);

            // Try Redis first
            var cached = _cache.GetRecord(id);
            if (cached != null)
                return cached;

            // Cache miss - get from SQL
            var record = _databaseRepo.GetRecordById(id);

            // Update Redis
            if (record != null && _cacheEnabled)
            {
                _cache.SetRecord(record);
            }

            return record;
        }

        public List<QuantityHistoryRecord> GetRecordsByCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
                return new List<QuantityHistoryRecord>();

            if (!_cacheEnabled)
                return _databaseRepo.GetRecordsByCategory(category);

            var cached = _cache.GetRecordsByCategory(category);
            if (cached != null && cached.Any())
                return cached;

            var dbRecords = _databaseRepo.GetRecordsByCategory(category);

            if (_cacheEnabled && dbRecords != null)
            {
                foreach (var record in dbRecords)
                {
                    _cache.SetRecord(record);
                }
            }

            return dbRecords ?? new List<QuantityHistoryRecord>();
        }

        public List<QuantityHistoryRecord> GetRecordsByOperationType(string operationType)
        {
            if (string.IsNullOrEmpty(operationType))
                return new List<QuantityHistoryRecord>();

            if (!_cacheEnabled)
                return _databaseRepo.GetRecordsByOperationType(operationType);

            var cached = _cache.GetRecordsByOperationType(operationType);
            if (cached != null && cached.Any())
                return cached;

            var dbRecords = _databaseRepo.GetRecordsByOperationType(operationType);

            if (_cacheEnabled && dbRecords != null)
            {
                foreach (var record in dbRecords)
                {
                    _cache.SetRecord(record);
                }
            }

            return dbRecords ?? new List<QuantityHistoryRecord>();
        }

        public bool DeleteRecord(int id)
        {
            bool deleted = _databaseRepo.DeleteRecord(id);

            // Always refresh cache for this ID
            if (_cacheEnabled)
            {
                // Remove from cache regardless
                var record = new QuantityHistoryRecord { Id = id };
                _cache.RemoveRecord(record);

                // Also refresh the all-records cache
                RefreshAllRecordsCache();
            }

            return deleted;
        }

        private void RefreshAllRecordsCache()
        {
            var allRecords = _databaseRepo.GetAllRecords();
            _cache.ClearAll();
            foreach (var record in allRecords)
            {
                _cache.SetRecord(record);
            }
        }

        public int DeleteAllRecords()
        {
            int count = _databaseRepo.DeleteAllRecords();

            if (_cacheEnabled)
            {
                _cache.ClearAll();
            }

            return count;
        }

        public int GetRecordCount()
        {
            return _databaseRepo.GetRecordCount();
        }

        public void ClearCache()
        {
            if (_cacheEnabled)
            {
                _cache.ClearAll();
            }
        }

        public void RefreshCache()
        {
            if (!_cacheEnabled)
                return;

            ClearCache();
            var allRecords = _databaseRepo.GetAllRecords();
            if (allRecords != null)
            {
                foreach (var record in allRecords)
                {
                    _cache.SetRecord(record);
                }
            }
        }

        public void Close()
        {
            _cache?.Dispose();
        }
    }
}