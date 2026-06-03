namespace HopDelivery
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Aquí configuramos el Login como página de inicio
            MainPage = new NavigationPage(new HopDelivery.Views.LoginPage());
        }
    }
}