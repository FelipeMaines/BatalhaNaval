using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;
using System.Xml;

namespace BatalhaNaval
{
    internal partial class Program
    {
        static Random rand = new Random();
        static string[,] matriz = new string[10, 10];
        static void Main(string[] args)
        {
            int pontuacao = 0;
            int tentativas = 1;

            EnxerMatriz();
            MostrarMatriz(matriz);

            while(FimDeJogo(tentativas, 5) && VerificarSeAcertouTudo(matriz))
            ChutarEVerifica(matriz, ref pontuacao, ref tentativas);
            

            MostrarFinalGame(matriz);
            MostrarPontuacaoFinal(pontuacao);

            static void Preenchedor(string letra, int quantidadeElementos)
            {
                int primeiroNumero, segundoNumero;
                for (int i = 0; i < quantidadeElementos; i++)
                {
                    primeiroNumero = rand.Next(0, 10);
                    segundoNumero = rand.Next(0, 10);

                    if (matriz[primeiroNumero, segundoNumero] == null)
                    {
                        matriz[primeiroNumero, segundoNumero] = letra;
                    }

                    else
                    {
                        i -= 1;
                        continue;
                    }
                }
            }
            static void EnxerMatriz()
            {
                Preenchedor("A", 2);
                Preenchedor("C", 1);
                Preenchedor("R", 2);
            }
            static void MostrarMatriz(string[,] matriz)
            {
                Console.Clear();
                for (int linha = 0; linha < matriz.GetLength(0); linha++)
                {
                    Console.WriteLine("\n---------------------------------------------------------------------------");

                    for (int coluna = 0; coluna < matriz.GetLength(1); coluna++)
                    {
                        if (matriz.GetLength(1) - 1 == coluna && matriz[linha, coluna] == null)
                        {
                            Console.Write($"| |");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write($"   {linha}");
                            Console.ResetColor();
                        }

                        else if (matriz.GetLength(1) - 1 == coluna && matriz[linha, coluna] != null)
                        {

                            Console.Write($"|");
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write($"{matriz[linha, coluna]}");
                            Console.ResetColor();
                            Console.Write($"|");


                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write($"   {linha}");
                            Console.ResetColor();

                        }

                        else if (matriz[linha, coluna] == "C" || matriz[linha, coluna] == "A" || matriz[linha, coluna] == "R")
                        {
                            Console.Write("|");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(matriz[linha, coluna]);
                            Console.ResetColor();
                            Console.Write("|\t");
                        }

                        else if (matriz[linha,coluna] == "c" || matriz[linha, coluna] == "a" || matriz[linha, coluna] == "r")
                        {
                            Console.Write($"|");
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write($"{matriz[linha, coluna]}");
                            Console.ResetColor();
                            Console.Write($"|\t");
                            
                        }

                        else if (matriz[linha, coluna] == null)
                            Console.Write("| |\t");

                        else if (matriz[linha, coluna] == "1")
                        {
                            Console.Write("|");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("1");
                            Console.ResetColor();
                            Console.Write("|\t");
                        }

                        else if (matriz[linha, coluna] == "2")
                        {
                            Console.Write("|");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("2");
                            Console.ResetColor();
                            Console.Write("|\t");
                        }

                        else if (matriz[linha, coluna] == "3")
                        {
                            Console.Write("|");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("3");
                            Console.ResetColor();
                            Console.Write("|\t");
                        }

                        else if (matriz[linha, coluna] == "M")
                        {
                            Console.Write("|");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("M");
                            Console.ResetColor();
                            Console.Write("|\t");
                        }


                    }
                }
                Console.WriteLine("\n---------------------------------------------------------------------------");

                for (int i = 0; i < 10; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($" {i} \t");
                    Console.ResetColor();
                }

                Console.WriteLine("\n\nPrimeiro numero");
                Console.WriteLine();
            }
            static void ChutarEVerifica(string[,] matriz, ref int pontuacao, ref int tentativas)
            {

                Console.Clear();
                MostrarMatriz(matriz);
                Console.WriteLine($"\nQtd de tentativas: {tentativas}\n");
                Console.WriteLine("Quais as casas que deseja atirar: Ex 0 0");
                string chutestr = Console.ReadLine();
                string[] chute = chutestr.Split(' ');

                int linha = int.Parse(chute[0]);
                int coluna = int.Parse(chute[1]);

                // Saiu da area
                if(!VerificaForaDoMapa(linha, coluna))
                {
                    while (true)
                    {
                        if (matriz[coluna, linha] == null)
                        {
                            Console.WriteLine("Vc errou!");
                            tentativas++;

                        }

                        else if (matriz[coluna, linha] != null)
                        {
                            if (matriz[coluna, linha] == "A")
                            {
                                Console.WriteLine("Voce acertou um Porta Aviao");
                                matriz[coluna, linha] = "a";
                                pontuacao += 5;
                                break;

                             }

                            else if (matriz[coluna, linha] == "C")
                            {
                                Console.WriteLine("Voce acertou um Cruzador");
                                matriz[coluna, linha] = "c";
                                pontuacao += 15;
                                break;

                            }

                            else if (matriz[coluna, linha] == "R")
                            {
                                Console.WriteLine("Voce acertou um Rebocador");
                                matriz[coluna, linha] = "r";
                                pontuacao += 10;
                                break;

                            }
                        }
                       

                        VerificarPosicaoUm(matriz, linha, coluna);

                        if (VerificarPosicaoUm(matriz, linha, coluna))
                        {
                            break;
                        }
                        else
                        {
                            VerificarPosicaoDois(matriz, linha, coluna);

                            if(VerificarPosicaoDois(matriz, linha, coluna))
                            {
                                break;
                                
                            }
                            else
                            {
                                if (VerificarPosicaoTres(matriz, linha, coluna))
                                    break;

                                else
                                {
                                    VerificarM(matriz, linha, coluna);
                                    break;
                                }
                            }
                        }
                    }
                }
                Console.ReadLine();
            }
        }
        public static void MostrarPontuacaoFinal(int pontuacao)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("SUA PONTUACAO FINAL FICOU EM:");

