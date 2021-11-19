using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Minsait_MVC.Helpers;
using Minsait_MVC.Models.ViewModel;
using Newtonsoft.Json;

namespace Minsait_MVC.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Clientes()
        {
            return View();
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ClienteChanges(string IdCliente)
        {
            ViewBag.Title = "Cartera Clientes";
            #region Recuperamos los datos
            if (IdCliente == null)
            {
                ViewBag.Mov = "NUEVO";
                return View(new ClientesVM());
            }
            else
            {
                string url = "https://localhost:44350/Api/Clientes/ObtenerClientexId?IdCliente=" + IdCliente;
                var obj = Send<string>(url, IdCliente, "POST");
                var objCliente = JsonConvert.DeserializeObject<ClientesVM>(obj);
                ViewBag.Mov = "EDITAR";
                return View(objCliente);
            }
            
            #endregion
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ObtenerCarteraClientes()
        {
            string strUrl = "https://localhost:44350/Api/Clientes/ObtenerCarteraClientes";
            var obj = Get(strUrl, "GET");
            if (obj.Contains("No hay clientes registrados"))
                return Json("0");

            List<ClientesVM> lst = JsonConvert.DeserializeObject<List<ClientesVM>>(obj);
            var jsonResult = Json(lst, JsonRequestBehavior.AllowGet);
            return jsonResult;
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
        public ActionResult ObtenerPaises()
        {
            string strUrl = "https://localhost:44350/Api/Clientes/ObtenerPaises";
            var obj = Get(strUrl, "GET");
            List<CatPaisesVM> lst = JsonConvert.DeserializeObject<List<CatPaisesVM>>(obj);
            var jsonResult = Json(lst, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult ObtenerMercados()
        {
            string strUrl = "https://localhost:44350/Api/Clientes/ObtenerMercados";
            var obj = Get(strUrl, "GET");
            List<CatMercadosVM> lst = JsonConvert.DeserializeObject<List<CatMercadosVM>>(obj);
            var jsonResult = Json(lst, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult CrearCliente(ClientesVM _clienteVM)
        {
            string strUrl = "https://localhost:44350/Api/Clientes/AddCliente";
            string resultado = Send<ClientesVM>(strUrl, _clienteVM, "POST");
            var jsonResult = Json(resultado);
            return jsonResult;
        }
        //--------------------------------------------------------------------------------------------
        public ActionResult EditarCliente(ClientesVM _clienteVM)
        {
            string strUrl = "https://localhost:44350/Api/Clientes/ModificarCliente";
            string resultado = Send<ClientesVM>(strUrl, _clienteVM, "PUT");
            var jsonResult = Json(resultado);
            return jsonResult;
        }
        //--------------------------------------------------------------------------------------------
    }
}