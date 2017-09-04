using System;

namespace SmsOfficeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new SmsOfficeClient("api-key");
            var response = client.Send(new SendSmsRequest
            {
                Content = "Test",
                To = "995597933199",
                Sender="Sender"
            });
            if (response.Success)
            {
                Console.WriteLine("Success");
                //Console.WriteLine(response.Result);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }
}
