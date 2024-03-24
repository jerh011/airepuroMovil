using AirePuro.Model;
using AirePuro.Views.PantallasMenuHamburgesa;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirePuro.ViewModel
{
    internal class VMhistorial : BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        List<VMhistorial> Registros;

        //list provicional

        #endregion
        #region OBJETOS
        public List<VMhistorial> Lista
        {
            get { return Registros; }
            set
            {
                SetValue(ref Registros, value);
                OnpropertyChanged();

            }
        }
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }


        #endregion
        #region PROCESOS
        public async Task ProcesoAsyncrono()
        {

        }
        public void ProcesoSimple()
        {

        }
        public async Task VerMasLabelTapped()
        {
            await Navigation.PushAsync(new Historial1());
        }
        public async Task VerMasLabelTapped2()
        {
            await Navigation.PushAsync(new Historial2());
        }

        #endregion
        #region COMANDOS
        public ICommand VerMasLabelTapped2command => new Command(async () => await VerMasLabelTapped2());
        public ICommand VerMasLabelTappedcommand => new Command(async () => await VerMasLabelTapped());
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);

        #endregion
        #region CONTRUCTOR
        public VMhistorial(INavigation navigation)
        {
            Navigation = navigation;
            Registros = new List<VMhistorial>();
            {
                new MHistorial { Ubicacion = "Cocina", FechaHora = "24/02/2024", Temperatura = "25°", NombreGas = "Oxigeno", GasDetectado = "No", RPM = "30" };
                new MHistorial { Ubicacion = "Sala", FechaHora = "14/12/2024", Temperatura = "30°", NombreGas = "Dioxido de carbono", GasDetectado = "Si", RPM = "60" };
            };


            //Registros = new List<MHistorial>
            //{
            //    new MHistorial { Ubicacion = "Cocina", FechaHora ="24/02/2024",Temperatura="25°",NombreGas="Oxigeno",GasDetectado="No",RPM="30"},
            //    new MHistorial { Ubicacion = "Sala", FechaHora ="13/11/2024",Temperatura="35°",NombreGas="Dioxido de carbono",GasDetectado="Si",RPM="60"}
            //};

        }
        #endregion
    }
}
