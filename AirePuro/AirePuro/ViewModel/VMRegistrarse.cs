using AirePuro.Model;
using AirePuro.Simulacion;
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
    public class VMRegistrarse : BaseViewModel
    {
        #region Variables
        private ConexionLogin _ConexionLogin = new ConexionLogin();
        private MUsuario _MUsuario = new MUsuario();
        private string _usuario;
        private string _numero;
        private string _contraseña;
        private string _confirmarContraseña;
        private bool _camposRellenos;
        #endregion
        public VMRegistrarse(INavigation naivigation)
        {
            Navigation = naivigation;
           Registrarcommand = new Command(async () => await Registrar(), () => CamposRellenados);
        }
        #region Objetos
        public string Usuario
        {
            get { return _usuario; }
            set
            {
                _usuario = value;
                OnPropertyChanged(nameof(Usuario));
                VerificarCamposRellenados();
            }
        }
        public string Numero
        {
            get { return _numero; }
            set
            {
                _numero = value;
                OnPropertyChanged(nameof(Numero));
                VerificarCamposRellenados();
            }
        }
        public string Contraseña
        {
            get { return _contraseña; }
            set
            {
                _contraseña = value;
                OnPropertyChanged(nameof(Contraseña));
                VerificarCamposRellenados();
            }
        }
        public string ConfirmarContraseña
        {
            get { return _confirmarContraseña; }
            set
            {
                _confirmarContraseña = value;
                OnPropertyChanged(nameof(ConfirmarContraseña));
                VerificarCamposRellenados();
            }
        }
        public bool CamposRellenados
        {
            get { return _camposRellenos; }
            set
            {
                _camposRellenos = value;
                OnPropertyChanged(nameof(CamposRellenados));
            }
        }
        #endregion
        #region Procesos

        private void VerificarCamposRellenados()
        {
            CamposRellenados = !string.IsNullOrWhiteSpace(Usuario) && !string.IsNullOrWhiteSpace(Numero) && !string.IsNullOrWhiteSpace(Contraseña) && !string.IsNullOrWhiteSpace(ConfirmarContraseña);
        }

        public async Task Registrar()
        {
            if (Contraseña != ConfirmarContraseña)
            {
                await DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return; // No permitir el registro si las contraseñas no coinciden
            }
            else
            {
                // Guardar los datos del usuario en Preferences
                /*
                   Preferences.Set("Usuario", Usuario);
                   Preferences.Set("Contraseña", Contraseña);
                */


                _MUsuario.Cuenta = Usuario;
                _MUsuario.Numero = Numero;
                _MUsuario.Contrasena = Contraseña;
                string mensage= await _ConexionLogin.Registrar(_MUsuario);
                if (mensage == "Usuario creado exitosamente")
                {
                    await DisplayAlert("Felicidades", $"{mensage}", "ok");
                    await Navigation.PushAsync(new Login());

                }
                else
                {
                    await DisplayAlert("Error", $"{mensage}", "OK");
                    return; // No permitir el registro si las contraseñas no coinciden
                }
            }
        }
        public async Task VolverAtras()
        {
            await Navigation.PushAsync(new Login());
        }

        #endregion


        #region Comandos

        public ICommand volvercommand => new Command(async () => await VolverAtras());

        public ICommand Registrarcommand { get; private set; }
        #endregion
    }
}
