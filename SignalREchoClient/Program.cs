using Microsoft.AspNet.SignalR.Client;
using System;

namespace SignalREchoClient
{
	class Program
	{
		private static string EchoUrlString = "sdms";
		private const string ServiceUri = "http://localhost:12722/";

		static void Main(string[] args)
		{
			var connection = new Connection(ServiceUri + EchoUrlString, "name=michal");
			connection.Received += connection_Received;
			connection.StateChanged += connection_StateChanged;

			Console.WriteLine("Connecting...");

			connection.Start().Wait();

			string inputMsg;
			while (!string.IsNullOrEmpty(inputMsg = Console.ReadLine()))
			{
				connection.Send(inputMsg).Wait();
			}

			connection.Stop();
		}

		static void connection_StateChanged(StateChange state)
		{
			if (state.NewState == ConnectionState.Connected)
			{
				Console.WriteLine("Connected.");
			}
		}

		static void connection_Received(string data)
		{
			Console.WriteLine(data);
		}
	}
}
