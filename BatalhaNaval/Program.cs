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

            EnxerMatriz(matriz, rand);
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

            static void EnxerMatriz(string[,] matriz, Random rand)
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
                    VerificarPosicaoUm(matriz, linha, coluna);

                    if (!VerificarPosicaoUm(matriz, linha, coluna))
                    VerificarPosicaoDois(matriz, linha, coluna);                

                    // N acertou nada
                    if (matriz[coluna, linha] == null)
                    {
                        Console.WriteLine("Vc errou!");
                        tentativas++;

                    }

                    //acertou
                    else if (matriz[coluna, linha] != null)
                    {
                        if (matriz[coluna, linha] == "A")
                        {
                            Console.WriteLine("Voce acertou um Porta Aviao");
                            pontuacao += 5;

                        }

                        else if (matriz[coluna, linha] == "C")
                        {
                            Console.WriteLine("Voce acertou um Cruzador");
                            pontuacao += 15;

                        }

                        else if (matriz[coluna, linha] == "R")
                        {
                            Console.WriteLine("Voce acertou um Rebocador");
                            pontuacao += 10;

                        }
                    }
                }



                //Procura 1 casa



                Console.ReadLine();

                return pontuacao;
            }
        }

        public static bool VerificarPosicaoUm(string[,] matriz, int linha, int coluna)
        {
            string distancia = "1";
            string greenDistancia = "\u001b[32m" + distancia + "\u001b[0m"; // ANSI escape sequence for green color

            if (coluna != 9 && linha != 9 && coluna != 0 && linha != 0)
            {

                if (matriz[coluna + 1, linha] == "A" || matriz[coluna + 1, linha + 1] == "A" ||
                    matriz[coluna, linha - 1] == "A" || matriz[coluna - 1, linha] == "A" || matriz[coluna - 1, linha - 1] == "A" ||
                    matriz[coluna, linha + 1] == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    matriz[coluna, linha] = distancia;
                    Console.ResetColor();
                    return true;
                }
            }

            else if (coluna == 9 && linha > 0)
            {
                if (matriz[coluna, linha + 1] == "A" || matriz[coluna - 1, linha + 1] == "A" || matriz[coluna - 1, linha] == "A" || matriz[coluna - 1, linha - 1] == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    matriz[coluna, linha] = distancia;
                    Console.ResetColor();
                    return true;
                }
            }

            else if (linha == 9 && coluna > 0)
            {
                if (matriz[coluna + 1, linha] == "A" || matriz[coluna - 1, linha] == "A" || matriz[coluna - 1, linha - 1] == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    matriz[coluna, linha] = distancia;
                    Console.ResetColor();
                    return true;
                }
            }

            else if (linha == 0 && coluna < 9)
            {
                if (matriz[coluna, linha + 1] == "A" || matriz[coluna - 1, linha + 1] == "A" || matriz[coluna - 1, linha] == "A" || matriz[coluna - 1, linha] == "A" || matriz[coluna + 1, linha + 1] == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    matriz[coluna, linha] = distancia;
                    Console.ResetColor();
                    return true;
                }
            }

            else if (linha < 9 && coluna == 0)
            {
                if (matriz[coluna, linha + 1] == "A" || matriz[coluna, linha - 1] == "A" || matriz[coluna, linha] == "A" || matriz[coluna + 1, linha + 1] == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    matriz[coluna, linha] = distancia;
                    Console.ResetColor();
                    return true;
                }
            }
            return false;
        }

        public static bool VerificarPosicaoDois(string[,] matriz, int linha, int coluna)
        {
            string distancia = "2";

            //if (coluna < 8 && linha < 8 && coluna > 2 && linha > 2)
            //{

            //    if (matriz[coluna + 2, linha] == "A" || matriz[coluna + 2, linha + 2] == "A" ||
            //        matriz[coluna, linha - 2] == "A" || matriz[coluna - 2, linha] == "A" || matriz[coluna - 2, linha - 2] == "A" ||
            //        matriz[coluna, linha + 2] == "A")
            //    {
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        matriz[coluna, linha] = distancia;
            //        Console.ResetColor();
            //        return true;
            //    }
            //}

            //else if (coluna > 7 && linha > 2)
            //{
            //    if (matriz[coluna, linha + 2] == "A" || matriz[coluna - 2, linha + 2] == "A" || matriz[coluna - 2, linha] == "A" || matriz[coluna - 2, linha - 2] == "A")
            //    {
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        matriz[coluna, linha] = distancia;
            //        Console.ResetColor();
            //        return true;
            //    }
            //}

            //else if (linha > 8 && coluna > 2)
            //{
            //    if (matriz[coluna + 2, linha] == "A" || matriz[coluna - 2, linha] == "A" || matriz[coluna - 2, linha - 2] == "A")
            //    {
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        matriz[coluna, linha] = distancia;
            //        Console.ResetColor();
            //        return true;
            //    }
            //}

            //else if (linha < 8 && coluna > 1)
            //{
            //    if (matriz[coluna, linha + 2] == "A" || matriz[coluna, linha - 2] == "A" || matriz[coluna, linha] == "A" || matriz[coluna + 2, linha + 2] == "A")
            //    {
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        matriz[coluna, linha] = distancia;
            //        Console.ResetColor();
            //        return true;
            //    }
            //}

            //else if (linha < 2 && coluna > 7)
            //{
            //    if (matriz[coluna, linha + 2] == "A" || matriz[coluna - 2, linha + 2] == "A" || matriz[coluna - 2, linha] == "A" || matriz[coluna - 2, linha] == "A" || matriz[coluna + 2, linha + 2] == "A")
            //    {
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        matriz[coluna, linha] = distancia;
            //        Console.ResetColor();
            //        return true;
            //    }
            //}


            //else if (linha < 2 && coluna < 7)
            //{
            //    if (matriz[coluna, linha + 2] == "A" || matriz[coluna - 2, linha + 2] == "A" || matriz[coluna - 2, linha] == "A" || matriz[coluna - 2, linha] == "A" || matriz[coluna + 2, linha + 2] == "A")
            //    {
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        matriz[coluna, linha] = distancia;
            //        Console.ResetColor();
            //        return true;
            //    }
            //}

            //else if (linha > 7 && coluna < 2)
            //{
            //    if (matriz[coluna, linha - 2] == "A" || matriz[coluna, linha] == "A" || matriz[coluna + 2, linha] == "A")
            //    {
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        matriz[coluna, linha] = distancia;
            //        Console.ResetColor();
            //        return true;
            //    }
            //}
            //return false;

            if (coluna < 8 && linha < 8 && coluna > 1 && linha > 1)
            {
                if (matriz[coluna + 1, linha] == "A" || matriz[coluna, linha - 1] == "A" || matriz[coluna - 1, linha] == "A" || matriz[coluna, linha + 1] == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    matriz[coluna, linha] = distancia;
                    Console.ResetColor();
                    return true;
                }
            }
            else if (coluna > 7 && linha > 7)
            {
                if (matriz[coluna, linha + 1] == "A" || matriz[coluna - 1, linha] == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    matriz[coluna, linha] = distancia;
                    Console.ResetColor();
                    return true;
                }
            }
            else if (linha > 7 && coluna < 7)
            {
                if (matriz[coluna + 1, linha] == "A" || matriz[coluna - 1, linha] == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    matriz[coluna, linha] = distancia;
                    Console.ResetColor();
                    return true;
                }
            }
            else if (linha < 8 && coluna > 1)
            {
                if (matriz[coluna, linha + 1] == "A" || matriz[coluna, linha - 1] == "A" || matriz[coluna, linha] == "A")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
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