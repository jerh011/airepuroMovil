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
        private string api_url = "https://1p9p726s-5031.usw3.devtunnels.ms/api/Ventilador";
        static HttpClient client = new HttpClient();
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