﻿using HRLeaveManagement.MVC.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Services.Base
{
    public class BaseHttpService
    {
        protected IClient _client;
        private readonly ILocalStorageService _localStorage;

        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        protected Response<Guid> ConverApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new Response<Guid>()
                {
                    Message = "Validation error have occured.",
                    ValidationErrors = ex.Response,
                    Success = false
                };
            }
            else if (ex.StatusCode == 404)
            {
                return new Response<Guid>()
                {
                    Message = "The requested item could not be found",
                    Success = false
                };
            }
            else
            {
                return new Response<Guid>()
                {
                    Message = "Something went wrong, please try again.",
                    Success = false
                };
            }
        }

        protected void AddBearerToken()
        {
            if (_localStorage.Exists("token"))
                _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        _localStorage.GetStorageValue<string>("token")
                    );
        }
    }
}
