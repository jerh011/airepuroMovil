using AirePuro.Model;
using AirePuro.Model.Listados;
using AirePuro.Simulacion;
using AirePuro.Simulacion.Logueo;
using AirePuro.Views.Pantalla;
using AirePuro.Views.PantallasMenuHamburgesa;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirePuro.ViewModel
{
    internal class VMRegistro : BaseViewModel
    {
        #region variables
        private List<MHistorial> _historial;
        private HistorialConexion _VENTILADORES = HistorialConexion.Instancia;//BORRAR
        private Logueo _Logueo = Logueo.Instancia;

        #endregion

        public VMRegistro(INavigation navigation)
        {
            Navigation = navigation;

            ListarVentiladores();
        }

        #region Procesos
        public List<MHistorial> Lista
        {
            get { return _historial; }
            set
            {
                SetValue(ref _historial, value);
                OnpropertyChanged();

            }
        }
        #endregion
        #region Procesos

       

        public async Task ListarVentiladores()
        {
            
            string idUsuario = await _Logueo.OpteneteUsuari();

            Lista =await  _VENTILADORES.ObtenerAreglo(idUsuario);
        }
        public async Task IraEditar(MHistorial _modulo)
        {
            await Navigation.PushAsync(new Historial1(_modulo));
        }
        public async Task agregarregistro()
        {
            _VENTILADORES.agregarRegistro(await _Logueo.OpteneteUsuari());

            await Navigation.PopAsync();
        }

        #endregion

        #region Comandos

        public ICommand IraEditarcommand => new Command<MHistorial>(async (p) => await IraEditar(p));
        public ICommand Agregarregistrocommand => new Command(async () => await agregarregistro());
        #endregion

    }
}
