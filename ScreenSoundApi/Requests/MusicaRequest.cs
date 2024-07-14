using ScreenSound.Shared.Modelos.Modelos;
using System.Collections;

namespace ScreenSoundApi.Requests
{
    public record MusicaRequest(string nome, int? anoLancamento, int artistaId, ICollection<GeneroRequest> generos=null);
}
