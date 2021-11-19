using Minsait.Models;
using Minsait.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Minsait.DAL;

namespace Minsait.Controllers
{
    public class ClientesController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Add(ClientesVM _VM)
        {
            using (MinsaitEntities dataBaseContext = new MinsaitEntities())
            {
                var Cliente = ClientesDAL.ObtenerCliente(_VM);

                if (Cliente == null)
                {
                    Clientes NuevoCliente = new Clientes()
                    {
                        IdCliente = _VM.IdCliente,
                        IdPais = _VM.IdPais,
                        IdMercado = _VM.IdMercado,
                        NombreCliente = _VM.NombreCliente,
                        IdentificadorFiscal = _VM.IdentificadorFiscal,
                        Email = _VM.Email
                    };
                    dataBaseContext.Clientes.Add(NuevoCliente);
                    dataBaseContext.SaveChanges();
                    return Ok("El cliente se registró con éxito.");
                }
                else
                    return Ok("El registro del cliente ya existe.");

            }
        }
        //--------------------------------------------------------------------------------------------
        [HttpGet]
        public IHttpActionResult ObtenerClientexId(ClientesVM _VM)
        {
            using (MinsaitEntities dataBaseContext = new MinsaitEntities())
            {
                var Cliente = ClientesDAL.ObtenerClientexId(_VM);

                if (Cliente == null)
                    return Ok("El cliente no está registrado.");
                else
                    return Ok(Cliente);

            }
            //return Ok("Ok");
        }
    }
}
