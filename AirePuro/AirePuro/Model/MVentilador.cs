using System;
using System.Collections.Generic;
using System.Text;

namespace AirePuro.Model
{
    public class MVentilador
    {
     
        public string id { get; set; }
        public string idUsuario { get; set; }
        public int rpm { get; set; }
        public string ubicacion { get; set; }
        public bool encendido { get; set; }
        public string pinEnsendido { get; set; }
        public string pinRPM { get; set; }
    }
}