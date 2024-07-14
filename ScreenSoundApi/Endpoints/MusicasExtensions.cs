using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;
using ScreenSoundApi.Requests;
using ScreenSoundApi.Responses;

namespace ScreenSoundApi.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndpointsMusicas(this WebApplication app)
        {
            app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
            {
                var listaMusicas = EntityListToResponseList(dal.Listar());
                return Results.Ok(listaMusicas);
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
            {
                var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                var musicaResponse = EntityToResponse(musica);

                if (musicaResponse is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(musicaResponse);
            });

            app.MapPost("/Musicas", ([FromServices] DAL<Musica> dalMusica, [FromServices] DAL <Genero> dalGenero, [FromBody] MusicaRequest musicaRequest) =>
            {
                var musica = new Musica(musicaRequest.nome);
                musica.AnoLancamento = musicaRequest.anoLancamento;
                musica.ArtistaId = musicaRequest.artistaId;
                musica.Generos = musicaRequest.generos is not null? GeneroRequestConverter(musicaRequest.generos, dalGenero): new List<Genero>();
                dalMusica.Adicionar(musica);
                return Results.Ok();
            });

            app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
            {
                var musica = dal.RecuperarPor(a => a.Id == id);
                if (musica is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(musica);
                return Results.NoContent();
            });

            app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequestEdit musicaRequest) =>
            {
                var musicaAtualizar = dal.RecuperarPor(a => a.Id == musicaRequest.id);
                if (musicaAtualizar is null)
                {
                    return Results.NotFound();
                }
                musicaAtualizar.Nome = musicaRequest.nome;
                musicaAtualizar.AnoLancamento = musicaRequest.anoLancamento;

                dal.Atualizar(musicaAtualizar);
                return Results.Ok();
            });
        }

        private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, DAL<Genero> dal)
        {
            var listaGeneros = new List<Genero>();
            foreach (var item in generos)
            {
                var entity = RequestToEntity(item);
                var genero = dal.RecuperarPor(g => g.Nome.ToUpper().Equals(item.nome.ToUpper()));
                if (genero is not null)
                {
                    listaGeneros.Add(genero);
                }
                else
                {
                    listaGeneros.Add(entity);
                }
            }
            return listaGeneros;
        }

        private static Genero RequestToEntity(GeneroRequest genero)
        {
            return new Genero() { Nome = genero.nome, Descricao = genero.descricao };
        }

        private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
        {
            return musicaList.Select(a => EntityToResponse(a)).ToList();
        }

        private static MusicaResponse EntityToResponse(Musica musica)
        {
            return new MusicaResponse(musica.Id, musica.Nome!, musica.AnoLancamento, musica.Artista?.Id, musica.Artista?.Nome);
        }
    }
}
