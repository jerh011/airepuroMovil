using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirePuro.ViewModel
{
    internal class VMconfigSensores : BaseViewModel
    {
        #region VARIABLES
        string _ActivoTipoTemperaturaFarenheit = "#bbd0f7";
        string _ActivoTipoTemperaturaCelcius = "#bbd0f7";
        string _HorarioActivoTemperatura_24Hrs = "#bbd0f7";
        string _HorarioActivoTemperatura_7_10Hrs = "#bbd0f7";
        #endregion
        #region CONTRUCTOR
        public VMconfigSensores(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
        #region OBJETOS
        public string ActivoTipoTemperaturaFarenheit
        {
            get { return _ActivoTipoTemperaturaFarenheit; }
            set { SetValue(ref _ActivoTipoTemperaturaFarenheit, value); }
        }
        public string ActivoTipoTemperaturaCelcius
        {
            get { return _ActivoTipoTemperaturaCelcius; }
            set { SetValue(ref _ActivoTipoTemperaturaCelcius, value); }
        }
        public string HorarioActivoTemperatura_24Hrs
        {
            get { return _HorarioActivoTemperatura_24Hrs; }
            set { SetValue(ref _HorarioActivoTemperatura_24Hrs, value); }
        }
        public string HorarioActivoTemperatura_7_10Hrs
        {
            get { return _HorarioActivoTemperatura_7_10Hrs; }
            set { SetValue(ref _HorarioActivoTemperatura_7_10Hrs, value); }
        }

        #endregion
        #region PROCESOS
        public async Task ProcesoAsyncrono()
        {

        }
        public async void TipoActivoFARENHEIT()
        {

            if (ActivoTipoTemperaturaFarenheit == "#bbd0f7")
            {
                await DisplayAlert("Operación exitosa", "Se cambio la temperatura a tipo Farenheit", "OK");
                ActivoTipoTemperaturaFarenheit = "Green";
                ActivoTipoTemperaturaCelcius = "#bbd0f7";
            }
            else
            {
                await DisplayAlert("Operación dudosa", "Ya se encuentra activo el tipo Farenheit", "OK");
            }
        }
        public async void TipoActivoCelcius()
        {

            if (ActivoTipoTemperaturaCelcius == "#bbd0f7")
            {
                await DisplayAlert("Operación exitosa", "Se cambio la temperatura a tipo Celcius", "OK");
                ActivoTipoTemperaturaCelcius = "Green";
                ActivoTipoTemperaturaFarenheit = "#bbd0f7";
            }
            else
            {
                await DisplayAlert("Operación dudosa", "Ya se encuentra activo el tipo Celcius", "OK");
            }
        }

        public async void Horario24Hrs()
        {

            if (HorarioActivoTemperatura_24Hrs == "#bbd0f7")
            {
                await DisplayAlert("Operación exitosa", "Se cambio el horario a 24 horas.", "OK");
                HorarioActivoTemperatura_24Hrs = "Green";
                HorarioActivoTemperatura_7_10Hrs = "#bbd0f7";
            }
            else
            {
                await DisplayAlert("Operación dudosa", "Ya se encuentra el horario a 24 horas.", "OK");
            }
        }
        public async void Horario7_10Hrs()
        {

            if (HorarioActivoTemperatura_7_10Hrs == "#bbd0f7")
            {
                await DisplayAlert("Operación exitosa", "Se cambio el horario a 10 horas.", "OK");
                HorarioActivoTemperatura_7_10Hrs = "Green";
                HorarioActivoTemperatura_24Hrs = "#bbd0f7";
            }
            else
            {
                await DisplayAlert("Operación dudosa", "Ya se encuentra el horario a 10 horas.", "OK");
            }
        }

        #endregion
        #region COMANDOS
        public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        public ICommand TipoActivoFARENHEITcommand => new Command(TipoActivoFARENHEIT);
        public ICommand TipoActivoCelciuscommand => new Command(TipoActivoCelcius);
        public ICommand Horario24Hrscommand => new Command(Horario24Hrs);
        public ICommand Horario7_10Hrscommand => new Command(Horario7_10Hrs);

        #endregion
    }
}
