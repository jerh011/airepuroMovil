using AirePuro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AirePuro.Views.Pantalla
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrarte : ContentPage
    {
        public Registrarte()
        {
            InitializeComponent();
            BindingContext = new VMRegistrarse(Navigation);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.BackgroundColor = Color.Green;
            await button.ScaleTo(1.2, 200, Easing.SinOut);
            await Task.Delay(200);
            await button.ScaleTo(1, 200, Easing.SinOut);
            button.BackgroundColor = Color.FromHex("#FEC100");

        }
    }
}