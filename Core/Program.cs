using SplashKitSDK;
namespace Core;

public class Program
{
    public static void Main()
    {
        Game.GameInstance.Run();
        Game.GameInstance.Close();
    }
}