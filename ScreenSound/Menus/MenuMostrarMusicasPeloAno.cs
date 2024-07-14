using ScreenSound.Banco;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Menus
{
    public class MenuMostrarMusicasPeloAno : Menu
    {
        public override void Executar(DAL<Artista> DAL)
        {
            base.Executar(DAL);
            ExibirTituloDaOpcao("Exibir músicas pelo ano");
            Console.Write("Digite o ano de lançamento das músicas que deseja ver: ");
            int anoLancamento = Convert.ToInt32(Console.ReadLine());
            var musicaDAL = new DAL<Musica>(new ScreenSoundContext());
            var musicasDoAno = musicaDAL.ListarPor(m => m.AnoLancamento == anoLancamento);
            if (musicasDoAno.Any())
            {
                Console.WriteLine("\nMúsicas:");
                foreach (var musica in musicasDoAno)
                {
                    musica.ExibirFichaTecnica();
                }
                Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine($"\nO ano de {anoLancamento} não possui músicas registradas!");
                Console.WriteLine("Digite uma tecla para voltar ao menu principal");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
