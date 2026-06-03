using System;
using Microsoft.Maui.Controls;

namespace HopDelivery.Views.Catalogos
{
    public partial class AgregarCervezaPage : ContentPage
    {
        public AgregarCervezaPage()
        {
            InitializeComponent();
        }

        private void OnGuardarClicked(object sender, EventArgs e)
        {
           
            string marca = MarcaEntry.Text;
            string nombre = NombreEntry.Text;
            string abv = ABVEntry.Text;
            string calificacion = CalificacionEntry.Text;
            string descripcion = DescripcionEntry.Text;

           

            DisplayAlert("Éxito", "La cerveza está lista para enviarse", "OK");
        }
    }
}