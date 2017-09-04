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
                response.Success = true;
                response.Result = HumanizeResultCode(postResponse.Content.ReadAsStringAsync().Result);
            }
            return response;
        }
        private string HumanizeResultCode(string resultCode)
        {
            switch (resultCode)
            {
                case "01":
                    return ApplicationStrings.Result01;
                case "100":
                    return ApplicationStrings.Result100;
                case "120":
                    return ApplicationStrings.Result120;
                case "130":
                    return ApplicationStrings.Result130;
                case "200":
                    return ApplicationStrings.Result200;
                default:
                    return String.Empty;
            }
        }

    }
}
