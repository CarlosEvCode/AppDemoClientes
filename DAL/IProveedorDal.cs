using Entidades;

namespace DAL
{
    public interface IProveedorDal
    {
        Task<List<Proveedor>> ListarAsync(string? buscar = null, CancellationToken ct = default);

        Task<Proveedor?> ObtenerAsync(int id, CancellationToken ct = default);

        Task<Proveedor?> CrearAsync(Proveedor proveedor, CancellationToken ct = default);

        Task ActualizarAsync(Proveedor proveedor, CancellationToken ct = default);

        Task EliminarAsync(int id, CancellationToken ct = default);
    }
}
