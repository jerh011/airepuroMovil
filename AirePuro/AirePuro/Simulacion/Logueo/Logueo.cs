using AirePuro.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AirePuro.Simulacion.Logueo
{
    public class Logueo
     {
        // private MVentilador[] _Venti=new MVentilador[11];
        private MUsuario _Usuario = new MUsuario();
      
        private static Logueo _instanciaLogin;

        public static Logueo Instancia
        {
            get
            {
                if (_instanciaLogin == null)
                {
                    _instanciaLogin = new Logueo();
                }
                return _instanciaLogin;
            }
        }

        public async Task Insertar(MUsuario _MUsuario)
        {
            _Usuario = _MUsuario;
        }
      
        public async Task <string> OpteneteUsuari()
        {

            return _Usuario.Id;
        }

        

    }
}