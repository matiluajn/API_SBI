using API_SBI.Entities;
using API_SBI.Entities.Dtos;
using API_SBI.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace API_SBI.Repository
{
    public class ServerPostRepository : IServerPost
    {
        private readonly IConfiguration _config;
        private ICollection<ServerPost> _serverPosts;
        
        public ServerPostRepository(IConfiguration config)
        {
            _config = config;          
            _serverPosts = new List<ServerPost>();

        }

        //GETERS DE API
        private string _UrlHost;
        public string UrlHost
        {
            get { return _UrlHost; }
            set { _UrlHost = value; }
        }
       
        private void ObtenerConfiguracionAPI()
        {
            _UrlHost = _config.GetSection("Keys:UrlHost").Value.ToString();    
            
        }
       

        public ICollection<ServerPost> GetAllServerPost()
        {
            _serverPosts=  GetServerPostAsync().Result;
            return _serverPosts;
        }

        public ServerPost GetServerPostById(int id)
        {
            _serverPosts = GetServerPostAsync().Result;
            return _serverPosts.Where(x=>x.id==id).FirstOrDefault();
        }

        public async Task<ICollection<ServerPost>> GetServerPostAsync()
        {
            // CONSTRUIMOS LA URL DE LA ACCIÓN
            ObtenerConfiguracionAPI();
            var url_ = _UrlHost;

            // INSTANCIAMOS EL HttpClient
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();

            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    ///////////////////////////////////////
                    // CONSTRUIMOS LA PETICIÓN (REQUEST) //
                    ///////////////////////////////////////
                    // DEFINIMOS EL MÉTODO HTTP
                    request_.Method = new HttpMethod("GET");

                    // DEFINIMOS LA URI
                    request_.RequestUri = new Uri(url_, System.UriKind.RelativeOrAbsolute);

                    // DEFINIMOS EL Accept, EN ESTE CASO ES "application/json"
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));




                    /////////////////////////////////////////
                    // CONSTRUIMOS LA RESPUESTA (RESPONSE) //
                    /////////////////////////////////////////
                    // Utilizamos ConfigureAwait(false) para evitar el DeadLock.
                    var response_ = await client.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                    // OBTENEMOS EL Content DEL RESPONSE como un String
                    // Utilizamos ConfigureAwait(false) para evitar el DeadLock.
                    var responseText_ = await response_.Content.ReadAsStringAsync().ConfigureAwait(false);

                    // SI ES LA RESPUESTA ESPERADA !! ...
                    if (response_.StatusCode == System.Net.HttpStatusCode.OK) // 200
                    {
                        // DESERIALIZAMOS Content DEL RESPONSE
                        return JsonConvert.DeserializeObject<ICollection<ServerPost>>(responseText_);
                        

                    }
                    else
                    // CUALQUIER OTRA RESPUESTA ...
                    if (response_.StatusCode != System.Net.HttpStatusCode.OK && // 200
                        response_.StatusCode != System.Net.HttpStatusCode.NoContent) // 204
                    {
                        throw new Exception((int)response_.StatusCode + ". No se esperaba el código de estado HTTP de la respuesta. " +
                            responseText_);                        
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inconveniente generado" + " " + ex);
                return new List<ServerPost>();

            }
            return new List<ServerPost>();

        }
    }
}
