using System;
using System.Diagnostics;
using System.IO;
using RestSharp;

namespace HackHarvardConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 1;

            resetTotal();

            while (num == 1) {

                getRate();

                

            }
            

            
        }

        static void getRate() {
            //get total
            var client = new RestClient("http://hhapi20181020060947.azurewebsites.net/api/getRate");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "e9225030-0275-4421-b619-2f63faf78fa0");
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);

            //pay
            Pay(Convert.ToInt64(response.Content.ToString()));
        }

        static void resetTotal()
        {
            //reset total
            var client = new RestClient("http://hhapi20181020060947.azurewebsites.net/api/resetTotal");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "5312f721-ea4d-4aa8-b6b4-9b4a23eead86");
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);

            System.Threading.Thread.Sleep(5000);
        }

        static void Pay(Int64 Charge) {
            ProcessStartInfo pInfo = new ProcessStartInfo();
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = "cmd.exe";
            processInfo.WorkingDirectory = Path.GetDirectoryName("C:\\Users\\romir");
            processInfo.Arguments = "/c ilp-spsp send --receiver " + "\"$romirpatne.localtunnel.me\"" + " --amount "+ Charge + " ";
            Process.Start(processInfo);
        }


    }
}
