using AirePuro.Model;
using AirePuro.Simulacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;
using AirePuro.Model.Listados;
using AirePuro.Views.PantallasMenuHamburgesa;
using AirePuro.Simulacion.Logueo;
using AirePuro.Views.Pantalla;

namespace AirePuro.ViewModel.EditarModulo
{
    internal class VMEditarVentiladorModulo : BaseViewModel
    {
        #region variables
        public List<DPikerListado> _ListaSensores { get; set; }
        public List<PinesEncendido> _PinesEncendido { get; set; }
        public List<PinesRPM> _PinesRPM { get; set; }
        public List<MVentilador> listaVenti;



        public string _ElimnacionComponente;
        public string _ID;
        public string _Habitacion;
        public string _PinRPM;
        public string _PinEncendido;

        private DPikerListado _selectedSensor = new DPikerListado();
        private PinesEncendido _selectedEncendido = new PinesEncendido();
        private PinesRPM _selectedRPM = new PinesRPM();


        //BORRAR de apartir de aqui despues del lunes o no 
        public Ventiladoressim _VENTILADORES = Ventiladoressim.Instancia;
        public Logueo _Logueo = Logueo.Instancia;

        private Random random = new Random();

        //aquillano
        #endregion

        #region consutructores
        public VMEditarVentiladorModulo(INavigation navigation, MVentilador _Modulo)
        {
            Navigation = navigation;
            _ListaSensores = GetLista();
            _PinesEncendido = GetPinesEncendido();
            _PinesRPM = GetPinesRPM();
            ID = _Modulo.id;
            Habitacion = _Modulo.ubicacion;

            PinRPM = _PinesRPM.FirstOrDefault(s => s.pinRPM == _Modulo.pinRPM)?.pinRPM;
            PinEncendido = _PinesEncendido.FirstOrDefault(s => s.pinEnsendido == _Modulo.pinEnsendido)?.pinEnsendido;
            SelectedSensor = _ListaSensores.FirstOrDefault(s => s.Value == "Ventilador");

            _ElimnacionComponente = "Ventilador";

           
            Task.Run(async () =>
            {
                string idUsuario = await _Logueo.OpteneteUsuari();
                listaVenti = await _VENTILADORES.ObtenerAreglo(idUsuario);
                // Filtrando los pines encendido que no están en uso por los ventiladores existentes
                _PinesEncendido = _PinesEncendido.Where(p => !listaVenti.Any(v => v.pinEnsendido == p.pinEnsendido)).ToList();
                // Filtrando los pines RPM que no están en uso por los ventiladores existentes
                _PinesRPM = _PinesRPM.Where(p => !listaVenti.Any(v => v.pinRPM == p.pinRPM)).ToList();
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
        public string PinRPM
        {
            get { return _PinRPM; }
            set { SetValue(ref _PinRPM, value); }
        }
        public string PinEncendido
        {
            get { return _PinEncendido; }
            set { SetValue(ref _PinEncendido, value); }
        }

        public DPikerListado SelectedSensor
        {
            get { return _selectedSensor; }
            set { SetValue(ref _selectedSensor, value); }
        }

        public PinesRPM SelectedRPM
        {
            get { return _selectedRPM; }
            set { SetValue(ref _selectedRPM, value); }
        }

        public PinesEncendido SelectedEncendido
        {
            get { return _selectedEncendido; }
            set { SetValue(ref _selectedEncendido, value); }
        }
        #endregion

        #region pruebas 
        public List<DPikerListado> GetLista()
        {
            var lista = new List<DPikerListado>()
            {
                new DPikerListado(){Key= 1,Value="Ventilador" },
                new DPikerListado(){Key= 2,Value="Gaz" },
                new DPikerListado(){Key= 3,Value="Temperatura" }
            };

            return lista;
        }


        public List<PinesRPM> GetPinesRPM()
        {
            return new List<PinesRPM>()
            {
                new PinesRPM(){Key= 1,pinRPM="25" },
                new PinesRPM(){Key= 2,pinRPM="26" },
                new PinesRPM(){Key= 3,pinRPM="27" },
                new PinesRPM(){Key= 3,pinRPM="14" }

            };
        }
        public List<PinesEncendido> GetPinesEncendido()
        {
            return new List<PinesEncendido>()
            {
                new PinesEncendido(){Key= 1,pinEnsendido="32" },
                new PinesEncendido(){Key= 2,pinEnsendido="23" },
                new PinesEncendido(){Key= 3,pinEnsendido="12" },
                new PinesEncendido(){Key= 3,pinEnsendido="13" }
            };
        }
        #endregion

        #region Procesos
        public void Editar()
        {
            Random random = new Random();
            MVentilador ventilador = new MVentilador();

            ventilador.id = ID;
            ventilador.ubicacion = Habitacion;
            if (SelectedEncendido.pinEnsendido != null)
                ventilador.pinEnsendido = SelectedEncendido.pinEnsendido;
            else
                ventilador.pinEnsendido = PinEncendido;

            if (SelectedRPM.pinRPM != null)
                ventilador.pinRPM = SelectedRPM.pinRPM;
            else
                ventilador.pinRPM = PinRPM;

           _VENTILADORES.Actualizardatos(ventilador);
                  
            Volver();
        }
        public async Task EliminarAsync()
        {
            if (await _VENTILADORES.EliminarDatos(ID))
            {
                DisplayAlert("Eliminado", $"Se a Eliminado el componente {_ElimnacionComponente}", "Aceptar");

            }
            else
                DisplayAlert("Eliminado", "Eliminado fallida", "Aceptar");
                  
            Volver();
        }
        public void PrenderVentilador()
        {

            _VENTILADORES.ApagarVentilador(ID, true);

            Volver();
        }
        public void ApagarVentilador()
        {

            _VENTILADORES.ApagarVentilador(ID, false);

            Volver();
        }

        public async Task Volver()
        {
            await Navigation.PushAsync(new Ventiladores());
        }
        #endregion

        #region Comandos
        public ICommand EditarModuloSensorcommand => new Command(() => Editar());
        public ICommand Prendercommand => new Command(() => PrenderVentilador());
        public ICommand Apagarcommand => new Command(() => ApagarVentilador());
        public ICommand EliminarModuloSensorcommand => new Command(() => EliminarAsync());

        #endregion
    }
}