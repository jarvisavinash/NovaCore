using NovaCore.Models.Request;

namespace NovaCore.Services.Interfaces
{
    public interface IVoiceService
    {
        Task<string> ProcessText(VoiceRequest request);
    }
}