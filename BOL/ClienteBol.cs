using System;
using System.Collections.Generic;
using System.Text;
using Entidades;
using DAL;
using System.Text.RegularExpressions;

namespace BOL
{
    /// <summary>
    /// Lógica de negocio para clientes
    /// La UI solo puede conversar con esta clases
    /// </summary>
    public class ClienteBol
    {
        //Utilizaremos la interfaz para conocer los metodos a aplicar
        private readonly IClienteDal _dal;
        public ClienteBol(IClienteDal dal) => _dal = dal;
        /// <summary>
        /// Este metodo sirve la accion LISTAR para la UI, tomando como referencia la clase de acceso a datos (DAL) y su metodo ListarAsync
        /// </summary>
        /// <param name="buscar"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<List<Cliente>> ListarAsync(string? buscar = null, CancellationToken ct = default) => _dal.ListarAsync(buscar?.Trim(), ct);
        
        /// <summary>
        /// Buscador de cliente por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public Task<Cliente?> ObtenerPorIdAsync(int id, CancellationToken ct = default) => _dal.ObtenerAsync(id, ct);

        public async Task GuardarAsync(Cliente cliente, CancellationToken ct = default)
        {
            //Validacion
            Normalizar(cliente);    //Quitar los espacios en blanco (sanitizar)
            Validar(cliente);

            //La UI nos envio un ID = 0 (NUEVO)
            //La UI nos envio un ID = 7 (ACTUALIZAR)
            if (cliente.Id == 0) await _dal.CrearAsync(cliente, ct);
            else await _dal.ActualizarAsync(cliente, ct);
        }

        public Task EliminarAsync(int id, CancellationToken ct = default)
        {
            //En el UI manejaremos un ID = 0 (No hay registro activo)
            if (id == 0) throw new ReglaNegocioException("Selecciona un registro para eliminar");
            return _dal.EliminarAsync(id, ct);
        }

        private static void Normalizar(Cliente cliente)
        {
            cliente.Apellidos = cliente.Apellidos.Trim();
            cliente.Nombres = cliente.Nombres.Trim();
            cliente.Dni = cliente.Dni.Trim();
            cliente.Telefono = cliente.Telefono.Trim();
            cliente.Direccion = cliente.Direccion.Trim();
            cliente.Correo = cliente.Correo.Trim();
        }

        private static void Validar(Cliente cliente)
        {
            if (cliente.Apellidos.Length == 0) { throw new ReglaNegocioException("Ingresa los apellidos", nameof(Cliente.Apellidos)); }
            if (cliente.Nombres.Length == 0) { throw new ReglaNegocioException("Ingresa los nombres", nameof(Cliente.Nombres)); }
            if (cliente.Dni.Length != 8) { throw new ReglaNegocioException("Escriba correctamente el DNI", nameof(Cliente.Dni)); }
            
            if (!Regex.IsMatch(cliente.Dni,@"^\d{8}$")) { throw new ReglaNegocioException("El DNI debe contener 8 dígitos", nameof(Cliente.Dni)); }

            if (cliente.Telefono.Length != 9) { throw new ReglaNegocioException("El teléfono debe contener 9 dígitos", nameof(Cliente.Telefono)); }
            if (cliente.Direccion.Length == 0) { throw new ReglaNegocioException("Ingresa la dirección", nameof(Cliente.Direccion)); }
            if (cliente.Correo.Length == 0) { throw new ReglaNegocioException("Ingresa el correo", nameof(Cliente.Correo)); }
        }
    }
}
