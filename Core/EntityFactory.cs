namespace Core;

public static class EntityFactory
{
    public static Entity CreateEmptyEntity(EntityID id)
    {
        switch (id)
        {
            case EntityID.Conveyor: return new Conveyor();
            case EntityID.Loader: return new Loader();
            case EntityID.Splitter: return new Splitter();
            case EntityID.Manufactory: return new Manufactory();
            case EntityID.Farm: return new Farm();
            case EntityID.Depot: return new Depot();
            default: throw new Exception($"Invalid entity ID: {id}");
        }
    }
}