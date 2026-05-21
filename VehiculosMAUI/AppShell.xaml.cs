using VehiculosMAUI.Views;

namespace VehiculosMAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Las rutas DEBEN ir aquí adentro, no afuera
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(AgregarCervezaPage), typeof(AgregarCervezaPage));
    }
}