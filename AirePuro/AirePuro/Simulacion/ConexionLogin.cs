using AirePuro.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Xamarin.Essentials.Permissions;

namespace AirePuro.Simulacion
{
    public class ConexionLogin
    {
        private string api_url = "http://www.apiaire.somee.com/api/Usuario";
        static HttpClient client = new HttpClient();
        private string valido;
        private bool resultado;
        private MUsuario _Perfil = new MUsuario();


        public async Task<string> Registrar(MUsuario _MUsuario)
        {
            try
            {
              
                //valida la si ya existe usuario o numero registrado
                HttpResponseMessage validacion = await client.GetAsync($"{api_url}/validadar_usuario/{_MUsuario.Cuenta}/{_MUsuario.Numero}");
                if (validacion.IsSuccessStatusCode)
                {

                    valido = await validacion.Content.ReadAsStringAsync();

                    //convierte el resultado en booleano 
                    bool.TryParse(valido, out resultado);
                    if (!resultado)
                    {
                        _MUsuario.Id = "1";
                        var json = JsonConvert.SerializeObject(_MUsuario);
                        var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(api_url, contentJson);


                        if (response.StatusCode == HttpStatusCode.Created)
                            return "Usuario creado exitosamente";
                        else
                            return "Algo salio mal registro fallido compruebe la conexion";
                    }
                    else
                        return "El usuario o numero ya se encuentre en uso";
                }
                else
                    return "Algo salio mal registro fallido compruebe la conexion";

            }
            catch (Exception ex)
            {

                return "Algo salio mal registro fallido compruebe la conexion";
            }
        }


        // Método para obtener un arreglo de usuarios desde la API
        // Descomenta y completa según tus necesidades

        public async Task<MUsuario> Logearse(string cuenta, string contraseña)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{api_url}/Iniciarsesion/{cuenta}/{contraseña}");

                if (response.IsSuccessStatusCode)
                {
                   
                    string json = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<MUsuario>(json);
                }
                else
                {
                   
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}