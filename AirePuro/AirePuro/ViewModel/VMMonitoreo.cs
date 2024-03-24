using AirePuro.Model;
using AirePuro.Simulacion;
using AirePuro.Views.Pantalla;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirePuro.ViewModel
{
    internal class VMMonitoreo : BaseViewModel
    {
        #region variables


        public Ventiladoressim _VENTILADORES;//BORRAR

        #endregion

        public VMMonitoreo(INavigation navigation)
        {
            Navigation = navigation;

        }

        #region Objetos

        #endregion

        #region Procesos
        public async Task IrSensoresGas()
        {

            await Navigation.PushAsync(new SenGaz());
        }

        public async Task IrVentiladores()
        {

            await Navigation.PushAsync(new Ventiladores());
        }
        public async Task IrTemperaturas()
        {

            await Navigation.PushAsync(new SenosorTemp());
        }

        public async Task RegistrarSensor()
        {
            await Navigation.PushAsync(new RegistrarSensores());
        }

        #endregion


        #region Comandos


        public ICommand IrSensoresGascommand => new Command(async () => await IrSensoresGas());
        public ICommand IrVentiladorescommand => new Command(async () => await IrVentiladores());
        public ICommand IrTemperaturacommand => new Command(async () => await IrTemperaturas());


        public ICommand RegistrarSensorcommand => new Command(() => RegistrarSensor());
        #endregion
    }
}