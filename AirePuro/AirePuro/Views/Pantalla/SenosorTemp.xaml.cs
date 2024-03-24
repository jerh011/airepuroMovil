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
    public partial class SenosorTemp : ContentPage
    {

        VMSensorTemp vM;
        public SenosorTemp()
        {
            InitializeComponent();
            //NavigationPage.SetHasBackButton(this, false);
            vM = new VMSensorTemp(Navigation);
            BindingContext = vM;
            this.Appearing += ListaSenTemp_Appearing;
        }
        private async void ListaSenTemp_Appearing(object sender, EventArgs e)
        {
            await vM.ListarSenTemp();
        }
    }
}