using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netby.citas.negocio.Utilitarios
{
    public class CitasException : Exception
    {


        public int? StatusCodeHttp { get; set; }
        public string Code { get; set; }

        public CitasException() : base() { }

        public CitasException(string message) : base(message) { }

        public CitasException(string message, int statusCodeHttp) : base(message)
        {

            StatusCodeHttp = statusCodeHttp;
        }

        public CitasException(string message, string code) : base(message)
        {
            Code = code;
        }
    }
}
