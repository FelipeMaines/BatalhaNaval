using System.Reflection.Metadata.Ecma335;

namespace BatalhaNaval
{
    internal partial class Program
    {
        static Random rand = new Random();
        static string[,] matriz = new string[10, 10];
        static void Main(string[] args)
        {
            
            int chances = 15;
            int pontuacao = 0;
            int tentativas = 1;

            EnxerMatriz();
            MostrarMatriz(matriz);

            while(tentativas <= 15)
            {
            ChutarEVerifica(matriz, pontuacao, ref tentativas);
            }

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
                Preenchedor("A", 10);
                Preenchedor("C", 1);
                Preenchedor("R", 2);
            }

            static void MostrarMatriz(string[,] matriz)
            {
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
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{matriz[linha, coluna]}");
                            Console.ResetColor();
                            Console.Write($"|");


                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write($"   {linha}");
                            Console.ResetColor();

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

                        else
                            Console.Write("|{0}| \t", matriz[linha, coluna]);
                    }
                }
                Console.WriteLine("\n---------------------------------------------------------------------------");

                for (int i = 0; i < 10; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($" {i} \t");
                    Console.ResetColor();
                }

                Console.WriteLine();
                Console.WriteLine();
            }

            static int ChutarEVerifica(string[,] matriz, int pontuacao, ref int tentativas)
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
                VerificaForaDoMapa(linha, coluna);

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
                                pontuacao += 5;
                                break;

                             }

                            else if (matriz[coluna, linha] == "C")
                            {
                                Console.WriteLine("Voce acertou um Cruzador");
                                pontuacao += 15;
                                break;

                            }

                            else if (matriz[coluna, linha] == "R")
                            {
                                Console.WriteLine("Voce acertou um Rebocador");
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
                                VerificarPosicaoTres(matriz, linha, coluna);
                                break;
                            }
                        }
                    }
                    

                    // N acertou nada
                    

                    //acertou
                   
                }



                //Procura 1 casa



                Console.ReadLine();

                return pontuacao;
            }
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