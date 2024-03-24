using AirePuro.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirePuro.Simulacion
{
    internal class SenGazSim
    {
        // private MVentilador[] _Venti=new MVentilador[11];
        private List<MSenGaz> listaGaz = new List<MSenGaz>();
        private int Cont = -1;
        private static SenGazSim _instanciaGaz;

        public static SenGazSim Instancia
        {
            get
            {
                if (_instanciaGaz == null)
                {
                    _instanciaGaz = new SenGazSim();
                }
                return _instanciaGaz;
            }
        }

        public async Task<bool> Insertar(MSenGaz _ventilador)
        {
            Cont++;

            if (Cont <= 9)
            {
                listaGaz.Add(_ventilador);
                return true;
            }
            else
                return false;


        }

        public async Task<bool>  Actualizardatos(MSenGaz _ventilador)
        {
            for (int i = 0; i <= Cont; i++)
            {
                if (listaGaz[i] != null && listaGaz[i].id == _ventilador.id)
                {
                    listaGaz[i] = _ventilador;
                   return  true;
                    break;
                }
            }
            return false;
        }

        public async Task<bool> EliminarDatos(string ID)
        {
            for (int x = 0; x <= Cont; x++)
            {
                if (listaGaz[x].id == ID)
                {
                    listaGaz.RemoveAt(x);
                    Cont--;
                    return true;
                }
            }
            return false;
        }

        public async Task<List<MSenGaz>> ObtenerAreglo()
        {

            return listaGaz;
        }
    }
}