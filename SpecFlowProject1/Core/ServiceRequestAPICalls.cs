using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Reflection;
using System.Xml;

namespace ECOM.B2B.PageObjects.Core
{
    
    public class ServiceRequestAPICalls
    {
        private string filePath;




        public string Jsonfile(string Account)
        { 
            filePath = Path.Combine("TestData\\"+Account+".json");
            return filePath;
        }

        public string RestSharpRequestPOST(string url, string xmlrequest)
        {

            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "420dd233-9fcf-a4e8-8411-44abff03c8b4");
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter("undefined", System.IO.File.ReadAllText(xmlrequest), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            HttpStatusCode StatusCode = response.StatusCode;
            bool IsSuccessful = response.IsSuccessful;
            string Content = response.Content.Replace("\n", "");
            long ContentLength = response.ContentLength;
            string ErrorMessage = response.ErrorMessage;
            Exception ErrorException = response.ErrorException;
            Assert.IsTrue(StatusCode.ToString().Equals("OK"), "UnExected reponse: " + Content);
            return Content;
        }

        public string RestSharpRequestDELETE(string url, string xmlrequest)
        {

            var client = new RestClient(url);
            var request = new RestRequest(Method.DELETE);
            //request.AddHeader("postman-token", "420dd233-9fcf-a4e8-8411-44abff03c8b4");
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter("undefined", System.IO.File.ReadAllText(xmlrequest), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            HttpStatusCode StatusCode = response.StatusCode;
            bool IsSuccessful = response.IsSuccessful;
            string Content = response.Content.Replace("\n", "");
            long ContentLength = response.ContentLength;
            string ErrorMessage = response.ErrorMessage;
            Exception ErrorException = response.ErrorException;
            Assert.IsTrue(StatusCode.ToString().Equals("OK"), "UnExected reponse: " + Content);
            return Content;
        }

        public string RestSharpRequestGET(string url, string xmlrequest)
        {

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            //request.AddHeader("postman-token", "420dd233-9fcf-a4e8-8411-44abff03c8b4");
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter("undefined", System.IO.File.ReadAllText(xmlrequest), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            HttpStatusCode StatusCode = response.StatusCode;
            bool IsSuccessful = response.IsSuccessful;
            string Content = response.Content.Replace("\n", "");
            long ContentLength = response.ContentLength;
            string ErrorMessage = response.ErrorMessage;
            Exception ErrorException = response.ErrorException;
            Assert.IsTrue(StatusCode.ToString().Equals("OK"), "UnExected reponse: " + Content);
            return Content;
        }
        public string ReadData(string Response)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(Response);
            string data = xml.InnerText;
            return data;
        }
    }

}