            Console.WriteLine("------------------------");
            Console.WriteLine($"----------- { pontuacao } ----------");
            Console.WriteLine("------------------------\n\n");

            Console.ResetColor();

        }
        public static void MostrarFinalGame(string[,] matriz)
        {
            Console.Clear();

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                Console.WriteLine("\n----------------------------------------------------------------------------");
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i,j] != null)
                    {

                        if(matriz[i, j] == "1" || matriz[i, j] == "2" || matriz[i, j] == "3" || matriz[i, j] == "M")
                        {
                            Console.Write($"|");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{matriz[i, j]}");
                            Console.ResetColor();
                            Console.Write($"|\t");
                        }

                        else if (matriz[i,j] == "a" || matriz[i, j] == "r" || matriz[i, j] == "c")
                        {
                            Console.Write($"|");
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write($"{matriz[i, j]}");
                            Console.ResetColor();
                            Console.Write($"|\t");
                        }

                        else
                        {
                            Console.Write($"|");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write($"{matriz[i, j]}");
                            Console.ResetColor();
                            Console.Write($"|\t");
                        }



                        
                    }

                    else if (matriz[i, j] == null)
                    Console.Write("| | \t");



                }


            }

            Console.WriteLine("\n");
        }
        public static bool FimDeJogo(int tentativas, int chances )
        {
            if (tentativas <= chances)
                return true;

            return false;
        }
        public static bool VerificarSeAcertouTudo(string[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] == "A" || matriz[i, j] == "R" || matriz[i, j] == "C")
                        return true;
                }
            }
            return false;
        }
        public static bool VerificarPosicaoUm(string[,] matriz, int linha, int coluna)
        {
            string distancia = "1";

            for (int i = coluna - 1; i <= coluna + 1; i++)
            {
                for (int j = linha - 1; j <= linha + 1; j++)
                {
                    if (i < 0 || j < 0)
                        continue;

                    if (i >= matriz.GetLength(0) || j >= matriz.GetLength(1))
                    {
                        continue; // Posição está fora da matriz
                    }

                    if (matriz[i, j] == "A" || matriz[i, j] == "C" || matriz[i, j] == "R")
                    {
                        matriz[coluna, linha] = distancia;
                        Console.ResetColor();
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool VerificarPosicaoDois(string[,] matriz, int linha, int coluna)
        {
            string distancia = "2";

            for (int i = coluna - 2; i <= coluna + 2; i++)
            {
                for (int j = linha - 2; j <= linha + 2; j++)
                {

                    if (i == coluna && j == linha)
                        continue;

                    if (i < 0 || i >= matriz.GetLength(0) || j < 0 || j >= matriz.GetLength(1))
                    {
                        continue; // Posição está fora da matriz
                    }

                    if (matriz[i, j] == "A" || matriz[i, j] == "C" || matriz[i, j] == "R")
                    {
                        matriz[coluna, linha] = distancia;
                        Console.ResetColor();
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool VerificarPosicaoTres(string[,] matriz, int linha, int coluna)
        {
            string distancia = "3";

            for (int i = coluna - 3; i <= coluna + 3; i++)
            {
                for (int j = linha - 3; j <= linha + 3; j++)
                {
                    if (i < 0 || i >= matriz.GetLength(0) || j < 0 || j >= matriz.GetLength(1))
                    {
                        continue; // Posição está fora da matriz
                    }

                    if (matriz[i, j] == "A" || matriz[i, j] == "C" || matriz[i, j] == "R") 
                    {
                        matriz[coluna, linha] = distancia;
                        Console.ResetColor();
                        return true;
                    }
                }
            }

            return false;
        }
        public static bool VerificarM(string[,] matriz, int linha, int coluna)
        {
            string distancia = "M";

            for (int i = 0; i <= matriz.GetLength(0); i++)
            {
                if (i < 0 || i >= matriz.GetLength(1))
                {
                    continue; // Posição está fora da matriz
                }

                if (matriz[i, coluna] == "A" || matriz[i, coluna] == "C" || matriz[i, coluna] == "R")
                    {
                        matriz[coluna, linha] = distancia;
                        Console.ResetColor();
                        return true;
                    }
            }

            for (int j = 0; j <= matriz.GetLength(1); j++)
            {
                if (j < 0 || j >= matriz.GetLength(1))
                {
                    continue; // Posição está fora da matriz
                }

                if (matriz[j, linha] == "A" || matriz[j, linha] == "C" || matriz[j, linha] == "R")
                {
                    matriz[coluna, linha] = distancia;
                    Console.ResetColor();
                    return true;
                }
            }

       
            return false;
        }
        public static bool VerificaForaDoMapa(int linha, int coluna)
        {
            if (linha > 9 || coluna > 9)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Vc atirou para fora do mapa");
                Console.WriteLine("Nao sera contata a tentativa");
                Console.ResetColor();
                return true;
            }
            return false;
        }

    }
}