﻿namespace ScreenSoundApi.Requests
{
    public record ArtistaRequestEdit(int id, string nome, string bio) : ArtistaRequest(nome, bio);
}
