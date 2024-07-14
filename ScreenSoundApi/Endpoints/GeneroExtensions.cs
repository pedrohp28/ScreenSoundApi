using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;
using ScreenSoundApi.Requests;
using ScreenSoundApi.Responses;

namespace ScreenSoundApi.Endpoints
{
    public static class GeneroExtensions
    {
        public static void AddEndpointsGeneros(this WebApplication app)
        {
            app.MapGet("/Generos", ([FromServices] DAL<Genero> dal) =>
            {
                var listaGenero = EntityListToResponseList(dal.Listar());
                return Results.Ok(listaGenero);
            });

            app.MapGet("/Generos/{nome}", ([FromServices] DAL<Genero> dal, string nome) =>
            {
                var genero = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                var generoResponse = EntityToResponse(genero);

                if (generoResponse is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(generoResponse);
            });

            app.MapPost("/Generos", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequest generoRequest) =>
            {
                var genero = new Genero();
                genero.Nome = generoRequest.nome;
                genero.Descricao = generoRequest.descricao;
                dal.Adicionar(genero);
                return Results.Ok();
            });

            app.MapDelete("/Generos/{id}", ([FromServices] DAL<Genero> dal, int id) =>
            {
                var genero = dal.RecuperarPor(a => a.Id == id);
                if (genero is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(genero);
                return Results.NoContent();
            });

            app.MapPut("/Generos", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequestEdit generoRequest) =>
            {
                var generoAtualizar = dal.RecuperarPor(a => a.Id == generoRequest.id);
                if (generoAtualizar is null)
                {
                    return Results.NotFound();
                }
                generoAtualizar.Nome = generoRequest.nome;
                generoAtualizar.Descricao = generoRequest.descricao;

                dal.Atualizar(generoAtualizar);
                return Results.Ok();
            });
        }
        private static ICollection<GeneroResponse> EntityListToResponseList(IEnumerable<Genero> musicaList)
        {
            return musicaList.Select(a => EntityToResponse(a)).ToList();
        }

        private static GeneroResponse EntityToResponse(Genero genero)
        {
            return new GeneroResponse(genero.Id, genero.Nome, genero.Descricao);
        }
    }
}
