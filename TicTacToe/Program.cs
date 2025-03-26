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
        var jugadors = await PullInfo();
        if (jugadors is null) throw new Exception("No hi ha jugadors a la llist, null exception");
        var paisJugadors = ConverteixPresentacionsEnPaisJugadors(jugadors);
    }
    static async Task<List<string>?> PullInfo()
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
    static Dictionary<String, String> ConverteixPresentacionsEnPaisJugadors(List<string> presentacions)
    {
        const string patronJugador = @"participant\s([a-zA-Z'-]+\s[a-zA-Z'-]+)";
        const string patronPais = @"representa?[a-zA-Z]+\s[a-zA-Z]+\s([a-zA-Z]+)";
        
        Regex rgxJugador = new Regex(patronJugador);
        Regex rgxPais = new Regex(patronPais);
        
        var dictionary = new Dictionary<String, String>();
        presentacions.ForEach(x =>
        {
            var matchJugador = rgxJugador.Match(x);
            var matchPais = rgxPais.Match(x);
            
            var strJugador = matchJugador.Groups[1].Value;
            var strPais = matchPais.Groups[1].Value;
            dictionary.Add(strPais, strJugador);
            
            Console.WriteLine($"Jugador: {strJugador}, País: {strPais}");
        });
        
        return dictionary;
    }

    static async Task partidas(Dictionary<string,string> )
    {
        var totalPartidas = 10;
        var partida = 1;

        while (partida > totalPartidas)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetFromJsonAsync<UrlPartida>()
            }
        }
        
        Console.WriteLine(partida);
    }
}