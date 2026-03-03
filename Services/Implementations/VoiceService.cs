using NovaCore.Models.Request;
using NovaCore.Models.Response;
using NovaCore.Services.Interfaces;

namespace NovaCore.Services.Implementations
{
    public class VoiceService : IVoiceService
    {
        public VoiceResponse ProcessText(VoiceRequest request)
        {
            return new VoiceResponse
            {
                Message = $"Received: {request.Text}"
            };
        }
    }
}