using QuantityMeasurementApp.Application.DependencyInjection;
using QuantityMeasurementApp.Application.Menus;
using QuantityMeasurementApp.Service.Interfaces;

namespace QuantityMeasurementApp.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // 1. Configure all services in ServiceLocator
                ServiceLocator.ConfigureServices();
                
                // 2. Get service from ServiceLocator
                var quantityService = ServiceLocator.GetService<IQuantityService>();
                
                // 3. Run application
                var mainMenu = new MainMenu(quantityService);
                
                while (true)
                {
                    Console.Clear();
                    mainMenu.Display();
                    mainMenu.HandleInput();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }
    }
}