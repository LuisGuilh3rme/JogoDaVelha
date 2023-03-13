// Declaração de variáveis
char[,] tabuleiro = new char[3, 3];
char simbolo = 'X';
int rodada = 0;
int velha = 0;

// Programa
InicializarTabuleiro(tabuleiro);

do
{
    PrepararJogada(simbolo, tabuleiro, velha);
    rodada++;
    
    if (VerificarFim(rodada, tabuleiro, ref velha)) break;
    simbolo = TrocaSimbolo(simbolo);
} while (true);

ImprimirTabuleiro(tabuleiro);
if (velha == 0) Console.WriteLine("Vitória do jogador {0}", (simbolo == 'X' ? 1 : 2));
else
{
    Console.WriteLine("Empate");
    Console.Beep();
}
Console.WriteLine("Fim de jogo");

// Inicializar o tabuleiro atribuindo posições entendíveis para o usuário
void InicializarTabuleiro(char[,] tabuleiro)
{
    int contador = 1;

    for (int i = 0; i < tabuleiro.GetLength(0); i++)
    {
        for (int j = 0; j < tabuleiro.GetLength(1); j++)
        {
            tabuleiro[i, j] = char.Parse($"{contador}");
            contador++;
        }
    }
}

// Imprimindo o tabuleiro
void ImprimirTabuleiro(char[,] tabuleiro)
{
    Console.Clear();
    ConsoleColor aux = Console.ForegroundColor;

    Console.WriteLine();
    for (int i = 0; i < tabuleiro.GetLength(0); i++)
    {
        for (int j = 0; j < tabuleiro.GetLength(1); j++)
        {
            // Troca de cores caso condição seja verdadeira
            if (tabuleiro[i,j] == 'O') Console.ForegroundColor = ConsoleColor.Blue;
            else if (tabuleiro[i, j] == 'X') Console.ForegroundColor = ConsoleColor.Red;

            Console.Write(" {0} ", tabuleiro[i, j]);

            // Retornar à cor padrão
            Console.ForegroundColor = aux;
            if (j != 2) Console.Write("|");
        }
        if (i != 2)Console.WriteLine("\n---|---|---");
    }
    Console.WriteLine();
    Console.WriteLine();
}

// Prepara e realiza jogada no tabuleiro
void PrepararJogada (char simbolo, char[,] tabuleiro, int velha)
{
    ImprimirTabuleiro(tabuleiro);
    if (velha == 1) Console.WriteLine("(Empate encontrado)");
    Console.Write("Jogador {0}, insira [{1}]: ", (simbolo == 'X'? 1 : 2), simbolo);
    char.TryParse(Console.ReadLine(), out char posicao);

    // Caso jogada for inválida, reiniciar função através de recursiva
    if (!ExecutarJogada(posicao, simbolo, tabuleiro)) PrepararJogada(simbolo, tabuleiro, velha);
}

// Troca o simbolo conforme o que já está sendo utilizado
char TrocaSimbolo(char simbolo)
{
    if (simbolo == 'O')  return 'X';
    return 'O';
}

// Executar a jogada
bool ExecutarJogada (char posicao, char simbolo, char[,] tabuleiro)
{
    for (int i = 0; i < tabuleiro.GetLength(0); i++)
    {
        for (int j = 0; j < tabuleiro.GetLength(1); j++)
        {
            if (tabuleiro[i, j] == posicao)
            {
                tabuleiro[i, j] = simbolo;
                return true;
            }
        }
    }
    return false;
}

// Verificação de fim de jogo
bool VerificarFim (int rodada, char[,] tabuleiro, ref int velha)
{
    if (rodada >= 5)
    {
        if (VerificarVitoria(tabuleiro)) return true;
    }

    if (rodada >= 6)
    {
        if (VerificarVelha(tabuleiro)) velha = 1;
    }

    if (rodada == 9)
    {
        velha = 2;
        return true;
    }
    return false;
}

// Verificação de vitória
bool VerificarVitoria(char[,] tabuleiro)
{
    // Verificação de linhas
    for (int i = 0; i < tabuleiro.GetLength(0); i++)
    {
        if ((tabuleiro[i, 0] == tabuleiro[i, 1]) && (tabuleiro[i, 0] == tabuleiro[i, 2]))
        {
            return true;
        }
    }

    // Verificação de colunas
    for (int j = 0; j < tabuleiro.GetLength(1); j++)
    {
        if ((tabuleiro[0, j] == tabuleiro[1, j]) && (tabuleiro[1, j] == tabuleiro[2, j]))
        {
            return true;
        }
    }

    // Verificação de diagonal principal
    if ((tabuleiro[0,0] == tabuleiro[1, 1]) && (tabuleiro[1, 1] == tabuleiro[2, 2]))
    {
        return true;
    }

    // Verificação de diagonal secundária
    if ((tabuleiro[0, 2] == tabuleiro[1, 1]) && (tabuleiro[1, 1] == tabuleiro[2, 0]))
    {
        return true;
    }

    return false;
}

// Verificação antecipada de empate
bool VerificarVelha(char[,] tabuleiro)
{
    // Verifica se linhas 0 e 2 estão preenchidas alternadamente
    if (tabuleiro[0, 0] == tabuleiro[0, 2] && tabuleiro[0, 1] == tabuleiro[2, 0] && tabuleiro[0, 1] == tabuleiro[2, 2]) return true;
    
    // Verifica se colunas 0 e 2 estão preenchidas alternadamente
    if (tabuleiro[0, 0] == tabuleiro[2, 0] && tabuleiro[1, 0] == tabuleiro[0, 2] && tabuleiro[1, 0] == tabuleiro[2, 2]) return true;

    return false;
}