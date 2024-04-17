using AirePuro.Views.Pantalla;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirePuro.Model
{
    public class MHistorial
    {
       
        public string Id { get; set; } = string.Empty;

     
        public string Titulo { get; set; } = string.Empty;
       
        public string IdUsuario { get; set; } = string.Empty;
       
        public DateTime Fecha { get; set; }

      
        public string Hora { get; set; } = string.Empty;

        
        public MSenTemp SensorTemperatura { get; set; }
    
        public MVentilador Ventilador { get; set; }
    }
}
