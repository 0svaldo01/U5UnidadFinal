using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U5UnidadF.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace U5UnidadF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPeliculasView : ContentPage
    {
        public ListaPeliculasView()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AgregarPeliculaView());
        }
        private async void SwipeItem_Clicked(object sender, EventArgs e)
        {
           
            var swi = (SwipeItem)sender; 
            if (await DisplayAlert("Eliminar", $"¿Eliminar a {((Pelicula)swi.CommandParameter).Titulo} ?", "Si", "No") == true)
            {
                avme.EliminarCommand.Execute(swi.CommandParameter);
            }

        }
    }
}