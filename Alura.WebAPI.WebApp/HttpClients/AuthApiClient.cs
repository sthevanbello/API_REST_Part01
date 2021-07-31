﻿using Alura.ListaLeitura.Seguranca;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.HttpClients
{
    public class AuthApiClient
    {
        private readonly HttpClient _httpClient;

        public AuthApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> PostLoginAsync(LoginModel model)
        {
            var resposta = await _httpClient.PostAsJsonAsync("login", model);
            resposta.EnsureSuccessStatusCode();

            return await resposta.Content.ReadAsStringAsync();
        }
    }
}