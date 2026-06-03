using HopDelivery.Views;
using HopDelivery.Views.Catalogos;
using Microsoft.Maui.Controls;
using System;

namespace HopDelivery
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

          
            Routing.RegisterRoute(nameof(AgregarCervezaPage), typeof(AgregarCervezaPage));
            Routing.RegisterRoute(nameof(CatMarcaPage), typeof(CatMarcaPage));
            Routing.RegisterRoute(nameof(CatalogosPage), typeof(CatalogosPage));
        }
    }
}