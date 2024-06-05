Game game = new Game();
game.SetPlayerName();

class Player
{
    public int score = 0;
    public string name;

    public Player(string name)
    {
        this.name = name;
    }
}
class Dice
{
    private Random rnd = new Random();

    public int RandomNumber()
    {
        return rnd.Next(1, 7);
    }
}
class Game
{
    public void SetPlayerName()
    {
        Console.WriteLine("Kolik hráčů bude hrát");
        int playerCount = int.Parse(Console.ReadLine());
        Player[] players = new Player[playerCount];
        for (int i = 0; i < playerCount; i++)
        {
            Console.WriteLine("Zadejte Jméno hráče:");
            Player player = new Player(Console.ReadLine());
            players[i] = player;

        }
        Rounds(players);
    }
    public void Rounds(Player[] players)
    {
        Dice dice = new Dice();
        bool gameEnded = false;

        while (!gameEnded)
        {
            for (int i = 0; i < players.Length; i++)
            {
                Console.WriteLine("-------");
                Console.WriteLine($"Na tahu je {players[i].name}, zmáčkni enter pro zahrání");
                Console.WriteLine("-------");
                Console.ReadLine();

                bool playerTurn = true;
                int roundScore = 0;

                while (playerTurn && !gameEnded)
                {
                    int roll1 = dice.RandomNumber();
                    int roll2 = dice.RandomNumber();
                    Console.WriteLine($"{players[i].name} hodil {roll1} a {roll2}");

                    if (roll1 == 1 && roll2 == 1)
                    {
                        roundScore = 0;
                        playerTurn = false;
                    }
                    else if (roll1 == 1 || roll2 == 1)
                    {
                        playerTurn = false;
                    }
                    else
                    {
                        roundScore += roll1 + roll2;
                    }

                    Console.WriteLine($"{players[i].name} má skóre v tomto kole: {roundScore}");
                    System.Threading.Thread.Sleep(500);
                }

                players[i].score += roundScore;
                Console.WriteLine("-------");
                Console.WriteLine($"{players[i].name} má celkové skóre: {players[i].score}");

                if (players[i].score >= 100)
                {
                    Console.WriteLine($"{players[i].name} vyhrává se skóre {players[i].score}!");
                    gameEnded = true;
                    break;
                }
            }
        }
    }
}
