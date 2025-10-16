using SplashKitSDK;
namespace Core;

public class Program
{
    public static void Main()
    {
        Game game = new Game();
        game.Initialise();
        game.Run();
        game.Close();
    }
}