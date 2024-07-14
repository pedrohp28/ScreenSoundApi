using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

public class MenuMostrarArtistas : Menu
{
    public override void Executar(DAL<Artista> DAL)
    {
        base.Executar(DAL);
        ExibirTituloDaOpcao("Exibindo todos os artistas registradas na nossa aplicação");

        foreach (var artista in DAL.Listar())
        {
            Console.WriteLine($"Artista: {artista}");
        }

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
