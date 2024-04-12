using System;
using System.Collections.Generic;
using System.Text;

namespace AirePuro.Model
{
    public class MUsuario
    {
       
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public string Numero { get; set; } = string.Empty;
    }
}
