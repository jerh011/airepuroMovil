using AirePuro.Model;
using AirePuro.Model.Listados;
using AirePuro.Simulacion;
using AirePuro.Simulacion.Logueo;
using AirePuro.Views.Pantalla;
using Microcharts;
using SkiaSharp;
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
        private Ventiladoressim _VENTILADORES = Ventiladoressim.Instancia;//BORRAR
        private Logueo _Logueo = Logueo.Instancia;
        private Chart _grafica;
      
        #endregion

        public VMVentiladores(INavigation navigation)
        {
            Navigation = navigation;
            
            ListarVentiladores();
        }

        #region Procesos
        public List<MVentilador> Lista
        {
            get {
            
             
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
        
        public async Task IrSensoresGas()
        {
            await Navigation.PushAsync(new SenGaz());
        }

        public async Task ListarVentiladores()
        {

            //string idUsuario = await _logueo.OpteneteUsuari();
            string idUsuario = "661c848c684503e526b9bee1";
            Lista = await _VENTILADORES.ObtenerAreglo(idUsuario);
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