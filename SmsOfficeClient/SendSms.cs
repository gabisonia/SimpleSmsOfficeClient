using System;
using System.Collections.Generic;
using System.Text;

namespace SmsOfficeClient
{
    public class SendSmsRequest
    {
        public string To { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
    }
    public class SendSmsResponse
    {
        public bool Success { get; set; }
        public string Result { get; set; }
    }

}
