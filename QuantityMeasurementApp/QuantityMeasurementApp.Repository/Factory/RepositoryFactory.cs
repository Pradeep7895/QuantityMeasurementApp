using QuantityMeasurementApp.Repository.Implementations;
using QuantityMeasurementApp.Repository.Interfaces;

namespace QuantityMeasurementApp.Repository.Factory
{
    public enum RepositoryType
    {
        Database,   // SQL Database only
        RedisCache, // Redis + SQL Database
        Hybrid      // Redis + SQL + In-Memory
    }

    public static class RepositoryFactory
    {
        /// <summary>
        /// Create repository based on type
        /// </summary>
        public static IQuantityHistoryRepository Create(
            RepositoryType type,
            string sqlConnectionString,
            string redisConnectionString)
        {
            return type switch
            {
                RepositoryType.Database => new QuantityHistoryDatabaseRepository(sqlConnectionString),
                RepositoryType.RedisCache => new QuantityHistoryRepository(sqlConnectionString, redisConnectionString),
                RepositoryType.Hybrid => new QuantityHistoryRepository(sqlConnectionString, redisConnectionString),
                _ => new QuantityHistoryRepository(sqlConnectionString, redisConnectionString)
            };
        }

        /// <summary>
        /// Create Redis + SQL repository 
        /// </summary>
        public static IQuantityHistoryRepository CreateWithRedis(string sqlConnectionString, string redisConnectionString)
        {
            return new QuantityHistoryRepository(sqlConnectionString, redisConnectionString);
        }

        /// <summary>
        /// Create SQL only repository 
        /// </summary>
        public static IQuantityHistoryRepository CreateWithoutCache(string sqlConnectionString)
        {
            return new QuantityHistoryDatabaseRepository(sqlConnectionString);
        }
    }
}