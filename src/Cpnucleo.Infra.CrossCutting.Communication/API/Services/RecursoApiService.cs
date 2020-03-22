﻿using Cpnucleo.Infra.CrossCutting.Communication.API.Interfaces;
using Cpnucleo.Infra.CrossCutting.Util.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cpnucleo.Infra.CrossCutting.Communication.API.Services
{
    public class RecursoApiService : BaseApiService<RecursoViewModel>, IRecursoApiService
    {
        private const string actionRoute = "recurso";

        public async Task<bool> IncluirAsync(string token, RecursoViewModel obj)
        {
            return await PostAsync(token, actionRoute, obj);
        }

        public async Task<IEnumerable<RecursoViewModel>> ListarAsync(string token)
        {
            return await GetAsync(token, actionRoute);
        }

        public async Task<RecursoViewModel> ConsultarAsync(string token, Guid id)
        {
            return await GetAsync(token, actionRoute, id);
        }

        public async Task<bool> RemoverAsync(string token, Guid id)
        {
            return await DeleteAsync(token, actionRoute, id);
        }

        public async Task<bool> AlterarAsync(string token, RecursoViewModel obj)
        {
            return await PutAsync(token, actionRoute, obj.Id, obj);
        }

        public async Task<RecursoViewModel> AutenticarAsync(string login, string senha)
        {
            try
            {
                RestRequest request = new RestRequest($"api/v2/{actionRoute}/autenticar", Method.GET);
                request.AddQueryParameter("login", login);
                request.AddQueryParameter("senha", senha);

                IRestResponse response = await _client.ExecuteAsync(request);

                return JsonConvert.DeserializeObject<RecursoViewModel>(response.Content);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}