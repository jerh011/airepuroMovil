using AirePuro.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirePuro.Simulacion
{
    internal class SenTepsim
    {

        List<MSenTemp> listaTemp = new List<MSenTemp>();
        public int Cont = -1;
        private static SenTepsim _instanciaAtreglo;

        public static SenTepsim Instancia
        {
            get
            {
                if (_instanciaAtreglo == null)
                {
                    _instanciaAtreglo = new SenTepsim();
                }
                return _instanciaAtreglo;
            }
        }

        public bool Insertar(MSenTemp _Tem)
        {
            Cont++;

            if (Cont <= 9)
            {
                listaTemp.Add(_Tem);
                return true;
            }
            else
                return false;


        }

        public void Actualizardatos(MSenTemp _Tem)
        {
            for (int i = 0; i <= Cont; i++)
            {
                if (listaTemp[i] != null && listaTemp[i].ID == _Tem.ID)
                {
                    listaTemp[i] = _Tem;
                    break;
                }
            }
        }

        public bool EliminarDatos(string ID)
        {
            for (int x = 0; x <= Cont; x++)
            {
                if (listaTemp[x].ID == ID)
                {
                    listaTemp.RemoveAt(x);
                    Cont--;
                    return true;
                }
            }
            return false;
        }

        public async Task<List<MSenTemp>> ObtenerAreglo()
        {

            return listaTemp;
        }
    }
}