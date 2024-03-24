using AirePuro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AirePuro.Views.PantallasMenuHamburgesa
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Historial1 : ContentPage
    {
        public Historial1()
        {
            InitializeComponent();
            BindingContext = new VMhistorial(Navigation);

        }
    }
}