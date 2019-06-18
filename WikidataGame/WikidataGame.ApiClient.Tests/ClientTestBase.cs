﻿using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WikidataGame.Models;

namespace WikidataGame.ApiClient.Tests
{
    public abstract class ClientTestBase
    {
        public const string BaseUrl = "http://localhost:54264/"; //"https://wikidatagame.azurewebsites.net";


        protected async Task<AuthInfo> RetrieveBearerAsync()
        {
            var apiClient = new WikidataGameAPI(new Uri(BaseUrl), new TokenCredentials("auth"));
            return await apiClient.AuthenticateAsync(Guid.NewGuid().ToString(), string.Empty);
        }
    }
}
