namespace QuantityMeasurementApp.Application.Menus
{
    public interface IMenu
    {
        void Display();
        void HandleInput();
        string GetMenuTitle();
    }
}