using DepDos.Core.Helpers;
using DepDos.Core.Models;

namespace DepDos.Core.Interfaces;

public interface IDosService
{
    void StartRequests(string url, IDosConfiguration configuration);
    void StopRequests();
}