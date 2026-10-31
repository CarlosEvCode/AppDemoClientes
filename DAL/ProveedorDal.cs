using Entidades;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Net;

namespace DAL
{
    public class ProveedorDal : IProveedorDal
    {
        private const string RUTA = "api/proveedores";
        private readonly HttpClient _http;

        public ProveedorDal(HttpClient http)
        {
            _http = http;
        }

        public async Task ActualizarAsync(Proveedor proveedor, CancellationToken ct = default)
        {
            using var resp = await _http.PutAsJsonAsync($"{RUTA}/{proveedor.Id}", proveedor, ct);
            await AsegurarExitoAsync(resp, ct);
        }

        public async Task<Proveedor?> CrearAsync(Proveedor proveedor, CancellationToken ct = default)
        {
            using var resp = await _http.PostAsJsonAsync(RUTA, proveedor, ct);
            await AsegurarExitoAsync(resp, ct);
            return await resp.Content.ReadFromJsonAsync<Proveedor>(ct);
        }

        public async Task EliminarAsync(int id, CancellationToken ct = default)
        {
            using var resp = await _http.DeleteAsync($"{RUTA}/{id}", ct);
            await AsegurarExitoAsync(resp, ct);
        }

        public async Task<List<Proveedor>> ListarAsync(string? buscar, CancellationToken ct = default)
        {
            var url = string.IsNullOrWhiteSpace(buscar) ? RUTA : $"{RUTA}?buscar={Uri.EscapeDataString(buscar)}";
            using var resp = await _http.GetAsync(url, ct);
            await AsegurarExitoAsync(resp, ct);

            return await resp.Content.ReadFromJsonAsync<List<Proveedor>>(cancellationToken: ct) ?? new();
        }

        public async Task<Proveedor?> ObtenerAsync(int id, CancellationToken ct)
        {
            using var resp = await _http.GetAsync($"{RUTA}/{id}", ct);
            if (resp.StatusCode == HttpStatusCode.NotFound) { return null; };

            return await resp.Content.ReadFromJsonAsync<Proveedor>(ct);
        }

        private static async Task AsegurarExitoAsync(HttpResponseMessage resp, CancellationToken ct)
        {
            if (resp.IsSuccessStatusCode) return;

            var cuerpo = await resp.Content.ReadAsStringAsync(ct);
            var detalle = string.IsNullOrWhiteSpace(cuerpo) ? resp.ReasonPhrase : cuerpo;

            throw new ApiException($"El servidor respondió: {(int)resp.StatusCode}: {detalle}");
        }
    }
}
