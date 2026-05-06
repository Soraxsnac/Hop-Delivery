using VehiculosMAUI.Views;
using VehiculosMAUI.Views.Catalogos;

namespace VehiculosMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(Views.LoginPage));
            Routing.RegisterRoute(nameof(CatMarcaPage), typeof(Views.Catalogos.CatMarcaPage));
            Routing.RegisterRoute(nameof(RegistrarVehiculoPage), typeof(Views.RegistrarVehiculoPage));
        }
    }
}
