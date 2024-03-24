using AirePuro.Model;
using AirePuro.Model.Listados;
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
    internal class VMVentiladores : BaseViewModel
    {
        #region variables
        private List<MVentilador> _SensoresVentiladores;
    

        #endregion

        public VMVentiladores(INavigation navigation)
        {
            Navigation = navigation;

            ListarVentiladores();
        }

        #region Procesos
        public List<MVentilador> Lista
        {
            get { return _SensoresVentiladores; }
            set
            {
                SetValue(ref _SensoresVentiladores, value);
                OnpropertyChanged();

            }
        }
        #endregion
        #region Procesos

        public async Task IrSensoresGas()
        {
            await Navigation.PushAsync(new SenGaz());
        }

        public async Task ListarVentiladores()
        {
            var _VENTILADORES = Ventiladoressim.Instancia;//BORRAR
            Lista =await  _VENTILADORES.ObtenerAreglo();
        }
        public async Task IraEditar(MVentilador _modulo)
        {
            await Navigation.PushAsync(new EditarVentiladorModulo(_modulo));
        }

        #endregion

        #region Comandos
        public ICommand IrSensoresGascommand => new Command(async () => await IrSensoresGas());
        public ICommand IraEditarcommand => new Command<MVentilador>(async (p) => await IraEditar(p));
        #endregion

    }
}
