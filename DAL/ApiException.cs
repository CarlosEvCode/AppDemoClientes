using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    //Subclase de Exception (superclase)
    public class ApiException : Exception
    {
        //Constructor (metodo inicializador)
        //Anulando el constructor predeterminado vacio public Clase() {}
        //base(mensaje)
        public ApiException(string mensaje) : base(mensaje)
        {
        }
    }
}
