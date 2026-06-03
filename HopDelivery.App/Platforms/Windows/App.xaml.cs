using Microsoft.Maui;
using Microsoft.Maui.Hosting;


namespace HopDelivery.WinUI
{
    public partial class App : MauiWinUIApplication
    {
        public App()
        {
            this.InitializeComponent();
        }

        
        protected override MauiApp CreateMauiApp() => global::HopDelivery.App.MauiProgram.CreateMauiApp();
    }
}