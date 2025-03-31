namespace TicTacToe;

public class Partida
{
    public string jugador1 { get; set; }
    public string jugador2 { get; set; }
    public List<string> Tauler { get; set; }
    private string CaracterGanador()
    {
        // Líneas horizontales
        for (int i = 0; i < 3; i++)
        {
            if (Tauler[i][0] == Tauler[i][1] && Tauler[i][0] == Tauler[i][2] && Tauler[i][0] != '.')
                return Tauler[i][0].ToString();
        }
        // Líneas verticales
        for (int j = 0; j < 3; j++)
        {
            if (Tauler[0][j] == Tauler[1][j] && Tauler[0][j] == Tauler[2][j] && Tauler[0][j] != '.')
                return Tauler[0][j].ToString();
        }
        // Lineas Diagonales
        if (Tauler[0][0] == Tauler[1][1] && Tauler[0][0] == Tauler[2][2] && Tauler[0][0] != '.')
            return Tauler[0][0].ToString();
        if (Tauler[0][2] == Tauler[1][1] && Tauler[0][2] == Tauler[2][0] && Tauler[0][2] != '.')
            return Tauler[0][2].ToString();

        return "Empate";
    }

    public string Ganador()
    {
        const string X = "X";
        const string O = "O";
        var caracter = CaracterGanador();

        if (caracter == X) return jugador1;
        if (caracter == O) return jugador2;
        return "Empate";
    }
}