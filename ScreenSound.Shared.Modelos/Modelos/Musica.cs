﻿using ScreenSound.Shared.Modelos.Modelos;
using System.Collections;

namespace ScreenSound.Modelos;

public class Musica
{

    public int Id { get; set; }
    public string Nome { get; set; }
    public int? AnoLancamento { get; set; }
    public int? ArtistaId { get; set; }
    public virtual Artista? Artista { get; set; }
    public virtual ICollection<Genero> Generos { get; set; }
    public Musica(string nome)
    {
        Nome = nome;
    }

    public Musica()
    {
    }

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");
      
    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Nome}
        Ano Lancamento: {AnoLancamento}";
    }
}