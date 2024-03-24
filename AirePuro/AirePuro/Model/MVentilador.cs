using System;
using System.Collections.Generic;
using System.Text;

namespace AirePuro.Model
{
    public class MVentilador
    {
     
        public string id { get; set; } = string.Empty;


        public int rpm { get; set; }

        public string ubicacion { get; set; } = string.Empty;
    
        public bool encendido { get; set; }

     
        public string pinEnsendido { get; set; }

  
        public string pinRPM { get; set; }
    }
}