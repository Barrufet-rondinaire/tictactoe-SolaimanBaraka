using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace TicTacToe;

static class Program
{
    private const string Url = "http://localhost:8080/";
    private const string UrlJugadors = Url + "jugadors/";
    private const string UrlPartida = Url + "partida/";
    static async Task Main(string[] args)
    {
        var jugadorsStr = await PullInfoJugadors();
        if (jugadorsStr is null) throw new Exception("No hi ha jugadors a la llist, null exception");
        
        var jugadors = ConverteixPresentacionsEnJugadors(jugadorsStr); 
        await generarPartidas(jugadors);
        jugadors.ForEach(x => Console.WriteLine(x.nombreJugador + ": " + x.partidasGanadas));
        
        var victories = jugadors.Max(j => j.partidasGanadas);
        var campeon = jugadors.Find(j => j.partidasGanadas == victories);
        
        Console.WriteLine("-----------Campeon------------");
        Console.WriteLine($"El campeón es: {campeon.nombreJugador} con {campeon.partidasGanadas} victorias.");
    }
    static async Task<List<string>?> PullInfoJugadors()
    {
        List<String>? contentList;
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(UrlJugadors);
            var content = await response.Content.ReadAsStringAsync();
            contentList = JsonSerializer.Deserialize<List<string>>(content);
        }
        return contentList;
    }
    static List<Jugador> ConverteixPresentacionsEnJugadors(List<string> presentacions)
    {
        const string patronJugador = @"participant\s([a-zA-Z'-]+\s[a-zA-Z'-]+)";
        const string patronPais = @"representa?[a-zA-Z]+\s[a-zA-Z]+\s([a-zA-Z]+)";
        
        Regex rgxJugador = new Regex(patronJugador);
        Regex rgxPais = new Regex(patronPais);
        
        var jugadors = new List<Jugador>();
        Console.WriteLine("-----------Lista de Jugadors------------");
        presentacions.ForEach(x =>
        {
            var matchJugador = rgxJugador.Match(x);
            var matchPais = rgxPais.Match(x);
            
            var strJugador = matchJugador.Groups[1].Value;
            var strPais = matchPais.Groups[1].Value;
            jugadors.Add(new Jugador(strJugador, strPais));
            
            Console.WriteLine($"Jugador: {strJugador}, País: {strPais}");
        });
        return jugadors;
    }
    static async Task generarPartidas(List<Jugador> jugadors)
    {
        using (var client = new HttpClient())
        {
            for (int i = 1; i < 10000; i++)
            {
                var partida = await client.GetFromJsonAsync<Partida>(UrlPartida + i);
                var ganador = partida.Ganador();
                if (ganador != "Empate")
                {
                    var jugadorGanador = jugadors.Find(x => x.nombreJugador == ganador);
                    if (jugadorGanador != null)
                    {
                        jugadorGanador.partidasGanadas++;
                    }
                }
            }
        }
        Console.WriteLine("-----------Resultados de la Competicion------------");
    }
}