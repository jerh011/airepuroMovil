using AirePuro.Model;
using AirePuro.Simulacion;
using AirePuro.Simulacion.Logueo;
using AirePuro.Views.Pantalla;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirePuro.ViewModel
{
    internal class VMSensorTemp : BaseViewModel
    {
        #region variables
        private List<MSenTemp> _SensoresVentiladores;
        private SenTepsim _SenTep = SenTepsim.Instancia;
        private Logueo _Logueo = Logueo.Instancia;
        #endregion

        public VMSensorTemp(INavigation naivigation)
        {
            Navigation = naivigation;

            ListarSenTemp();


        }

        #region Procesos
        public List<MSenTemp> Lista
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

        public async Task ListarSenTemp()
        {
            string idUsuario = await _Logueo.OpteneteUsuari();
            Lista = await _SenTep.ObtenerAreglo(idUsuario);
        }


        public async Task IrSensoresGas()
        {

            await Navigation.PushAsync(new SenGaz());
        }
        public async Task IraEditar(MSenTemp _modulo)
        {
           await Navigation.PushAsync(new EditarTemperaturaModulo(_modulo));
        }

        #endregion


        #region Comandos


        public ICommand IrSensoresGascommand => new Command(async () => await IrSensoresGas());
        public ICommand IraEditarcommand => new Command<MSenTemp>(async (p) => await IraEditar(p));
        #endregion
    }
}
