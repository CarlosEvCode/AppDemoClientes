using System;
using System.Collections.Generic;
using System.Text;

namespace BOL
{
    /// <summary>
    /// Lanza los datos cuando no cumplen una regla de negocio
    /// </summary>
    public class ReglaNegocioException : Exception
    {
        /// <summary>
        /// Campo nombre de la propiedad que genera una excepcion (problema)
        /// Ejemplo ("Dni") la UI identifique el problema
        /// </summary>
        public string? Campo { get; }

        //Constructor
        public ReglaNegocioException(string mensaje, string? campo = null) : base(mensaje)
        {
            Campo = campo;
        }
    }
}
