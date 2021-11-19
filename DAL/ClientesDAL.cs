using Minsait.Models;
using Minsait.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minsait.DAL
{
    public static class ClientesDAL
    {
        public static ClientesVM ObtenerCliente(ClientesVM _VM)
        {
            try
            {
                using (MinsaitEntities dataBaseContext = new MinsaitEntities())
                {
                    var cliente = (from c in dataBaseContext.Clientes
                                   where c.NombreCliente == _VM.NombreCliente &&
                                         c.IdPais == _VM.IdPais &&
                                         c.IdMercado == _VM.IdMercado &&
                                         c.IdentificadorFiscal == _VM.IdentificadorFiscal &&
                                         c.Email == _VM.Email
                                   select new ClientesVM
                                   {
                                       IdCliente = c.IdCliente,
                                       IdPais = c.IdPais,
                                       IdMercado = c.IdMercado,
                                       NombreCliente = c.NombreCliente,
                                       IdentificadorFiscal = c.IdentificadorFiscal,
                                       Email = c.Email
                                   }).FirstOrDefault<ClientesVM>();

                    return cliente;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //--------------------------------------------------------------------------------------------
        public static ClientesVM ObtenerClientexId(ClientesVM _VM)
        {
            try
            {
                using (MinsaitEntities dataBaseContext = new MinsaitEntities())
                {
                    var cliente = (from c in dataBaseContext.Clientes
                                   where c.IdCliente == _VM.IdCliente
                                   select new ClientesVM
                                   {
                                       IdCliente = c.IdCliente,
                                       IdPais = c.IdPais,
                                       IdMercado = c.IdMercado,
                                       NombreCliente = c.NombreCliente,
                                       IdentificadorFiscal = c.IdentificadorFiscal,
                                       Email = c.Email
                                   }).FirstOrDefault<ClientesVM>();

                    return cliente;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}