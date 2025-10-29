using Core;

namespace Test;


public class Tests
{
    Instance instance;
    [SetUp]
    public void Setup()
    {
        instance = new Instance();
        instance.AddEntity(new Conveyor(0.5, [], new SplashKitSDK.Point2D { X = 6, Y = 7 }, OrientationID.East, 1));
        instance.AddEntity(new Conveyor(1.5, [], new SplashKitSDK.Point2D { X = 420, Y = 69 }, OrientationID.West, 1));
    }

    [Test]
    public void SaveTest()
    {
        string filename = "..\\src\\data\\saves\\test.txt";
        instance.saveToFile(filename);

        Assert.That(File.Exists(filename), Is.True);

        string[] lines = File.ReadAllLines(filename);
        List<string> expectedLines = new List<string>
        {
            "a",
            "b"
        };
        Assert.That(expectedLines, Is.EqualTo(lines));
    }
}
