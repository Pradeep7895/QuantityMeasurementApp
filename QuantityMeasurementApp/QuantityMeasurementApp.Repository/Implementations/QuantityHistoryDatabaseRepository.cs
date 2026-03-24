using Microsoft.Data.SqlClient;
using System.Data;
using QuantityMeasurementApp.Repository.Entities;
using QuantityMeasurementApp.Repository.Interfaces; 

namespace QuantityMeasurementApp.Repository.Implementations
{
    public class QuantityHistoryDatabaseRepository : IQuantityHistoryRepository
    {
        private readonly string _connectionString;

        public QuantityHistoryDatabaseRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void AddRecord(QuantityHistoryRecord record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand command = new SqlCommand("SPInsert_Quantity_History", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Category", record.Category ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@OperationType", record.OperationType ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@FirstValue", record.FirstValue ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@FirstUnit", record.FirstUnit ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@SecondValue", record.SecondValue ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@SecondUnit", record.SecondUnit ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@TargetUnit", record.TargetUnit ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ResultValue", record.ResultValue ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ResultUnit", record.ResultUnit ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ErrorMessage", record.ErrorMessage ?? (object)DBNull.Value);

            SqlParameter idParam = new SqlParameter("@Id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(idParam);

            command.ExecuteNonQuery();
            
            int newId = Convert.ToInt32(idParam.Value);
            record.Id = newId;
            record.CreatedAt = DateTime.Now;
        }

        public List<QuantityHistoryRecord> GetAllRecords()
        {
            var records = new List<QuantityHistoryRecord>();

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand command = new SqlCommand("SPGet_All_Quantity_History", connection);
            command.CommandType = CommandType.StoredProcedure;

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var record = MapToRecord(reader);
                if (record != null)
                    records.Add(record);
            }

            return records;
        }

        public QuantityHistoryRecord? GetRecordById(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand command = new SqlCommand("SPGet_Quantity_History_By_Id", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapToRecord(reader);
            }

            return null;
        }

        public List<QuantityHistoryRecord> GetRecordsByCategory(string category)
        {
            var records = new List<QuantityHistoryRecord>();

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand command = new SqlCommand("SPGet_Quantity_History_By_Category", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Category", category);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var record = MapToRecord(reader);
                if (record != null)
                    records.Add(record);
            }

            return records;
        }

        public List<QuantityHistoryRecord> GetRecordsByOperationType(string operationType)
        {
            var records = new List<QuantityHistoryRecord>();

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand command = new SqlCommand("SPGet_Quantity_History_By_Operation_Type", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@OperationType", operationType);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var record = MapToRecord(reader);
                if (record != null)
                    records.Add(record);
            }

            return records;
        }

        public bool DeleteRecord(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand command = new SqlCommand("SPDelete_Quantity_History_By_Id", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public int DeleteAllRecords()
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand command = new SqlCommand("SPDelete_All_Quantity_History", connection);
            command.CommandType = CommandType.StoredProcedure;

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }

        public int GetRecordCount()
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand command = new SqlCommand("SPGet_Quantity_History_Count", connection);
            command.CommandType = CommandType.StoredProcedure;

            object? result = command.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public void ClearCache()
        {
            // Database-only repository doesn't have cache
            // This method exists only to satisfy interface
        }

        public void RefreshCache()
        {
            // Database-only repository doesn't have cache
            // This method exists only to satisfy interface
        }

        public void Close()
        {
            // Connection pooling handles closing automatically
            // This method exists only to satisfy interface
        }

        private QuantityHistoryRecord? MapToRecord(SqlDataReader reader)
        {
            if (reader == null) return null;

            return new QuantityHistoryRecord
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Category = reader.IsDBNull(reader.GetOrdinal("Category")) ? null : reader.GetString(reader.GetOrdinal("Category")),
                OperationType = reader.IsDBNull(reader.GetOrdinal("OperationType")) ? null : reader.GetString(reader.GetOrdinal("OperationType")),
                FirstValue = reader.IsDBNull(reader.GetOrdinal("FirstValue")) ? null : (double?)reader.GetDecimal(reader.GetOrdinal("FirstValue")),
                FirstUnit = reader.IsDBNull(reader.GetOrdinal("FirstUnit")) ? null : reader.GetString(reader.GetOrdinal("FirstUnit")),
                SecondValue = reader.IsDBNull(reader.GetOrdinal("SecondValue")) ? null : (double?)reader.GetDecimal(reader.GetOrdinal("SecondValue")),
                SecondUnit = reader.IsDBNull(reader.GetOrdinal("SecondUnit")) ? null : reader.GetString(reader.GetOrdinal("SecondUnit")),
                TargetUnit = reader.IsDBNull(reader.GetOrdinal("TargetUnit")) ? null : reader.GetString(reader.GetOrdinal("TargetUnit")),
                ResultValue = reader.IsDBNull(reader.GetOrdinal("ResultValue")) ? null : (double?)reader.GetDecimal(reader.GetOrdinal("ResultValue")),
                ResultUnit = reader.IsDBNull(reader.GetOrdinal("ResultUnit")) ? null : reader.GetString(reader.GetOrdinal("ResultUnit")),
                ErrorMessage = reader.IsDBNull(reader.GetOrdinal("ErrorMessage")) ? null : reader.GetString(reader.GetOrdinal("ErrorMessage")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
            };
        }
    }
}