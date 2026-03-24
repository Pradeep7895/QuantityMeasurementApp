using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Repository.Entities;
using QuantityMeasurementApp.Repository.Interfaces;
using QuantityMeasurementApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuantityMeasurementApp.Service.Services
{
    public class QuantityService : IQuantityService
    {
        private readonly IConversionService _conversionService;
        private readonly IArithmeticService _arithmeticService;
        private readonly IEqualityService _equalityService;
        private readonly IValidationService _validationService;
        private readonly IQuantityHistoryRepository _repository;

        public QuantityService(
            IConversionService conversionService,
            IArithmeticService arithmeticService,
            IEqualityService equalityService,
            IValidationService validationService,
            IQuantityHistoryRepository repository)
        {
            _conversionService = conversionService;
            _arithmeticService = arithmeticService;
            _equalityService = equalityService;
            _validationService = validationService;
            _repository = repository;
        }

        /// <summary>
        /// Converts a quantity from its current unit to a target unit
        /// </summary>
        public QuantityDTO Convert(QuantityDTO source, string targetUnit)
        {
            // Validate input
            if (!_validationService.IsValid(source))
                throw new ArgumentException("Invalid source quantity");

            // Perform conversion
            var result = _conversionService.Convert(source, targetUnit);
            
            // Save to repository (SQL + Redis)
            _repository.AddRecord(new QuantityHistoryRecord
            {
                Category = source.MeasurementType,
                OperationType = "Convert",
                FirstValue = source.Value,
                FirstUnit = source.Unit,
                TargetUnit = targetUnit,
                ResultValue = result.Value,
                ResultUnit = result.Unit,
                CreatedAt = DateTime.Now
            });
            
            return result;
        }

        /// <summary>
        /// Compares two quantities for equality
        /// </summary>
        public bool Compare(QuantityDTO q1, QuantityDTO q2)
        {
            // Validate inputs
            if (!_validationService.IsValid(q1) || !_validationService.IsValid(q2))
                throw new ArgumentException("Invalid quantity input");

            // Perform comparison
            bool result = _equalityService.AreEqual(q1, q2);
            
            // Save to repository (SQL + Redis)
            _repository.AddRecord(new QuantityHistoryRecord
            {
                Category = q1.MeasurementType,
                OperationType = "Compare",
                FirstValue = q1.Value,
                FirstUnit = q1.Unit,
                SecondValue = q2.Value,
                SecondUnit = q2.Unit,
                ResultValue = result ? 1 : 0,
                ResultUnit = "Boolean",
                CreatedAt = DateTime.Now
            });
            
            return result;
        }

        /// <summary>
        /// Adds two quantities and returns result in first quantity's unit
        /// </summary>
        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2)
        {
            // Validate arithmetic operation
            if (!_validationService.CanPerformArithmetic(q1, q2))
                throw new InvalidOperationException("Cannot add these quantities");

            // Perform addition
            var result = _arithmeticService.Add(q1, q2);
            
            // Save to repository (SQL + Redis)
            _repository.AddRecord(new QuantityHistoryRecord
            {
                Category = q1.MeasurementType,
                OperationType = "Add",
                FirstValue = q1.Value,
                FirstUnit = q1.Unit,
                SecondValue = q2.Value,
                SecondUnit = q2.Unit,
                ResultValue = result.Value,
                ResultUnit = result.Unit,
                CreatedAt = DateTime.Now
            });
            
            return result;
        }

        /// <summary>
        /// Adds two quantities and returns result in specified target unit
        /// </summary>
        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2, string targetUnit)
        {
            // Validate arithmetic operation
            if (!_validationService.CanPerformArithmetic(q1, q2))
                throw new InvalidOperationException("Cannot add these quantities");

            // Perform addition with target unit
            var result = _arithmeticService.Add(q1, q2, targetUnit);
            
            // Save to repository (SQL + Redis)
            _repository.AddRecord(new QuantityHistoryRecord
            {
                Category = q1.MeasurementType,
                OperationType = "Add",
                FirstValue = q1.Value,
                FirstUnit = q1.Unit,
                SecondValue = q2.Value,
                SecondUnit = q2.Unit,
                TargetUnit = targetUnit,
                ResultValue = result.Value,
                ResultUnit = result.Unit,
                CreatedAt = DateTime.Now
            });
            
            return result;
        }

        /// <summary>
        /// Subtracts second quantity from first and returns result in first quantity's unit
        /// </summary>
        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2)
        {
            // Validate arithmetic operation
            if (!_validationService.CanPerformArithmetic(q1, q2))
                throw new InvalidOperationException("Cannot subtract these quantities");

            // Perform subtraction
            var result = _arithmeticService.Subtract(q1, q2);
            
            // Save to repository (SQL + Redis)
            _repository.AddRecord(new QuantityHistoryRecord
            {
                Category = q1.MeasurementType,
                OperationType = "Subtract",
                FirstValue = q1.Value,
                FirstUnit = q1.Unit,
                SecondValue = q2.Value,
                SecondUnit = q2.Unit,
                ResultValue = result.Value,
                ResultUnit = result.Unit,
                CreatedAt = DateTime.Now
            });
            
            return result;
        }

        /// <summary>
        /// Subtracts second quantity from first and returns result in specified target unit
        /// </summary>
        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2, string targetUnit)
        {
            // Validate arithmetic operation
            if (!_validationService.CanPerformArithmetic(q1, q2))
                throw new InvalidOperationException("Cannot subtract these quantities");

            // Perform subtraction with target unit
            var result = _arithmeticService.Subtract(q1, q2, targetUnit);
            
            // Save to repository (SQL + Redis)
            _repository.AddRecord(new QuantityHistoryRecord
            {
                Category = q1.MeasurementType,
                OperationType = "Subtract",
                FirstValue = q1.Value,
                FirstUnit = q1.Unit,
                SecondValue = q2.Value,
                SecondUnit = q2.Unit,
                TargetUnit = targetUnit,
                ResultValue = result.Value,
                ResultUnit = result.Unit,
                CreatedAt = DateTime.Now
            });
            
            return result;
        }

        /// <summary>
        /// Divides first quantity by second quantity
        /// </summary>
        public double Divide(QuantityDTO q1, QuantityDTO q2)
        {
            // Validate arithmetic operation
            if (!_validationService.CanPerformArithmetic(q1, q2))
                throw new InvalidOperationException("Cannot divide these quantities");

            // Perform division
            double result = _arithmeticService.Divide(q1, q2);
            
            // Save to repository (SQL + Redis)
            _repository.AddRecord(new QuantityHistoryRecord
            {
                Category = q1.MeasurementType,
                OperationType = "Divide",
                FirstValue = q1.Value,
                FirstUnit = q1.Unit,
                SecondValue = q2.Value,
                SecondUnit = q2.Unit,
                ResultValue = result,
                ResultUnit = "Ratio",
                CreatedAt = DateTime.Now
            });
            
            return result;
        }

        /// <summary>
        /// Validates a quantity DTO
        /// </summary>
        public bool Validate(QuantityDTO quantity)
        {
            return _validationService.IsValid(quantity);
        }

        //  HISTORY METHODS

        /// <summary>
        /// Gets all history records from repository
        /// </summary>
        public List<QuantityHistoryRecord> GetHistory()
        {
            return _repository.GetAllRecords();
        }

        /// <summary>
        /// Gets history records by category
        /// </summary>
        public List<QuantityHistoryRecord> GetHistoryByCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
                return new List<QuantityHistoryRecord>();
                
            return _repository.GetRecordsByCategory(category);
        }

        /// <summary>
        /// Gets history records by operation type
        /// </summary>
        public List<QuantityHistoryRecord> GetHistoryByOperationType(string operationType)
        {
            if (string.IsNullOrEmpty(operationType))
                return new List<QuantityHistoryRecord>();
                
            return _repository.GetRecordsByOperationType(operationType);
        }

        /// <summary>
        /// Gets a specific record by ID
        /// </summary>
        public QuantityHistoryRecord? GetHistoryRecordById(int id)
        {
            if (id <= 0)
                return null;
                
            return _repository.GetRecordById(id);
        }

        /// <summary>
        /// Deletes a specific record by ID
        /// </summary>
        public bool DeleteHistoryRecord(int id)
        {
            if (id <= 0)
                return false;
                
            return _repository.DeleteRecord(id);
        }

        /// <summary>
        /// Deletes all history records
        /// </summary>
        public int DeleteAllHistoryRecords()
        {
            return _repository.DeleteAllRecords();
        }

        /// <summary>
        /// Gets total count of records
        /// </summary>
        public int GetHistoryCount()
        {
            return _repository.GetRecordCount();
        }

        /// <summary>
        /// Clears all history from repository
        /// </summary>
        public void ClearHistory()
        {
            _repository.DeleteAllRecords();
        }

        //  CACHE MANAGEMENT 

        /// <summary>
        /// Clears Redis cache
        /// </summary>
        public void ClearCache()
        {
            _repository.ClearCache();
        }

        /// <summary>
        /// Refreshes Redis cache from SQL database
        /// </summary>
        public void RefreshCache()
        {
            _repository.RefreshCache();
        }
        /// <summary>
        /// Displays repository statistics
        /// </summary>
        public void DisplayStatistics()
        {
            int totalRecords = GetHistoryCount();
            var recentRecords = GetHistory();
            
            Console.WriteLine("\n=== Repository Statistics ===");
            Console.WriteLine($"Total Records: {totalRecords}");
            Console.WriteLine($"Cache Status: Redis cache is active");
            
            if (recentRecords.Any())
            {
                Console.WriteLine("\nLast 5 Operations:");
                foreach (var record in recentRecords.Take(5))
                {
                    Console.WriteLine($"  [{record.Id}] {record.OperationType}: {record.FirstValue} {record.FirstUnit} => {record.ResultValue} {record.ResultUnit} ({record.CreatedAt:HH:mm:ss})");
                }
            }
            else
            {
                Console.WriteLine("\nNo records found.");
            }
        }

        /// <summary>
        /// Gets summary statistics by operation type
        /// </summary>
        public Dictionary<string, int> GetOperationStatistics()
        {
            var records = GetHistory();
            return records
                .GroupBy(r => r.OperationType)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        /// <summary>
        /// Gets summary statistics by category
        /// </summary>
        public Dictionary<string, int> GetCategoryStatistics()
        {
            var records = GetHistory();
            return records
                .GroupBy(r => r.Category)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}