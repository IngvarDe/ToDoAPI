﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoApi.Models;

namespace ToDoApi.DataServices
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService
            (

            )
        {
            _httpClient = new HttpClient();
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.2.2:5209" : "https://localhost:7209";
            _url = $"{_baseAddress}/api";
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }



        public async Task AddTodoAsync(ToDo toDo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Check the Internet Connectivity!");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize<ToDo>(toDo, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/todo", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created ToDo!!!");
                }
                else
                {
                    Debug.WriteLine("Non http request 2xx response");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Sorry, {ex.Message}");
            }

            return;
        }

        public async Task DeleteTodoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Check the Internet Connectivity!");
                return;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/todo/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created ToDo!!!");
                }
                else
                {
                    Debug.WriteLine("Non http request 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Sorry, {ex.Message}");
            }

            return;
        }

        public async Task<List<ToDo>> GetAllToDosAsync()
        {
            List<ToDo> todos = new List<ToDo>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("Check the Internet Connectivity!");
                return todos;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todo");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    todos = JsonSerializer.Deserialize<List<ToDo>>(content, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("Non http request 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Sorry, {ex.Message}");
            }
            return todos;
        }

        public async Task UpdateToDoAsync(ToDo toDo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {
                string jsonToDo = JsonSerializer.Serialize<ToDo>(toDo, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/todo/{toDo.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created ToDo");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return;
        }
    }
}
