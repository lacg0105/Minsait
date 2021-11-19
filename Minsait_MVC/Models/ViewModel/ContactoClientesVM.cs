using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minsait_MVC.Models.ViewModel
{
    public class ContactoClientesVM
    {
        public int IdContactoCliente { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string NumeroContacto { get; set; }
        public string PuestoContacto { get; set; }
        public string CorreoContacto { get; set; }
        public string NombreContacto { get; set; }
    }
}