namespace TicTacToe;

public class Jugador
{
    public string nombreJugador {get; set;}
    private string pais {get; set;}
    public int partidasGanadas {get; set;}
    
    public Jugador(string nombreJugador, string pais)
    {
        this.nombreJugador = nombreJugador;
        this.pais = pais;
        partidasGanadas = 0;
    }
}