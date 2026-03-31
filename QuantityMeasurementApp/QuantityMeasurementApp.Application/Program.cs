// using QuantityMeasurementApp.Application.DependencyInjection;
// using QuantityMeasurementApp.Application.Menus;
// using QuantityMeasurementApp.Service.Interfaces;
// using QuantityMeasurementApp.Repository.Factory;
// using QuantityMeasurementApp.Repository.Interfaces;
// using QuantityMeasurementApp.Repository.Implementations;

// namespace QuantityMeasurementApp.Application
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             try
//             {
//                 // Connection strings
//                 string sqlConnectionString = "Server=localhost\\SQLEXPRESS;Database=QuantityMeasurementDB;Trusted_Connection=True;TrustServerCertificate=True;";
//                 string memuraiConnectionString = "localhost:6379,abortConnect=false,connectTimeout=5000";
                
//                 Console.WriteLine("=== Quantity Measurement Application ===");
//                 Console.WriteLine($"SQL Server: {sqlConnectionString.Split(';')[0]}");
//                 Console.WriteLine($"Memurai (Redis): {memuraiConnectionString.Split(',')[0]}");
//                 Console.WriteLine();
                
//                 // Test Memurai connection
//                 Console.Write("Testing Memurai connection... ");
//                 bool cacheAvailable = TestMemuraiConnection(memuraiConnectionString);
                
//                 IQuantityHistoryRepository repository;
                
//                 if (cacheAvailable)
//                 {
//                     Console.WriteLine("✓ Memurai is available! Using Memurai Cache + SQL Database");
//                     repository = RepositoryFactory.CreateWithRedis(sqlConnectionString, memuraiConnectionString);
//                 }
//                 else
//                 {
//                     Console.WriteLine("✗ Memurai not available! Using SQL Database only");
//                     repository = RepositoryFactory.CreateWithoutCache(sqlConnectionString);
//                 }
                
//                 Console.WriteLine();
                
//                 // Configure services with repository
//                 ServiceLocator.ConfigureServices(repository);
                
//                 // Get service
//                 var quantityService = ServiceLocator.GetService<IQuantityService>();
                
//                 // Run application
//                 var mainMenu = new MainMenu(quantityService);
                
//                 while (true)
//                 {
//                     Console.Clear();
//                     mainMenu.Run();
                    
//                 }
//             }
//             catch (Exception ex)
//             {
//                 Console.ForegroundColor = ConsoleColor.Red;
//                 Console.WriteLine($"\nFatal Error: {ex.Message}");
//                 Console.ResetColor();
                
//                 Console.WriteLine("\nTroubleshooting:");
//                 Console.WriteLine("1. Make sure SQL Server is running");
//                 Console.WriteLine("2. Make sure Memurai is installed and running");
//                 Console.WriteLine("3. Run 'memurai-cli ping' to test Memurai");
//                 Console.WriteLine("\nPress any key to exit...");
//                 Console.ReadKey();
//             }
//         }

//         static bool TestMemuraiConnection(string connectionString)
//         {
//             try
//             {
//                 using var redis = StackExchange.Redis.ConnectionMultiplexer.Connect(connectionString);
//                 var db = redis.GetDatabase();
//                 var ping = db.Ping();
//                 Console.WriteLine($"Memurai ping: {ping.TotalMilliseconds}ms");
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Connection failed: {ex.Message}");
//                 return false;
//             }
//         }
//     }
// }