using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalho_hash
{
    class Program
    {
        static List<string> buscarVocalulario()
        {
            List<string> vocabulario = new List<string>();

            string[] arquivos = new string[] { "1", "2", "3", "4", "5", "6" };
            int maiorPalavra = 0;
            foreach (var arquivo in arquivos)
            {
                StreamReader f = new StreamReader(@"D:\Projetos\trabalhos_tap\trabalho_hash\trabalho_hash\arquivos\" + arquivo + ".txt");
                String texto = f.ReadToEnd();
                texto = texto.Replace(System.Environment.NewLine, ",");
                texto = texto.Replace(".", "");
                var palavras = texto.Split(',');
                foreach (var str in palavras)
                {
                    if (str != " ")
                    {
                        string aux = str.ToLower();

                        if (aux.Contains(':'))
                            aux = str.Substring(aux.IndexOf(':') + 1, aux.Length - aux.IndexOf(':') - 1);

                        if (aux[0] == ' ')
                            aux = aux.Substring(1, aux.Length - 1);

                        if (!vocabulario.Contains(aux))
                            vocabulario.Add(aux);

                        if (aux.Length > maiorPalavra)
                            maiorPalavra = aux.Length;
                    }
                }
            }
            vocabulario.Sort();

            return vocabulario;
        }

        static int buscarMaiorLength(List<string> vocabulario)
        {
            int maiorLength = 0;

            foreach (string palavra in vocabulario)
                if (palavra.Length > maiorLength)
                    maiorLength = palavra.Length;

            return maiorLength;
        }

        static int[] calcularPesos(int tamanho)
        {
            int[] pesos = new int[tamanho];
            Random rand = new Random();
            for (int i = 0; i < tamanho; i++)
            {
                pesos[i] = rand.Next(1, tamanho);
            }

            return pesos;
        }

        static int calcularK(string palavra, int[] pesos)
        {
            int k = 0;
            for (int i = 0; i < palavra.Length; i++)
                k += palavra[i] * pesos[i];

            return k;
        }

        static string[] gerarTabelaHash()
        {
            List<string> vocabulario = buscarVocalulario();
            int tamanhoMaximo = buscarMaiorLength(vocabulario);
            int[] pesos = calcularPesos(tamanhoMaximo);

            string[] tabelaHash = new string[vocabulario.Count];

            foreach (string palavra in vocabulario)
            {
                int k = calcularK(palavra, pesos);
                int h, i = 0;
                bool colidiu = false;
                do
                {
                    h = (k + i) % tabelaHash.Length;
                    if (tabelaHash[h] != null)
                    {
                        colidiu = true;
                        if (i > tabelaHash.Length - 1)
                            i = 0;
                        else
                            i++;
                    }
                    else
                    {
                        colidiu = false;
                        tabelaHash[h] = palavra;
                    }
                } while (colidiu);
            }

            return tabelaHash;
        }



        static void Main(string[] args)
        {

            FileStream fs = new FileStream(@"D:\Projetos\trabalhos_tap\trabalho_hash\trabalho_hash\arquivos\vocabulario.txt", FileMode.Create, FileAccess.Write);

            StreamWriter file = new StreamWriter(fs, ASCIIEncoding.ASCII);

            string pesos = "";
            foreach (var peso in calcularPesos(buscarMaiorLength(buscarVocalulario())))
            {
                pesos += peso + " ";
            }

            file.WriteLine(pesos.Trim());

            foreach (string palavra in gerarTabelaHash())
            {
                file.WriteLine(palavra);
            }

            file.Close();
        }
    }
}
