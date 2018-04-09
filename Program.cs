using System;
using RestSharp;
using Newtonsoft.Json;


namespace RestClientOA
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenClient = new RestClient("https://jxgxmx.eu.auth0.com/oauth/token");
            var tokenRequest = new RestRequest(Method.POST);
            tokenRequest.AddHeader("content-type", "application/json");
            tokenRequest.AddParameter("application/json", "{\"grant_type\":\"client_credentials\",\"client_id\": \"OSLnFKRKq1JHlEF1L57vkaoBX3lXFbnE\",\"client_secret\": \"ZPIaapCXjtFW9-EmBLscsAB8bXr4oICJ3OPknlKbKphyYbbkTTxRB7u86Xu6y0ua\",\"audience\": \"https://oatestcore/api\"}", ParameterType.RequestBody);
            IRestResponse tokenResponse = tokenClient.Execute(tokenRequest);
            
            BearerToken myToken = JsonConvert.DeserializeObject<BearerToken>(tokenResponse.Content);
            
            var authClient = new RestClient("http://localhost:5000/api/private");
            var authRequest = new RestRequest(Method.GET);
            authRequest.AddHeader("authorization", $"{myToken.token_type + " " + myToken.access_token}");
            IRestResponse authResponse = authClient.Execute(authRequest);
        }
    }

   
}