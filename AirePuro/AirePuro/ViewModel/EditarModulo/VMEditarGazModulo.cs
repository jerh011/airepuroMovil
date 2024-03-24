using AirePuro.Model;
using AirePuro.Simulacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AirePuro.Model.Listados;
using AirePuro.Views.PantallasMenuHamburgesa;

namespace AirePuro.ViewModel.EditarModulo
{
    internal class VMEditarGazModulo : BaseViewModel
    {
        #region variables
        public List<DPikerListado> _ListaSensores { get; set; }
        public List<PinesEncendido> _PinesEncendido { get; set; }
        public List<PindeGaz> _PinesGaz { get; set; }
        private List<MSenGaz> listaGaz;

        public string _ElimnacionComponente;
        public string _ID;
        public string _Habitacion;
        public string _PinGaz;

        private DPikerListado _selectedSensor = new DPikerListado();
        private PindeGaz _selectedGaz = new PindeGaz();

        public SenGazSim _SenGazSim = SenGazSim.Instancia;
        #endregion

        #region consutructores
        public VMEditarGazModulo(INavigation navigation, MSenGaz _Modulo)
        {
            Navigation = navigation;
            _ListaSensores = GetLista();
            _PinesGaz = GetPinesGaz();
            _ID = _Modulo.id;
            _Habitacion = _Modulo.ubicacion;

            _PinGaz = _PinesGaz.FirstOrDefault(p => p.pindeGaz == _Modulo.pinGaz)?.pindeGaz;
            _selectedSensor = _ListaSensores.FirstOrDefault(s => s.Value == "Gaz");

            _ElimnacionComponente = "Ventilador";

            Task.Run(async () =>
            {
                listaGaz = await _SenGazSim.ObtenerAreglo();

                _PinesGaz = _PinesGaz.Where(p => !listaGaz.Any(v => v.pinGaz == p.pindeGaz)).ToList();
            }).Wait();
        }
        #endregion

        #region Objetos
        public string ID
        {
            get { return _ID; }
            set { SetValue(ref _ID, value); }
        }
        public string Habitacion
        {
            get { return _Habitacion; }
            set { SetValue(ref _Habitacion, value); }
        }
        public string PinGaz
        {
            get { return _PinGaz; }
            set { SetValue(ref _PinGaz, value); }
        }

        public DPikerListado SelectedSensor
        {
            get { return _selectedSensor; }
            set { SetValue(ref _selectedSensor, value); }
        }

        public PindeGaz SelectedGaz
        {
            get { return _selectedGaz; }
            set { SetValue(ref _selectedGaz, value); }
        }
        #endregion

        #region pruebas 
        public List<DPikerListado> GetLista()
        {
            var lista = new List<DPikerListado>()
            {
                new DPikerListado(){Key= 1, Value="Ventilador" },
                new DPikerListado(){Key= 2, Value="Gaz" },
                new DPikerListado(){Key= 3, Value="Temperatura" }
            };

            return lista;
        }
        public List<PindeGaz> GetPinesGaz()
        {
            return new List<PindeGaz>()
            {
                new PindeGaz(){Key= 1, pindeGaz="12" },
                new PindeGaz(){Key= 2, pindeGaz="14" },
                new PindeGaz(){Key= 3, pindeGaz="27" }
            };
        }
        #endregion

        #region Procesos
        public void Editar()
        {
            MSenGaz sensoGaz = new MSenGaz();

            sensoGaz.id = ID;
            sensoGaz.ubicacion = Habitacion;
            sensoGaz.gasDetectado = "Co2";
            sensoGaz.pinGaz = PinGaz;
            _SenGazSim.Actualizardatos(sensoGaz);

            Volver();
        }
        public async Task EliminarAsync()
        {
            if (await _SenGazSim.EliminarDatos(ID))
            {
                await Application.Current.MainPage.DisplayAlert("Eliminado", $"Se ha eliminado el componente {_ElimnacionComponente}", "Aceptar");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Eliminado", "Eliminación fallida", "Aceptar");
            }

            await Volver();
        }

        public async Task Volver()
        {
            await Navigation.PushAsync(new Monitoreo());
        }
        #endregion

        #region Comandos
        public ICommand EditarModuloSensorcommand => new Command(() => Editar());
        public ICommand EliminarModuloSensorcommand => new Command(async () => await EliminarAsync());
        #endregion
    }
}
