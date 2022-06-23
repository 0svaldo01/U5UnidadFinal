using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using U5UnidadF.Views; 

namespace U5UnidadF
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.ListaPeliculasView()) { BarTextColor = Color.White , BarBackgroundColor = Color.FromHex("#1d1d27") };

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
