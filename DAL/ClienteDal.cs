using Entidades; //Invocar capa Entidades
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Net;

namespace DAL
{
    //Java : Herencia extends
    //C# : Herencia ":"

    /// <summary>
    /// Acceso a datos de clientes. En esta arquitectura el acceso a la BD es por API REST
    /// Se utilizará HTTPCLIENT en lugar de SQLCONNECTION
    /// </summary>
    public class ClienteDal : IClienteDal
    {
        //objetos privados
        private const string RUTA = "api/clientes";
        private readonly HttpClient _http;

        //Constructor (nombre de la Clase)
        public ClienteDal(HttpClient http)
        {
            _http = http;
        }
        public async Task ActualizarAsync(Cliente cliente, CancellationToken ct = default)
        {
            using var resp = await _http.PutAsJsonAsync($"{RUTA}/{cliente.Id}", ct);
            await AsegurarExitoAsync(resp, ct);
        }

        public async Task<Cliente?> CrearAsync(Cliente cliente, CancellationToken ct = default)
        {
            using var resp = await _http.PostAsJsonAsync(RUTA, cliente, ct);
            await AsegurarExitoAsync(resp, ct);
            return await resp.Content.ReadFromJsonAsync<Cliente>(ct); // Al registrar, API rest devuelve {...}
        }

        public async Task EliminarAsync(int id, CancellationToken ct = default)
        {
            using var resp = await _http.DeleteAsync($"{RUTA}/{id}", ct);
            await AsegurarExitoAsync(resp, ct);
        }

        /// <summary>
        /// Retornar una lista de clientes utilizando un filtro de maneara opcional
        /// </summary>
        /// <param name="buscar"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<List<Cliente>> ListarAsync(string? buscar, CancellationToken ct = default)
        {
            //¿Se nos envio datos en la variable buscar?
            var url = string.IsNullOrWhiteSpace(buscar) ? RUTA : $"{RUTA}?buscar={Uri.EscapeDataString(buscar)}";
            using var resp = await _http.GetAsync(url, ct);
            await AsegurarExitoAsync(resp, ct);
            
            return await resp.Content.ReadFromJsonAsync<List<Cliente>>(cancellationToken: ct) ?? new();
        }

        public async Task<Cliente?> ObtenerAsync(int id, CancellationToken ct)
        {
            //api/clientes/3
            using var resp = await _http.GetAsync($"{RUTA}/{id}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) { return null;  };

            //Es una busqueda por id, solo devolvemos un objeto
            return await resp.Content.ReadFromJsonAsync<Cliente>(ct);

        }

        private static async Task AsegurarExitoAsync(HttpResponseMessage resp, CancellationToken ct)
        {
            //Server Code 2XX
            if (resp.IsSuccessStatusCode) return;

            //Y si algo no funcionó?...
            var cuerpo = await resp.Content.ReadAsStringAsync(ct);
            var detalle = string.IsNullOrWhiteSpace(cuerpo) ? resp.ReasonPhrase : cuerpo;

            //Exception - ERROR (4XX, 5XX)
            throw new ApiException($"El servidor respondió: {(int)resp.StatusCode}: {detalle}");
        }
    }
}
