using Microsoft.Maui.Controls;

namespace HopDelivery.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

           
            MainPage = new HopDelivery.AppShell();
        }
    }
}