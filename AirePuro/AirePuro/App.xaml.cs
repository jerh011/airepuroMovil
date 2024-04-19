using AirePuro.ViewModel;
using AirePuro.Views.Pantalla;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AirePuro
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDet { get; set; }
        public App()
        {
          

            InitializeComponent();

            MainPage = new NavigationPage(new Login());
            //MainPage = new NavigationPage(new Ventiladores());
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
