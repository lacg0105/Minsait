using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minsait_MVC.Models.ViewModel
{
    public class ClientesVM
    {
        public int IdCliente { get; set; }
        public int IdPais { get; set; }
        public int IdMercado { get; set; }
        public string NombreCliente { get; set; }
        public string IdentificadorFiscal { get; set; }
        public string Email { get; set; }
        public string NombrePais { get; set; }
        public string NombreMercado { get; set; }
    }
}