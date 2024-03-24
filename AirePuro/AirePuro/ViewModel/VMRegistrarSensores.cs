using AirePuro.Model;
using AirePuro.Model.Listados;
using AirePuro.Simulacion;
using AirePuro.Views.Pantalla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirePuro.ViewModel
{
    internal class VMRegistrarSensores : BaseViewModel
    {
        #region Variables
        public List<DPikerListado> _ListaSensores { get; set; }
        public List<PinesEncendido> _PinesEncendido { get; set; }
        public List<PinesRPM> _PinesRPM { get; set; }
        
        public string _ID;
        public string _Habitacion;

        private DPikerListado _selectedSensor = new DPikerListado();
        private PinesEncendido _selectedEncendido = new PinesEncendido();
        private PinesRPM _selectedRPM = new PinesRPM();

        public Ventiladoressim _VENTILADORES = Ventiladoressim.Instancia;
        public SenGazSim _SensorGaz = SenGazSim.Instancia;
        public SenTepsim _SensorTemperatura = SenTepsim.Instancia;
        private Random random = new Random();
        private List<MVentilador> listaVenti;

        #endregion
        public VMRegistrarSensores(INavigation naivigation)
        {
            Navigation = naivigation;
            _ListaSensores = GetLista();
            _PinesEncendido = GetPinesEncendido();
            _PinesRPM = GetPinesRPM();
            Task.Run(async () =>
            {
                listaVenti = await _VENTILADORES.ObtenerAreglo();
                // Filtrar los pines de encendido y RPM para que no se muestren en el DataPicker
                _PinesEncendido = _PinesEncendido.Where(p => !listaVenti.Any(v => v.pinEnsendido == p.pinEnsendido)).ToList();
                _PinesRPM = _PinesRPM.Where(p => !listaVenti.Any(v => v.pinRPM == p.pinRPM)).ToList();
            }).Wait();

        }

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

        public List<DPikerListado> GetLista()
        {
            return new List<DPikerListado>()
            {
                new DPikerListado(){Key= 1,Value="Ventilador" },
                new DPikerListado(){Key= 2,Value="Gaz" },
                new DPikerListado(){Key= 3,Value="Temperatura" }
            };
        }

        public List<PinesRPM> GetPinesRPM()
        {
            return new List<PinesRPM>()
            {
                new PinesRPM(){Key= 1,pinRPM="22" },
                new PinesRPM(){Key= 2,pinRPM="23" },
                new PinesRPM(){Key= 3,pinRPM="18" },
                new PinesRPM(){Key= 3,pinRPM="19" }

            };
        }
        public List<PinesEncendido> GetPinesEncendido()
        {
            return new List<PinesEncendido>()
            {
                new PinesEncendido(){Key= 1,pinEnsendido="4" },
                new PinesEncendido(){Key= 2,pinEnsendido="2" },
                new PinesEncendido(){Key= 3,pinEnsendido="15" },
                new PinesEncendido(){Key= 3,pinEnsendido="13" }
            };
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

        public async Task InsertarSensor()
        {
            try
            {
            
                switch (SelectedSensor.Value)
                {
                    case "Ventilador":
                        {


                            if (listaVenti.Any(v => v.pinRPM == SelectedRPM.pinRPM || v.pinEnsendido == SelectedEncendido.pinEnsendido))
                            {
                                if (listaVenti.Any(v2 => v2.pinRPM == SelectedRPM.pinRPM))
                                    await DisplayAlert("Advertencia", $"El puerto de rpm {SelectedRPM.pinRPM} ya esta en uso.", "Aceptar");
                                else
                                    await DisplayAlert("Advertencia", $"El puerto de ensendido {SelectedEncendido.pinEnsendido} ya esta en uso.", "Aceptar");
                                return;
                            }

                            MVentilador ventilador = new MVentilador();
                            ventilador.id = ID;
                            ventilador.ubicacion = Habitacion;
                            ventilador.rpm = random.Next(699, 3201);
                            ventilador.encendido = true;
                            ventilador.pinEnsendido = SelectedEncendido.pinEnsendido;
                            ventilador.pinRPM = SelectedRPM.pinRPM;
                            if (ventilador.pinRPM != null || ventilador.pinEnsendido != null)
                            {
                                if (await _VENTILADORES.Insertar(ventilador))
                                    await DisplayAlert("Registro", $"Se registro el componente {SelectedSensor.Value}", "Aceptar");
                              
                            }
                            else
                                await DisplayAlert("Registro", "seleccione un puerto", "Aceptar");

                            break;
                        }
                    case "Gaz":
                        {
                            MSenGaz _senGaz = new MSenGaz();
                            _senGaz.id = ID;
                            _senGaz.ubicacion = Habitacion;
                            _senGaz.gasDetectado = "Co2";//cambiar a 0


                            if (await _SensorGaz.Insertar(_senGaz))
                            {
                                DisplayAlert("Registro", $"Se registro el sensor de {SelectedSensor.Value}", "Aceptar");
                            }
                            else
                                DisplayAlert("Registro", "Registro fallido ser alcanzo el limite de resgistos de 10", "Aceptar");
                        }
                        break;
                    case "Temperatura":
                        {
                            MSenTemp _senTemp = new MSenTemp();
                            _senTemp.ID = ID;
                            _senTemp.Habitacion = Habitacion;
                            _senTemp.Humedad = (random.Next(10, 60)).ToString();//cambiar a 0
                            _senTemp.Temp = (random.Next(-19, 41)).ToString();

                            if (_SensorTemperatura.Insertar(_senTemp))
                            {
                                DisplayAlert("Registro", $"Se registro el sensor de {SelectedSensor.Value}", "Aceptar");
                            }
                            else
                                DisplayAlert("Registro", "Registro fallido ser alcanzo el limite de resgistos de 10", "Aceptar");
                        }
                        break;


                    default:
                        await DisplayAlert("Error inesperado", $"¿Qué seleccionaste?", "Aceptar");
                        break;
                }
               
            }
            catch (Exception ex)
            {
              
                await DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "Aceptar");
            }
            await Volver();
        }


        public async Task Volver()
        {
            await Navigation.PopAsync();
        }


        #region Comandos
        public ICommand InsertarSensorcommand => new Command(async () => await InsertarSensor());
        #endregion
    }
}
