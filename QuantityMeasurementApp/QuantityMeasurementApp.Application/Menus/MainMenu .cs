using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Model.Entities;

namespace QuantityMeasurementApp.Application.Menus
{
    public class MainMenu
    {
        private readonly IQuantityService _service;

        public MainMenu(IQuantityService service)
        {
            _service = service;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n=== QUANTITY MEASUREMENT SYSTEM ===");
                Console.WriteLine("1. Length Measurements");
                Console.WriteLine("2. Weight Measurements");
                Console.WriteLine("3. Volume Measurements");
                Console.WriteLine("4. Temperature Measurements");
                Console.WriteLine("5. View History");
                Console.WriteLine("6. Exit");
                Console.Write("Select option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ShowOperations("Length", new[] { "FEET", "INCH", "YARD", "CENTIMETERS" }); break;
                    case "2": ShowOperations("Weight", new[] { "MILLIGRAM", "GRAM", "KILOGRAM", "POUND", "TONNE" }); break;
                    case "3": ShowOperations("Volume", new[] { "LITRE", "MILLILITRE", "GALLON" }); break;
                    case "4": ShowOperations("Temperature", new[] { "CELSIUS", "FAHRENHEIT", "KELVIN" }); break;
                    case "5": ShowHistory(); break;
                    case "6": Console.WriteLine("\nThank you! Goodbye!");
                                Environment.Exit(0); break;
                    default: Console.WriteLine("Invalid option"); break;
                }

            }
        }

        private void ShowOperations(string type, string[] units)
        {
            var operations = new OperationsMenu(_service, type, units);
            operations.Run();
        }

        private void ShowHistory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n=== HISTORY MENU ===");
                Console.WriteLine("1. View All Records");
                Console.WriteLine("2. View Record by ID");
                Console.WriteLine("3. View Records by Category");
                Console.WriteLine("4. View Records by Operation Type");
                Console.WriteLine("5. Delete Record by ID");
                Console.WriteLine("6. Delete All Records");
                Console.WriteLine("7. Back to Main Menu");
                Console.Write("Select option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": DisplayAllRecords(); break;
                    case "2": DisplayRecordById(); break;
                    case "3": DisplayRecordsByCategory(); break;
                    case "4": DisplayRecordsByOperation(); break;
                    case "5": DeleteRecordById(); break;
                    case "6": DeleteAllRecords(); break;
                    case "7": return;
                    default: Console.WriteLine("Invalid option"); break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private void DisplayAllRecords()
        {
            var records = _service.GetHistory();
            if (records.Count == 0)
            {
                Console.WriteLine("\nNo records found.");
                return;
            }

            Console.WriteLine("\n=== ALL RECORDS ===");
            foreach (var record in records)
            {
                Console.WriteLine($"ID: {record.Id} | {record.CreatedAt:yyyy-MM-dd HH:mm:ss} | {record.OperationType} | {record.FirstValue} {record.FirstUnit} => {record.ResultValue} {record.ResultUnit}");
            }
            Console.WriteLine($"\nTotal: {records.Count} records");
        }

        private void DisplayRecordById()
        {
            Console.Write("\nEnter Record ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var record = _service.GetHistoryRecordById(id);
                if (record != null)
                {
                    Console.WriteLine($"\nID: {record.Id}");
                    Console.WriteLine($"Category: {record.Category}");
                    Console.WriteLine($"Operation: {record.OperationType}");
                    Console.WriteLine($"First: {record.FirstValue} {record.FirstUnit}");
                    Console.WriteLine($"Second: {record.SecondValue} {record.SecondUnit}");
                    Console.WriteLine($"Target Unit: {record.TargetUnit}");
                    Console.WriteLine($"Result: {record.ResultValue} {record.ResultUnit}");
                    Console.WriteLine($"Date: {record.CreatedAt}");
                    Console.WriteLine($"Error: {record.ErrorMessage ?? "None"}");
                }
                else
                {
                    Console.WriteLine($"\nRecord with ID {id} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        private void DisplayRecordsByCategory()
        {
            Console.Write("\nEnter Category (Length/Weight/Volume/Temperature): ");
            string category = Console.ReadLine();
            var records = _service.GetHistoryByCategory(category);

            if (records.Count == 0)
            {
                Console.WriteLine($"\nNo records found for category: {category}");
                return;
            }

            Console.WriteLine($"\n=== RECORDS FOR {category.ToUpper()} ===");
            foreach (var record in records)
            {
                Console.WriteLine($"ID: {record.Id} | {record.OperationType} | {record.FirstValue} {record.FirstUnit} => {record.ResultValue} {record.ResultUnit}");
            }
            Console.WriteLine($"\nTotal: {records.Count} records");
        }

        private void DisplayRecordsByOperation()
        {
            Console.Write("\nEnter Operation Type (Convert/Add/Subtract/Compare/Divide): ");
            string operation = Console.ReadLine();
            var records = _service.GetHistoryByOperationType(operation);

            if (records.Count == 0)
            {
                Console.WriteLine($"\nNo records found for operation: {operation}");
                return;
            }

            Console.WriteLine($"\n=== RECORDS FOR {operation.ToUpper()} ===");
            foreach (var record in records)
            {
                Console.WriteLine($"ID: {record.Id} | {record.Category} | {record.FirstValue} {record.FirstUnit} => {record.ResultValue} {record.ResultUnit}");
            }
            Console.WriteLine($"\nTotal: {records.Count} records");
        }

        private void DeleteRecordById()
        {
            Console.Write("\nEnter Record ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var record = _service.GetHistoryRecordById(id);
                if (record != null)
                {
                    Console.Write($"Delete record {id}? (y/n): ");
                    if (Console.ReadLine()?.ToLower() == "y")
                    {
                        bool deleted = _service.DeleteHistoryRecord(id);
                        Console.WriteLine(deleted ? "Record deleted successfully!" : "Delete failed.");
                    }
                }
                else
                {
                    Console.WriteLine($"Record with ID {id} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        private void DeleteAllRecords()
        {
            int count = _service.GetHistoryCount();
            if (count == 0)
            {
                Console.WriteLine("\nNo records to delete.");
                return;
            }

            Console.Write($"\nDelete ALL {count} records? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                int deleted = _service.DeleteAllHistoryRecords();
                Console.WriteLine($"{deleted} records deleted successfully!");
            }
        }
    }
}