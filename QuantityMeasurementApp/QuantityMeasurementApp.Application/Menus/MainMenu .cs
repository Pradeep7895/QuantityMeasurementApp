using QuantityMeasurementApp.Service.Interfaces;

namespace QuantityMeasurementApp.Application.Menus
{
    /// <summary>
    /// Main Menu - Shows unit types and calls OperationMenu
    /// Entry point of the application's user interface
    /// </summary>
    public class MainMenu : BaseMenu
    {
        // Dictionary mapping measurement types to their available units
        private readonly Dictionary<string, string[]> _unitTypes;

        /// <summary>
        /// Constructor initializes main menu with service and unit types
        /// </summary>
        /// <param name="service">Quantity service for performing operations</param>
        public MainMenu(IQuantityService service) : base(service)
        {
            // Initialize all available unit types and their units
            _unitTypes = new Dictionary<string, string[]>
            {
                ["Length"] = new[] { "FEET", "INCH", "YARD", "CENTIMETERS" },
                ["Weight"] = new[] { "MILLIGRAM", "GRAM", "KILOGRAM", "POUND", "TONNE" },
                ["Volume"] = new[] { "LITRE", "MILLILITRE", "GALLON" },
                ["Temperature"] = new[] { "CELSIUS", "FAHRENHEIT", "KELVIN" }
            };
        }

        /// <summary>
        /// Initializes main menu options
        /// </summary>
        protected override void InitializeOptions()
        {
            _options.Add(new MenuOption("Length Measurements", () => ShowOperationMenu("Length")));
            _options.Add(new MenuOption("Weight Measurements", () => ShowOperationMenu("Weight")));
            _options.Add(new MenuOption("Volume Measurements", () => ShowOperationMenu("Volume")));
            _options.Add(new MenuOption("Temperature Measurements", () => ShowOperationMenu("Temperature")));
            _options.Add(new MenuOption("Exit", Exit));
        }

        /// <summary>
        /// Shows the operation menu for a specific measurement type
        /// </summary>
        /// <param name="measurementType">The selected measurement type (Length, Weight, etc.)</param>
        private void ShowOperationMenu(string measurementType)
        {
            // Get units for the selected measurement type
            var units = _unitTypes[measurementType];
            
            // Create operation menu for this measurement type
            var operationMenu = new OperationMenu(_service, measurementType, units);
            
            // Loop until user chooses to go back to main menu
            bool backToMain = false;
            while (!backToMain)
            {
                Console.Clear();
                operationMenu.Display();
                operationMenu.HandleInput();
                backToMain = operationMenu.IsBackSelected;
            }
        }

        private void Exit()
        {
            Console.WriteLine("\nThank you for using Quantity Measurement System!");
            Environment.Exit(0);
        }

        public override string GetMenuTitle() => "QUANTITY MEASUREMENT SYSTEM";
    }
}