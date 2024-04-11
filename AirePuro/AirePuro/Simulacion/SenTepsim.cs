using AirePuro.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AirePuro.Simulacion
{
    internal class SenTepsim
    {

        List<MSenTemp> listaTemp = new List<MSenTemp>();
        private static SenTepsim _instanciaAtreglo;
        private static Ventiladoressim _instanciaVentiladior;

        private string api_url = "https://1p9p726s-5031.usw3.devtunnels.ms/api/SensorTemperatura";
        static HttpClient client = new HttpClient();
        int Cont=-1;
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
                if (listaTemp[i] != null && listaTemp[i].id == _Tem.id)
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
                if (listaTemp[x].id == ID)
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
            string Temperatura = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(api_url);
                if (response.IsSuccessStatusCode)
                {

                    Temperatura = await response.Content.ReadAsStringAsync();
                    listaTemp = JsonConvert.DeserializeObject<List<MSenTemp>>(Temperatura);

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

          
            return listaTemp;
        }
    }
}