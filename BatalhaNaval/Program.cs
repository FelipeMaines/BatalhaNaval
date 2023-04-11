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

            EnxerMatriz(matriz, rand);
            MostrarMatriz(matriz);


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
                            Console.Write($"| |   {linha}");

                        else if (matriz.GetLength(1) - 1 == coluna && matriz[linha, coluna] != null)
                            Console.Write($"|{matriz[linha, coluna]}|   {linha}");

                        else if (matriz[linha, coluna] == null)
                             Console.Write("| |\t");
                        
                        else
                            Console.Write("|{0}| \t", matriz[linha, coluna]);
                    }
                }
                Console.WriteLine("\n---------------------------------------------------------------------------");

                for (int i = 0; i < 10; i++)
                {
                    Console.Write($" {i} \t");
                }
            }



            


            
        }
    }
}