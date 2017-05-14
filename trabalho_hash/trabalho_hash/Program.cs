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
        static void Main(string[] args)
        {
            List<string> vocabulario = new List<string>();
            string[] arquivos = new string[] { "1", "2", "3", "4", "5", "6" };
            int maiorPalavra = 0;
            foreach (var arquivo in arquivos)
            {
                StreamReader f = new StreamReader(@"D:\Projetos\trabalho_hash\trabalho_hash\arquivos\" + arquivo + ".txt");
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

            FileStream fs = new FileStream(@"D:\Projetos\trabalho_hash\trabalho_hash\arquivos\vocabulario.txt", FileMode.Create, FileAccess.Write);

            StreamWriter file = new StreamWriter(fs, ASCIIEncoding.ASCII);

            string pesos = "";
            Random rand = new Random();
            for (int i = 0; i < maiorPalavra; i++)
            {
                pesos += rand.Next(1, maiorPalavra) + " ";
            }

            pesos = pesos.Substring(0, pesos.Length - 2);

            file.WriteLine(pesos);

            foreach (string palavra in vocabulario)
            {
                file.WriteLine(palavra);
            }

            file.Close();
        }
    }
}
