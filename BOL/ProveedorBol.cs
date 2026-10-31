using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using DAL;
using System.Text.RegularExpressions;

namespace BOL
{
    public class ProveedorBol
    {
        private readonly IProveedorDal _dal;
        public ProveedorBol(IProveedorDal dal) => _dal = dal;

        public Task<List<Proveedor>> ListarAsync(string? buscar = null, CancellationToken ct = default) => _dal.ListarAsync(buscar?.Trim(), ct);

        public Task<Proveedor?> ObtenerPorIdAsync(int id, CancellationToken ct = default) => _dal.ObtenerAsync(id, ct);

        public async Task GuardarAsync(Proveedor proveedor, CancellationToken ct = default)
        {
            Normalizar(proveedor);
            Validar(proveedor);

            if (proveedor.Id == 0) await _dal.CrearAsync(proveedor, ct);
            else await _dal.ActualizarAsync(proveedor, ct);
        }

        public Task EliminarAsync(int id, CancellationToken ct = default)
        {
            if (id == 0) throw new ReglaNegocioException("Selecciona un registro para eliminar");
            return _dal.EliminarAsync(id, ct);
        }

        private static void Normalizar(Proveedor proveedor)
        {
            proveedor.RazonSocial = proveedor.RazonSocial.Trim();
            proveedor.Ruc = proveedor.Ruc.Trim();
            proveedor.Telefono = proveedor.Telefono.Trim();
            proveedor.Direccion = proveedor.Direccion.Trim();
            proveedor.Correo = proveedor.Correo.Trim();
        }

        private static void Validar(Proveedor proveedor)
        {
            if (proveedor.RazonSocial.Length == 0) { throw new ReglaNegocioException("Ingresa la razón social", nameof(Proveedor.RazonSocial)); }

            if (proveedor.Ruc.Length != 11) { throw new ReglaNegocioException("Escriba correctamente el RUC", nameof(Proveedor.Ruc)); }
            if (!Regex.IsMatch(proveedor.Ruc, @"^\d{11}$")) { throw new ReglaNegocioException("El RUC debe contener 11 dígitos", nameof(Proveedor.Ruc)); }

            if (proveedor.Telefono.Length != 9) { throw new ReglaNegocioException("El teléfono debe contener 9 dígitos", nameof(Proveedor.Telefono)); }
            if (proveedor.Direccion.Length == 0) { throw new ReglaNegocioException("Ingresa la dirección", nameof(Proveedor.Direccion)); }
            if (proveedor.Correo.Length == 0) { throw new ReglaNegocioException("Ingresa el correo", nameof(Proveedor.Correo)); }
        }
    }
}
