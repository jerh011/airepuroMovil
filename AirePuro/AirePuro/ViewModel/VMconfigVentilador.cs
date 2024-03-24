using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirePuro.ViewModel
{
    internal class VMconfigVentilador : BaseViewModel
    {
        #region VARIABLES
        string _horas = "00:00";
        bool _VentiladorAutomatico = false;
        public int horas1 = 0;
        public int horas2 = 0;
        #endregion
        #region CONTRUCTOR
        public VMconfigVentilador(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
        #region OBJETOS
        public string HorasVentilador
        {
            get { return _horas; }
            set { SetValue(ref _horas, value); }
        }
        public bool VentiladorAuto
        {
            get { return _VentiladorAutomatico; }
            set { SetValue(ref _VentiladorAutomatico, value); }
        }
        #endregion
        #region PROCESOS
        public async Task ProcesoAsyncrono()
        {

        }
        public async void VentiladorAutomatico(Button button)
        {
            if (VentiladorAuto == false)
            {
                VentiladorAuto = true;
            }
            else
            {
                VentiladorAuto = false;
            }

            if (VentiladorAuto == true)
            {
                await DisplayAlert("Operación exitosa", "Se activó el ventilador automático", "OK");
                button.BackgroundColor = Color.Green;
                await button.ScaleTo(1.2, 200, Easing.SinOut);
                await button.ScaleTo(1, 200, Easing.SinOut);

            }
            else
            {
                await DisplayAlert("Operación exitosa", "Se cambió el ventilador automático al modo manual", "OK");
                await button.ScaleTo(1.2, 200, Easing.SinOut);
                await button.ScaleTo(1, 200, Easing.SinOut);
                button.BackgroundColor = Color.FromHex("#6699CC");
            }
        }
        public async void IncrementoHoras()
        {
            if (horas1 <= 58)
            {
                horas1 += 1;
                HorasVentilador = $"{horas2}:{horas1}";
                await DisplayAlert("Operacion exitosa", "Se aumento en minutos", "ok");
            }
            else
            {
                if (horas2 == 24)
                {
                    await DisplayAlert("Liminte maximo", "Son unas 24 horas", "ok");
                }
                else if (horas1 == 59)
                {
                    horas1 = 0;
                }
                if (horas2 <= 23)
                {
                    horas2 += 1;
                    HorasVentilador = $"{horas2}:{horas1}";
                    await DisplayAlert("Operacion exitosa", "Se aumento en horas", "ok");
                }
            }
        }
        public async void QuitaHoras()
        {
            if (horas1 != 0)
            {
                horas1 -= 1;
                HorasVentilador = $"{horas2}:{horas1}";
                await DisplayAlert("Operacion exitosa", "Se quitaron minutos", "ok");
            }
            else
            {
                if (horas2 != 0)
                {
                    horas2 -= 1;
                    HorasVentilador = $"{horas2}:{horas1}";
                    await DisplayAlert("Operacion exitosa", "Se quitaron horas", "ok");
                }
                else
                {
                    await DisplayAlert("Limite minimo", "No se permiten numeros negativos", "ok");
                }
            }
        }
        #endregion
        #region COMANDOS
        public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        public ICommand IncrementoHorascommand => new Command(IncrementoHoras);
        public ICommand QuitaHorascommand => new Command(QuitaHoras);
        public ICommand VentiladorAutomaticoCommand => new Command<Button>((button) => VentiladorAutomatico(button));
        #endregion

    }
}
