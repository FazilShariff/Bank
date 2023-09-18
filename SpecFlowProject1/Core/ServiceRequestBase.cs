using System.Net;
using System.Text;


namespace ECOM.B2B.PageObjects.Core
{
    public enum HttpMethod
    {
        GET,
        PUT,
        POST,
        DELETE
    }
   public  class ServiceRequestBase
    {
        public string EndPoint { get; set; }
        public HttpMethod Method { get; set; }
        public string ContentType { get; set; }
        public string Data { get; set; }
        //header fields
        public string Accept { get; set; }
        public string Host { get; set; }
        public long ContentLength { get; set; }


        public ServiceRequestBase()
        { }

        public string MakeRequest(HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return MakeRequest("", expectedStatusCode);
        }

        public string MakeRequest(string parameters, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

            request.Accept = Accept;
            request.UserAgent = "Fiddler";
            request.ContentType = ContentType;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            request.Method = Method.ToString();
            request.UseDefaultCredentials = true;

           // string xmlString = System.IO.File.ReadAllText(@"C:\Users\g9a6\Desktop\cxmlPunchOut.xml");
           //var requestBytes = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(xmlString);
           // request.ContentLength = requestBytes.Length;
           // Stream requestStream = request.GetRequestStream();
           // requestStream.Write(requestBytes, 0, requestBytes.Length);
           // requestStream.Close();

            if (!string.IsNullOrEmpty(Data) && (Method == HttpMethod.POST || Method == HttpMethod.PUT))
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(Data);
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {

                    var responseValue = string.Empty;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }
                    // grab the response
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }
                    return responseValue;
                }
            }

            catch (WebException w)
            {
                HttpWebResponse res = (HttpWebResponse)w.Response;
                String responseValue = String.Empty;
                var responseStream = res.GetResponseStream();

                if (responseStream != null)
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseValue = reader.ReadToEnd();
                    }

                if (expectedStatusCode != res.StatusCode)
                {
                    throw new Exception("Unexpected response from Service  " + request.RequestUri + " Expected:" + expectedStatusCode + " but got:" + res.StatusCode );
                }
                return w.Message;
            }
        }
    }
}
