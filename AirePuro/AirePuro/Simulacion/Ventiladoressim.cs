﻿using AirePuro.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AirePuro.Views.Pantalla;
using System.Linq;

namespace AirePuro.Simulacion
{
    internal class Ventiladoressim
    {
        // private MVentilador[] _Venti=new MVentilador[11];
        private List<MVentilador> listaVenti = new List<MVentilador>();
      
        private static Ventiladoressim _instanciaVentiladior;
        private string api_url = "https://1p9p726s-5031.usw3.devtunnels.ms/api/Ventilador";
        static HttpClient client = new HttpClient();
        public static Ventiladoressim Instancia
        {
            get
            {
                if (_instanciaVentiladior == null)
                {
                    _instanciaVentiladior = new Ventiladoressim();
                }
                return _instanciaVentiladior;
            }
        }

        public async Task<bool> Insertar(MVentilador _ventilador)
        {
                try
                {
                    
                    // Si no existe, agregar el ventilador a la lista
                    listaVenti.Add(_ventilador);

                    // Lógica para insertar en el servidor
                    var json = JsonConvert.SerializeObject(_ventilador);
                    var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(api_url, contentJson);
                    if (response.StatusCode == HttpStatusCode.Created)
                        return true;
                    else
                        return false;
                }
                catch (Exception ex)
                {
                    // Manejo de errores aquí
                    Console.WriteLine($"Error al insertar ventilador: {ex.Message}");
                    return false;
                }
        }
      
        public async Task<List<MVentilador>> ObtenerAreglo()
        {

            string Ventilor=null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(api_url);
                if (response.IsSuccessStatusCode)
                {
                   
                    Ventilor = await response.Content.ReadAsStringAsync();
                    listaVenti = JsonConvert.DeserializeObject<List<MVentilador>>(Ventilor);

                }
                else
                {
                    // Console.WriteLine("Error al realizar la petición HTTP");
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine(Ventilor);
            return listaVenti;
        }
        public async Task<bool> Actualizardatos(MVentilador _ventilador)
        {

            Uri RequestUri = new Uri(api_url + $"/VentiladorToUpdateXamarin/{_ventilador.id}/{_ventilador.ubicacion}/{_ventilador.pinRPM}/{_ventilador.pinEnsendido}");
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
                HttpResponseMessage response = await client.DeleteAsync($"{api_url}/borrrar/{ID}");
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

    }
}