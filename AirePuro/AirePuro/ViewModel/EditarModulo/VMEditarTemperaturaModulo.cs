﻿using AirePuro.Model;
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

namespace AirePuro.ViewModel.EditarModulo
{
    internal class VMEditarTemperaturaModulo : BaseViewModel
    {
        #region variables
        public List<DPikerListado> _ListaSensores { get; set; }
        public List<PindatosTemp> _PindatosTemp { get; set; }
  
        private List<MSenTemp> listaTemperatura;

        public string _ElimnacionComponente;
        public string _ID;
        public string _Habitacion;
        public string _PinDatos;
        public string _Humedad;
        public string _Tempertatura;

        private DPikerListado _selectedSensor = new DPikerListado();
        private PindatosTemp _selectedPindatosTemp = new PindatosTemp();
        

        //BORRAR de apartir de aqui despues del lunes o no 
        public SenTepsim _Sentemperatura = SenTepsim.Instancia;
        private Random random = new Random();

        //aquillano
        #endregion

        #region consutructores
        public VMEditarTemperaturaModulo(INavigation navigation, MSenTemp _Modulo)
        {
            Navigation = navigation;
            _ListaSensores = GetLista();
         
            ID = _Modulo.id;
            Habitacion = _Modulo.ubicacion;
            Temperatura = _Modulo.temperatura;
            Humedad=_Modulo.humedad;
            PinDatos=_Modulo.PinDatos;
            SelectedSensor = _ListaSensores.FirstOrDefault(s => s.Value == "Temperatura");

            _PindatosTemp = GetPinesDatosTemp();

            Task.Run(async () =>
            {
                listaTemperatura = await _Sentemperatura.ObtenerAreglo();
                _PindatosTemp = _PindatosTemp.Where(p => !listaTemperatura.Any(v => v.PinDatos == p.pinTemp)).ToList();
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
        public string Temperatura
        {
            get { return _Tempertatura; }
            set { SetValue(ref _Tempertatura, value); }
        }
        public string Humedad
        {
            get { return _Humedad; }
            set { SetValue(ref _Humedad, value); }
        }

        public string PinDatos
        {
            get { return _PinDatos; }
            set { SetValue(ref _PinDatos, value); }
        }

        public DPikerListado SelectedSensor
        {
            get { return _selectedSensor; }
            set { SetValue(ref _selectedSensor, value); }
        }

        public PindatosTemp SelectedPinTemp
        {
            get { return _selectedPindatosTemp; }
            set { SetValue(ref _selectedPindatosTemp, value); }
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

        public List<PindatosTemp> GetPinesDatosTemp()
        {
            return new List<PindatosTemp>()
            {
                new PindatosTemp(){Key= 1,pinTemp="4" },
                new PindatosTemp(){Key= 2,pinTemp="2" },
                new PindatosTemp(){Key= 3,pinTemp="15" },
                new PindatosTemp(){Key= 4,pinTemp="13" }
            };
        }
        #endregion

        #region Procesos
        public void Editar()
        {
            Random random = new Random();
            MSenTemp temperatura = new MSenTemp();

            temperatura.id = ID;
            temperatura.ubicacion = Habitacion;
            temperatura.humedad = Humedad;
            temperatura.temperatura=Temperatura;
            temperatura.PinDatos = PinDatos;

            _Sentemperatura.Actualizardatos(temperatura);
                  
            Volver();
        }
        public async Task EliminarAsync()
        {
            if (_Sentemperatura.EliminarDatos(ID))
            {
                DisplayAlert("Eliminado", $"Se a Eliminado el componente Ventilador", "Aceptar");

            }
            else
                DisplayAlert("Eliminado", "Eliminado fallida", "Aceptar");
                  
            Volver();
        }

        public async Task Volver()
        {
            await Navigation.PushAsync(new Monitoreo());
        }
        #endregion


        #region Comandos
        public ICommand EditarModuloSensorcommand => new Command(() => Editar());
        public ICommand EliminarModuloSensorcommand => new Command(() => EliminarAsync());

        #endregion
    }
}