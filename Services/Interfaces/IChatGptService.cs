using System.Threading.Tasks;

namespace NovaCore.Services.Interfaces
{
    public interface IChatGptService
    {
        Task<string> GetResponse(string prompt);
    }
}