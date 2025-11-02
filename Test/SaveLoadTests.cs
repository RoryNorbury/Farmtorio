using Core;

namespace Test;


public class SaveLoadTests
{
    string coreDirectory = "..\\..\\..\\..\\";
    Instance instance;
    [SetUp]
    public void Setup()
    {
        instance = new Instance();
        instance.Name = "test";
        instance.AddEntity(new Conveyor(0.5, [], new SplashKitSDK.Point2D { X = 6, Y = 7 }, OrientationID.East, 1));
        instance.AddEntity(new Conveyor(2.0/3.0, [], new SplashKitSDK.Point2D { X = 420, Y = 69 }, OrientationID.West, 1));
    }

    [Test]
    public void SaveTest()
    {
        string filename = coreDirectory + "\\src\\data\\saves\\test.txt";
        instance.SaveToFile(filename);

        Assert.That(File.Exists(filename), Is.True);

        string[] lines = File.ReadAllLines(filename);
        List<string> expectedLines = new List<string>
        {
            "test,0",
            "Conveyor,6,7,1,1,0.5",
            "Conveyor,420,69,3,1,0.6666666666666666"

        };
        Assert.That(expectedLines, Is.EqualTo(lines));
    }
    [Test]
    public void LoadTest()
    {
        string filename = coreDirectory + "\\src\\data\\saves\\test.txt";
        instance.SaveToFile(filename);

        Instance loadedInstance = new Instance(filename);

        Assert.That(loadedInstance.Name, Is.EqualTo("test"));
        Assert.That(loadedInstance.Cycle, Is.EqualTo(0));
        Assert.That(loadedInstance.DrawableEntities().Count, Is.EqualTo(2));

        Conveyor conveyor1 = (Conveyor)loadedInstance.DrawableEntities()[0];
        Assert.That(conveyor1.Position.X, Is.EqualTo(6.0));
        Assert.That(conveyor1.Position.Y, Is.EqualTo(7.0));
        Assert.That(conveyor1.Orientation, Is.EqualTo(OrientationID.East));
        Assert.That(conveyor1.TextureIndex, Is.EqualTo(1));
        Assert.That(conveyor1.Speed, Is.EqualTo(0.5));

        Conveyor conveyor2 = (Conveyor)loadedInstance.DrawableEntities()[1];
        Assert.That(conveyor2.Position.X, Is.EqualTo(420));
        Assert.That(conveyor2.Position.Y, Is.EqualTo(69));
        Assert.That(conveyor2.Orientation, Is.EqualTo(OrientationID.West));
        Assert.That(conveyor2.TextureIndex, Is.EqualTo(1));
        Assert.That(conveyor2.Speed, Is.EqualTo(2.0/3.0));
    }
}
