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
    internal class HistorialConexion
    {

        List<MHistorial> listaTemp = new List<MHistorial>();
        private static HistorialConexion _instanciaAtreglo;
     

        private string api_url = "https://rklkgjd6-5031.usw3.devtunnels.ms/api/Vhistorial";
        static HttpClient client = new HttpClient();
        int Cont=-1;
        public static HistorialConexion Instancia
        {
            get
            {
                if (_instanciaAtreglo == null)
                {
                    _instanciaAtreglo = new HistorialConexion();
                }
                return _instanciaAtreglo;
            }
        }

        public async Task<List<MHistorial>> ObtenerAreglo(string ID)
        {
            string Temperatura = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(api_url+ $"/OptenerhitorialbyUsuario/{ID}");
                if (response.IsSuccessStatusCode)
                {

                    Temperatura = await response.Content.ReadAsStringAsync();
                    listaTemp = JsonConvert.DeserializeObject<List<MHistorial>>(Temperatura);

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

          
            return listaTemp;
        }
        public async Task<bool> agregarRegistro(string ID)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync(api_url + $"?Idusuario={ID}", null); 
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}