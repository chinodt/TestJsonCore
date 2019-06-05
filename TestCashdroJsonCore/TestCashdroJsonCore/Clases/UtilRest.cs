using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Net;
using System.IO;
using TestCashdroJsonCore.Enums;
using TestCashdroJsonCore.Implementation;
using Newtonsoft.Json;

namespace TestCashdroJsonCore.Clases
{
    public static class UtilRest
    {
        public static ResponseRestDM<TResultado> InvocaServicioRest<TResultado>
                                                    (string url, string element)
        {
            var respuesta = new ResponseRestDM<TResultado>() { };
            TResultado resultado = default(TResultado);
            string sMessage = "";
            CashdroEnum sCode = 0;

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            var responses = (HttpWebResponse)request.GetResponse();
            var response = new StreamReader(responses.GetResponseStream()).ReadToEnd();
            dynamic resul = JsonConvert.DeserializeObject<dynamic>(response);
            sCode = (CashdroEnum)resul["code"];
            sMessage = "";

            if (sCode != CashdroEnum.OK)
            {
                sMessage = resul["data"];

            }
            else
            {
                //Este devuelve un Newtonsoft.Json.Linq.JObject
                //var test = JsonConvert.DeserializeObject<dynamic>(response);
                //TResultado result = test.ToObject<TResultado>();

                //Este devuelve un Newtonsoft.Json.Linq.JToken
                var test2 = Newtonsoft.Json.Linq.JToken.Parse((string)resul["data"]);
                //Resultados para un solo elemento

                //Necesito validar acá el tipo de valor para aplicar varios tipo de conversión dependiendo 
                //si es un objeto complejo o no
                if (test2.GetType() == typeof(Newtonsoft.Json.Linq.JValue))
                {
                    //Respuestas simples como un id
                    var obj = JsonConvert.DeserializeObject<dynamic>(response);
                    resultado = obj.ToObject<TResultado>();
                }
                else
                {
                    //Respuesta para un JSON complejo
                    if (((Newtonsoft.Json.Linq.JContainer)test2).Count == 1)
                    {
                        resultado = test2[element].ToObject<TResultado>();
                    }
                }

            }
            respuesta.Codigo = sCode;
            respuesta.Mensaje = sMessage;
            respuesta.Data = resultado;
            return respuesta;
        }

        public static ResponseListRestDM<TResultado> InvocaServicioListRest<TResultado>
                                            (string url, string element)
        {
            var respuesta = new ResponseListRestDM<TResultado>() { };
            List<TResultado> resultadoLst = new List<TResultado>();
            string sMessage = "";
            CashdroEnum sCode = 0;

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            var responses = (HttpWebResponse)request.GetResponse();
            var response = new StreamReader(responses.GetResponseStream()).ReadToEnd();
            dynamic resul = JsonConvert.DeserializeObject<dynamic>(response);
            sCode = (CashdroEnum)resul["code"];
            sMessage = "";

            if (sCode != CashdroEnum.OK)
            {
                sMessage = resul["data"];

            }
            else
            {
                //Este devuelve un Newtonsoft.Json.Linq.JObject
                //var test = JsonConvert.DeserializeObject<dynamic>(response);
                //TResultado result = test.ToObject<TResultado>();

                //Este devuelve un Newtonsoft.Json.Linq.JToken
                var test2 = Newtonsoft.Json.Linq.JToken.Parse((string)resul["data"]);
                //Resultados para un solo elemento
                if (((Newtonsoft.Json.Linq.JContainer)test2).Count > 1)
                {
                    resultadoLst = test2[element].ToObject<List<TResultado>>();
                }

            }

            respuesta.Codigo = sCode;
            respuesta.Mensaje = sMessage;
            respuesta.Data = resultadoLst;
            return respuesta;
        }

    }
}
