using AirePuro.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
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

        public async Task<bool> InsertarAsync(MSenTemp _Tem)
        {

            try
            {
              
                _Tem.id = "1";
                var json = JsonConvert.SerializeObject(_Tem);
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(api_url, contentJson);
                if (response.StatusCode == HttpStatusCode.Created)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string si=ex.Message;

                return false;
            }


        }

        public async Task<bool>  Actualizardatos(MSenTemp _Tem)
        {

            Uri RequestUri = new Uri(api_url + $"/TemperaturaToUpdateXamarin/{_Tem.id}/{_Tem.ubicacion}/{_Tem.pinDatos}");
            var client = new HttpClient();

            var response = await client.PutAsync(RequestUri, null);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
     
        public async Task<bool> EliminarDatos(string ID)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"{api_url}/Borrar/{ID}");
                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<MSenTemp>> ObtenerAreglo(string ID)
        {
            string Temperatura = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(api_url+ $"/TemperaturaByIdUsuario/{ID}");
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