using Microsoft.Maui.Controls;

namespace HopDelivery.App 
{
    public partial class App : Application
    {
        public App(Views.LoginPage loginPage)
        {
            InitializeComponent();

            
            MainPage = new NavigationPage(loginPage);
        }
    }
}