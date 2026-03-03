using NovaCore.Models.Request;
using NovaCore.Models.Response;

namespace NovaCore.Services.Interfaces
{
    public interface IVoiceService
    {
        VoiceResponse ProcessText(VoiceRequest request);
    }
}