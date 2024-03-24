using AirePuro.Model;
using AirePuro.ViewModel;
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
    public partial class EditarVentiladorModulo : ContentPage
    {
        public EditarVentiladorModulo(MVentilador ventilador)
        {
            InitializeComponent();
            BindingContext = new VMEditarVentiladorModulo(Navigation, ventilador);
        }
    }
}