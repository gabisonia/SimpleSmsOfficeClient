using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SmsOfficeClient
{
    public class SmsOfficeClient
    {
        private HttpClient _client;
        private string _baseAddress;
        private string _key;
        public SmsOfficeClient(string key)
        {
            _baseAddress = "http://smsoffice.ge/api/send.aspx";
            _client = new HttpClient();
            _key = key;
        }
        public SendSmsResponse Send(SendSmsRequest request)
        {
            var response = new SendSmsResponse();
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("key", _key),
                new KeyValuePair<string, string>("destination", request.To),
                new KeyValuePair<string, string>("sender", request.Sender),
                new KeyValuePair<string, string>("content", request.Content)
             };
            var content = new FormUrlEncodedContent(pairs);
            var postResponse = _client.PostAsync(_baseAddress, content).Result;
            if (postResponse.IsSuccessStatusCode)
            {
                var resultCode = postResponse.Content.ReadAsStringAsync().Result;
                response.Result = HumanizeResultCode(resultCode);
                response.Success = resultCode == "01";
            }
            return response;
        }
        private string HumanizeResultCode(string resultCode)
        {
            var result = String.Empty;
            switch (resultCode)
            {
                case "01":
                    result = ApplicationStrings.Result01;
                    break;
                case "100":
                    result = ApplicationStrings.Result100;
                    break;
                case "120":
                    result = ApplicationStrings.Result120;
                    break;
                case "130":
                    result = ApplicationStrings.Result130;
                    break;
                case "200":
                    result = ApplicationStrings.Result200;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
