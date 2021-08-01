using Alura.ListaLeitura.Seguranca;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.HttpClients
{
    public class AuthApiClient
    {
        public class LoginResult
        {
            public bool Succeeded { get; set; }
            public string Token { get; set; }
        }

        private readonly HttpClient _httpClient;

        public AuthApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        public async Task<LoginResult> PostLoginAsync(LoginModel model)
        {
            var resposta = await _httpClient.PostAsJsonAsync("login", model);

            return new LoginResult
            {
                Succeeded = resposta.IsSuccessStatusCode,
                Token = await resposta.Content.ReadAsStringAsync()
            };

        }
    }
}