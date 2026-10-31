namespace Entidades
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string Ruc { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}
