using AirePuro.Model;
using AirePuro.ViewModel.EditarModulo;
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
    public partial class EditarTemperaturaModulo : ContentPage
    {
        public EditarTemperaturaModulo(MSenTemp temperatura)
        {
            InitializeComponent();
            BindingContext = new VMEditarTemperaturaModulo(Navigation, temperatura);
        }
    }
}