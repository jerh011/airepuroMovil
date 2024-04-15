using AirePuro.Model;
using AirePuro.Simulacion;
using AirePuro.Simulacion.Logueo;
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
        #region variables
        private string _UsuarioLogin;
        private string _ContraseñaLogin;
        private bool _camposRellenados;
        private Logueo _Logueo = Logueo.Instancia;
  
        private ConexionLogin _ConexionLogin = new ConexionLogin();
        #endregion

        #region constructor
        public VMLogin(INavigation naivigation)
        {
            Navigation = naivigation;
            Iniciarcommand = new Command(async () => await IniciarSesion(), () => CamposRellenados);
            Registrarcommand = new Command(async () => await Registrar());
        }
        #endregion

        #region Objetos
        public string UsuarioLogin
        {
            get { return _UsuarioLogin; }
            set
            {
                _UsuarioLogin = value;
                OnPropertyChanged(nameof(UsuarioLogin));
                VerificarCamposRellenados();
            }
        }

        public string ContraseñaLogin
        {
            get { return _ContraseñaLogin; }
            set
            {
                _ContraseñaLogin = value;
                OnPropertyChanged(nameof(ContraseñaLogin));
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
        #endregion


        #region Procesos
        private void VerificarCamposRellenados()
        {
            CamposRellenados = !string.IsNullOrWhiteSpace(UsuarioLogin) && !string.IsNullOrWhiteSpace(ContraseñaLogin);
        }

        private async Task IniciarSesion()
        {
            /*
            string usuarioRegistrado = Preferences.Get("Usuario", string.Empty);
            string contraseñaRegistrada = Preferences.Get("Contraseña", string.Empty);
            */

            MUsuario _usuario = await _ConexionLogin.Logearse(UsuarioLogin, ContraseñaLogin);

            if (_usuario!=null)
            {

                await DisplayAlert("", "Inicio de sesión Exitoso", "ok");
                _Logueo.Insertar(_usuario);
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
            }

        }

        private async Task Registrar()
        {
            await Navigation.PushAsync(new Registrarte());
        }
        #endregion

        #region Comandos
        public ICommand Iniciarcommand { get; private set; }
        public ICommand Registrarcommand { get; private set; }
        #endregion
    }
}