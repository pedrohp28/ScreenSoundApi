namespace ScreenSoundApi.Requests
{
    public record GeneroRequestEdit(int id, string? nome, string? descricao) : GeneroRequest(nome, descricao);
}
