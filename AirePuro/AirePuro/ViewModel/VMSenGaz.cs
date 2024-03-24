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
    internal class VMSenGaz : BaseViewModel
    {
        #region variables
        public List<MSenGaz> _SensoresGaz;

        #endregion

        public VMSenGaz(INavigation naivigation)
        {
            Navigation = naivigation;

            ListarSenGaz();
            ///Esto se quitara despues 

        }

        #region Procesos
        public List<MSenGaz> Lista
        {
            get { return _SensoresGaz; }
            set
            {
                SetValue(ref _SensoresGaz, value);
                OnpropertyChanged();

            }
        }
        #endregion
        #region Procesos



        public async Task IrSensoresGas()
        {

            await Navigation.PushAsync(new SenGaz());
        }

        public async Task ListarSenGaz()
        {
            var _SenGaz = SenGazSim.Instancia;//BORRAR
            Lista = await _SenGaz.ObtenerAreglo();
        }
        public async Task IraEditar(MSenGaz _modulo)
        {
           // await Navigation.PushAsync(new EditarModulo(_modulo));
        }
        #endregion


        #region Comandos


        public ICommand IrSensoresGascommand => new Command(async () => await IrSensoresGas());
        public ICommand IraEditarcommand => new Command<MSenGaz>(async (p) => await IraEditar(p));
        #endregion
    }
}
