using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Delans.Models
{
    public class ApiMethods
    {
        static public async Task<LoginResponse> Login(string basepath, string login, string password)
        {
            LoginRequest loginRequest = new LoginRequest() { Login = login, Password = password };
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = BaseAuth();

            var url = StringUrl(basepath, "login");// "https://unf.42clouds.com/unf_base1/11976/hs/api-delivery/login";
            var json = JsonConvert.SerializeObject(loginRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<LoginResponse>(result); 
        }

        static public async Task<bool> Logout(string basepath, string Token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = BaseAuth();

            var url = StringUrl(basepath, "logout"); //"https://unf.42clouds.com/unf_base1/11976/hs/api-delivery/logout";
            var data = new StringContent($"{{\"SessionId\":\"{Token}\"}}", Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
           Response DelansResponse = JsonConvert.DeserializeObject<Response>(result);

            return DelansResponse.Success;
        }

        static public async Task<GetShipmentsResponse> GetExt(string basepath, string Token, string ICN)
        {
            GetShipmentsRequest GetShipmentsRequest = new GetShipmentsRequest();
            GetShipmentsRequest.ICN = ICN;
            //GetShipmentsRequest.Date_Start = DateTime.Now.ToString("yyyyMMdd");
            GetShipmentsRequest.Date_Start = new DateTime(2020, 1, 1).ToString("yyyyMMdd");
            GetShipmentsRequest.Date_End = DateTime.Now.AddDays(7.0).ToString("yyyyMMdd");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = BaseAuth();
            client.DefaultRequestHeaders.Add("SessionId", Token);

            var url = StringUrl(basepath, "GetExtMon");//"https://unf.42clouds.com/unf_base1/11976/hs/api-delivery/GetExtMon";
            var json = JsonConvert.SerializeObject(GetShipmentsRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            GetShipmentsResponse DelansShipmentResponse = JsonConvert.DeserializeObject<GetShipmentsResponse>(result);

            return DelansShipmentResponse;
        }

        static public async Task<GetHistoryStatusInvoiceResponse> GetStatusOfInvoice(string basepath, string Token, string ICN, string InvNumber)
        {
            GetHistoryStatusInvoiceRequest GetHistoryStatusInvoiceRequest = new GetHistoryStatusInvoiceRequest();
            GetHistoryStatusInvoiceRequest.ICN = ICN;
            //GetHistoryStatusInvoiceRequest.ShipmentNumber[0] = InvNumber;
            GetHistoryStatusInvoiceRequest.ShipmentNumber.Add(InvNumber);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = BaseAuth();
            client.DefaultRequestHeaders.Add("SessionId", Token);

            var url = StringUrl(basepath, "HistoryStatusChangesInvoices");//"https://unf.42clouds.com/unf_base1/11976/hs/api-delivery/HistoryStatusChangesInvoices";
            var json = JsonConvert.SerializeObject(GetHistoryStatusInvoiceRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            GetHistoryStatusInvoiceResponse DelansGetHistoryStatusInvoiceResponse = JsonConvert.DeserializeObject<GetHistoryStatusInvoiceResponse>(result);

            return DelansGetHistoryStatusInvoiceResponse;
        }

        static public async Task<GetInvoiceResponse> GetInvoice(string basepath, string Token, string ICN,  string[] InvNumbers)
        {
            GetInvoiceStickersRequest GetInvoiceRequest = new GetInvoiceStickersRequest();
            GetInvoiceRequest.ICN = ICN;
            GetInvoiceRequest.Invoices = InvNumbers;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = BaseAuth();
            client.DefaultRequestHeaders.Add("SessionId", Token);

            var url = StringUrl(basepath, "GetInvoice");//"https://unf.42clouds.com/unf_base1/11976/hs/api-delivery/GetInvoice";
            var json = JsonConvert.SerializeObject(GetInvoiceRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            GetInvoiceResponse DelansGetInvoiceResponse = JsonConvert.DeserializeObject<GetInvoiceResponse>(result);

            return DelansGetInvoiceResponse;
        }

        static public async Task<GetStickerResponse> GetStickers(string basepath, string Token, string ICN, string[] InvNumbers)
        {
            GetInvoiceStickersRequest GetStickerRequest = new GetInvoiceStickersRequest();
            GetStickerRequest.ICN = ICN;
            GetStickerRequest.Invoices = InvNumbers;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = BaseAuth();
            client.DefaultRequestHeaders.Add("SessionId", Token);

            var url = StringUrl(basepath, "GetStickers");//"https://unf.42clouds.com/unf_base1/11976/hs/api-delivery/GetStickers";
            var json = JsonConvert.SerializeObject(GetStickerRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            GetStickerResponse DelansGetStickerResponse = JsonConvert.DeserializeObject<GetStickerResponse>(result);

            return DelansGetStickerResponse;
        }

        static public async Task<LoginResponseBase> GetLoginBase(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("Администратор:")));

            var url = "https://web-1c.42clouds.com/e18c338ab47fa91a4c523cac/1c_my_18199_10/hs/adm-api/linkbase?token=" + token;
            
            var response = await client.GetAsync(url);

            string result = response.Content.ReadAsStringAsync().Result;
            LoginResponseBase DelansLoginResponseBase = JsonConvert.DeserializeObject<LoginResponseBase>(result);

            return DelansLoginResponseBase;
        }

        static public string StringUrl(string basepath, string method)
        {
            return $"{basepath}/hs/api-delivery/{method}"; 
        }

        static public AuthenticationHeaderValue BaseAuth()
        {
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("Delans-Admin:Ghjnjnbg21@")));
        }
    }
}