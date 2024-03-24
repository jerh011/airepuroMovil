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
    public partial class Ventiladores : ContentPage
    {
        VMVentiladores vM;
        public Ventiladores()
        {
            InitializeComponent();
            //NavigationPage.SetHasBackButton(this, false);
            vM = new VMVentiladores(Navigation);
            BindingContext = vM;
            this.Appearing += Listaventiladores_Appearing;
        }
        private async void Listaventiladores_Appearing(object sender, EventArgs e)
        {
            await vM.ListarVentiladores();
        }

    }
}