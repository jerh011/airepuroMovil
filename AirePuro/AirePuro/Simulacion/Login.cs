using AirePuro.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AirePuro.Simulacion
{
    public class Login
    {
        private string api_url = "https://1p9p726s-5031.usw3.devtunnels.ms/api/Ventilador";
        static HttpClient client = new HttpClient();

        private MUsuario _Usuario;
        public async Task<bool> Insertar(MUsuario _Usuario)
        {
            try
            {
              
                var json = JsonConvert.SerializeObject(_Usuario);
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(api_url, contentJson);
                if (response.StatusCode == HttpStatusCode.Created)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<MVentilador> ObtenerAreglo()
        {

            string Ventilor = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(api_url);
                if (response.IsSuccessStatusCode)
                {

                    Ventilor = await response.Content.ReadAsStringAsync();
                    listaVenti = JsonConvert.DeserializeObject<List<MVentilador>>(Ventilor);

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine(Ventilor);
            return listaVenti;
        }


    }
}
