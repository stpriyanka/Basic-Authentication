using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class Program
	{
		private static HttpClient _client;
		private static string _baseUrl;

		public static void Main(string[] args)
		{
			SSLSite();
			Console.WriteLine("finished");
			Console.ReadLine();
		}

		public static async Task Response()
		{
			//var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", "username ", "password")));
			//_baseUrl = "http://localhost:1322/api/values";

			//_client = new HttpClient();
			//_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

			//Stopwatch sw = Stopwatch.StartNew();

			//var byteArray = Encoding.ASCII.GetBytes("username:password");
			//_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
			//HttpResponseMessage response = await _client.GetAsync(_baseUrl);

			//Console.WriteLine("Status Code: " + response.StatusCode);
			//response.EnsureSuccessStatusCode();

			//sw.Stop();

			//string responseBody = await response.Content.ReadAsStringAsync();
			//Console.WriteLine(responseBody);

			////Console.WriteLine(r);
			//Console.WriteLine("Response time: " + sw.Elapsed.TotalMilliseconds);
			//---------------------c:\users\priyanka\documents\visual studio 2015\Projects\WebApplication4\WebApplication4\certificate\aaa.pfx

		}

		public static async Task SSLSite()
		{

			ServicePointManager.ServerCertificateValidationCallback = delegate
			{ return true; };

			string Certificate = "C:\\github\\ssltest.cer";

			var handler = new WebRequestHandler();
			// Load the certificate into an X509Certificate object.
			X509Certificate2 cert = new X509Certificate2();
			cert.Import(Certificate);

			handler.ClientCertificates.Add(cert);
			HttpClient httpClient = new HttpClient(handler);

			httpClient.BaseAddress = new Uri("https://localhost:44300/");
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
			//httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

			Stopwatch sw = Stopwatch.StartNew();

			var byteArray = Encoding.ASCII.GetBytes("username:password");
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

			try
			{
				HttpResponseMessage response = await httpClient.GetAsync("api/values");
				sw.Stop();

				Console.WriteLine("Status Code: " + response.StatusCode);
				response.EnsureSuccessStatusCode();
				var responseBody = await response.Content.ReadAsStringAsync();

				Console.WriteLine(responseBody);
				Console.WriteLine("Response time: " + sw.Elapsed.TotalMilliseconds);

			}
			catch (Exception e)
			{
				Console.WriteLine(e.InnerException);

			}

		}

		// callback used to validate the certificate in an SSL conversation
		private static bool ValidateRemoteCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
		{
			bool result = false;
			if (cert.Subject.ToUpper().Contains("localhost"))
			{
				result = true;
			}

			return result;
		}
	}
}
