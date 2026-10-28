using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Apellidos { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}
