using AirePuro.Views.Pantalla;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AirePuro.ViewModel
{
    internal class VMLogin : BaseViewModel
    {
        private string _campo1;
        private string _campo2;
        private bool _camposRellenados;

        public string Campo1
        {
            get { return _campo1; }
            set
            {
                _campo1 = value;
                OnPropertyChanged(nameof(Campo1));
                VerificarCamposRellenados();
            }
        }

        public string Campo2
        {
            get { return _campo2; }
            set
            {
                _campo2 = value;
                OnPropertyChanged(nameof(Campo2));
                VerificarCamposRellenados();
            }
        }

        public bool CamposRellenados
        {
            get { return _camposRellenados; }
            set
            {
                _camposRellenados = value;
                OnPropertyChanged(nameof(CamposRellenados));
            }
        }

        public ICommand Iniciarcommand { get; private set; }
        public ICommand Registrarcommand { get; private set; }

        public VMLogin(INavigation naivigation)
        {
            Navigation = naivigation;
            Iniciarcommand = new Command(async () => await IniciarSesion(), () => CamposRellenados);
            Registrarcommand = new Command(async () => await Registrar());
        }

        private void VerificarCamposRellenados()
        {
            CamposRellenados = !string.IsNullOrWhiteSpace(Campo1) && !string.IsNullOrWhiteSpace(Campo2);
        }

        private async Task IniciarSesion()
        {
            string usuarioRegistrado = Preferences.Get("Usuario", string.Empty);
            string contraseñaRegistrada = Preferences.Get("Contraseña", string.Empty);

            if (Campo1 == usuarioRegistrado && Campo2 == contraseñaRegistrada)
            {
                await Application.Current.MainPage.DisplayAlert("", "Inicio de sesión Exitoso", "ok");
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
            }

        }

        private async Task Registrar()
        {
            await Navigation.PushAsync(new Registrarte());
        }
    }
}