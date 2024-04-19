using AirePuro.Model;
using AirePuro.Simulacion;
using AirePuro.Simulacion.Logueo;
using AirePuro.Views.Pantalla;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections;
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

        public Logueo _Logueo = Logueo.Instancia;

        public Ventiladoressim _VENTILADORES;//BORRAR
        private List<MVentilador> _SensoresVentiladores;
        private Chart _grafica;

        #endregion

        public VMMonitoreo(INavigation navigation)
        {
            Navigation = navigation;
            Graficas();
        }

        #region Objetos
        public Chart Grafica
        {
            get { return _grafica; }
            set { _grafica = value; }
        }
        public List<MVentilador> Lista
        {
            get
            {


                return _SensoresVentiladores;
            }
            set
            {
                SetValue(ref _SensoresVentiladores, value);
                OnpropertyChanged();

            }
        }

        #endregion

        #region Procesos


        public async Task Graficas()
        {
            string idUsuario = await _Logueo.OpteneteUsuari();

         
            Lista = await _VENTILADORES.ObtenerAreglo(idUsuario);

            // Crear una lista de ChartEntry para almacenar las entradas de la gráfica
            var entries = new List<ChartEntry>();

            // Recorrer la lista de ventiladores y agregar las entradas de la gráfica basadas en el valor de RPM
            foreach (var ventilador in Lista)
            {
                entries.Add(new ChartEntry(ventilador.rpm)
                {
                    Color = SKColor.Parse("#ff0000"), // Color rojo para porcentaje mayor a 70
                });
            }

            // Crear la gráfica con las entradas generadas dinámicamente
            Grafica = new RadialGaugeChart
            {
                Entries = entries,
                MinValue = -1,
                MaxValue = 1200,
                BackgroundColor = SKColor.Parse("#00FFFFFF"),
                Margin = 0,
            };
        }
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