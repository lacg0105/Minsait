using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Minsait_MVC.Models.ViewModel;
using Newtonsoft.Json;

namespace Minsait_MVC.Controllers
{
    public class ContactoClientesController : Controller
    {
        public ActionResult ContactoClientes()
        {
            return View();
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ObtenerContactoClientes()
        {
            string strUrl = "https://localhost:44350/Api/ContactoClientes/ObtenerContactoClientes";
            var obj = Get(strUrl, "GET");
            if (obj.Contains("No hay clientes registrados"))
                return Json("0");

            List<ContactoClientesVM> lst = JsonConvert.DeserializeObject<List<ContactoClientesVM>>(obj);
            var jsonResult = Json(lst, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        //--------------------------------------------------------------------------------------------
        public string Get(string url, string method = "GET")
        {
            string result = "";

            try
            {
                //peticion
                WebRequest request = WebRequest.Create(url);
                //headers
                request.Method = method;
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8'";
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

            }
            catch (Exception e)
            {
                result = e.Message;

            }

            return result;
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ContactoClientesChanges(string IdContactoCliente)
        {
            ViewBag.Title = "Contacto Clientes";
            #region Recuperamos los datos
            if (IdContactoCliente == null)
            {
                ViewBag.Mov = "NUEVO";
                return View(new ContactoClientesVM());
            }
            else
            {
                string url = "https://localhost:44350/Api/ContactoClientes/ObtenerContactoClientexId?IdContactoCliente=" + IdContactoCliente;
                var obj = Send<string>(url, IdContactoCliente, "POST");
                var objContacto = JsonConvert.DeserializeObject<ContactoClientesVM>(obj);
                ViewBag.Mov = "EDITAR";
                return View(objContacto);
            }

            #endregion
        }
        //--------------------------------------------------------------------------------------------
        public string Send<T>(string url, T objectRequest, string method)
        {
            string result = "";

            try
            {

                JavaScriptSerializer js = new JavaScriptSerializer();

                //serializamos el objeto
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(objectRequest);

                //peticion
                WebRequest request = WebRequest.Create(url);
                //headers
                request.Method = method;
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8'";
                //request.Timeout = 10000; //esto es opcional

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

            }
            catch (Exception e)
            {
                return "0";

            }

            return result;
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ObtenerClientes()
        {
            string strUrl = "https://localhost:44350/Api/Clientes/ObtenerCarteraClientes";
            var obj = Get(strUrl, "GET");
            List<ClientesVM> lst = JsonConvert.DeserializeObject<List<ClientesVM>>(obj);
            var jsonResult = Json(lst, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult CrearContactoCliente(ContactoClientesVM _contactoVM)
        {
            string strUrl = "https://localhost:44350/Api/ContactoClientes/AddContacto";
            string resultado = Send<ContactoClientesVM>(strUrl, _contactoVM, "POST");
            var jsonResult = Json(resultado);
            return jsonResult;
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult EditarContactoCliente(ContactoClientesVM _contactoVM)
        {
            string strUrl = "https://localhost:44350/Api/ContactoClientes/ModificarContactoCliente";
            string resultado = Send<ContactoClientesVM>(strUrl, _contactoVM, "PUT");
            var jsonResult = Json(resultado);
            return jsonResult;
        }
        //--------------------------------------------------------------------------------------------
    }
}
