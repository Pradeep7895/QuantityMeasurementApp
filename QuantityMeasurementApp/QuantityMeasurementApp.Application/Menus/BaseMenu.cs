using QuantityMeasurementApp.Service.Interfaces;

namespace QuantityMeasurementApp.Application.Menus
{
    public abstract class BaseMenu : IMenu
    {
        // Service for quantity operations - available to all derived menus
        protected readonly IQuantityService _service;
        
        // List of menu options with their descriptions and actions
        protected readonly List<MenuOption> _options;

        /// <summary>
        /// Constructor to initializes the menu with required service
        /// </summary>
        /// <param name="service">Quantity service for performing operations</param>
        protected BaseMenu(IQuantityService service)
        {
            _service = service;
            _options = new List<MenuOption>();
            InitializeOptions();
        }

        //Abstract method to be implemented by derived classes
        //Used to populate the menu with specific options
        protected abstract void InitializeOptions();

        public virtual void Display()
        {
            Console.WriteLine($"\n=== {GetMenuTitle()} ===");
            for (int i = 0; i < _options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_options[i].Description}");
            }
            Console.Write("Select option: ");
        }

        /// <summary>
        /// Handles user input and executes the selected menu option
        /// </summary>
        public virtual void HandleInput()
        {
            if (int.TryParse(Console.ReadLine(), out int choice) && 
                choice > 0 && choice <= _options.Count)
            {
                // Execute the action associated with the chosen option
                _options[choice - 1].Action();
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }

        public abstract string GetMenuTitle();

        /// <summary>
        /// Helper method to read a double value from console with validation
        /// </summary>
        protected double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    return value;
                }
                Console.WriteLine("Invalid number. Please try again.");
            }
        }

        /// <summary>
        /// Helper method to display unit options and get user selection
        /// </summary>
        /// <param name="prompt">The prompt to display</param>
        /// <param name="units">Array of available unit strings</param>
        protected string SelectUnit(string prompt, string[] units)
        {
            Console.WriteLine(prompt);
            for (int i = 0; i < units.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {units[i]}");
            }

            while (true)
            {
                Console.Write($"Select unit (1-{units.Length}): ");
                if (int.TryParse(Console.ReadLine(), out int choice) && 
                    choice >= 1 && choice <= units.Length)
                {
                    return units[choice - 1];
                }
                Console.WriteLine($"Please enter a number between 1 and {units.Length}");
            }
        }

        /// <summary>
        /// Helper method to pause execution and wait for user key press
        /// </summary>
        protected void WaitForKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Represents a menu option with description and executable action
    /// </summary>
    public class MenuOption
    {
        //Description of the menu option to display
        public string Description { get; }
        
        //Action to execute when option is selected
        public Action Action { get; }

        
        public MenuOption(string description, Action action)
        {
            Description = description;
            Action = action;
        }
    }
}