using Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IClienteDal
    {
        //Listar <Cliente> Entidad
        //se utilizara para consultar todos los datos | buscador
        Task<List<Cliente>> ListarAsync(string? buscar = null, CancellationToken ct = default);

        //Obtener
        Task<Cliente?> ObtenerAsync(int id, CancellationToken ct = default);

        //Crear
        Task<Cliente?> CrearAsync(Cliente cliente, CancellationToken ct = default);

        //Actualizar
        //No retorna valor, pero si define un metodo asincrono
        Task ActualizarAsync(Cliente cliente, CancellationToken ct = default);

        //Eliminar
        Task EliminarAsync(int id, CancellationToken ct = default);
    }
}
