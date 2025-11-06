namespace Core;

public static class EntityFactory
{
    public static Entity CreateEmptyEntity(EntityID id)
    {
        switch (id)
        {
            case EntityID.Conveyor: return new Conveyor();
            case EntityID.Loader: return new Loader();
            case EntityID.Router: return new Router();
            case EntityID.Manufactory: return new Manufactory();
            case EntityID.Farm: return new Farm();
            case EntityID.Depot: return new Depot();
            default: throw new Exception($"Invalid entity ID: {id}");
        }
    }
    public static Entity CreateEmptyEntity(string id)
    {
        if (id == Entity.EntityIDStrings[(int)EntityID.Conveyor]) { return new Conveyor(); }
        if (id == Entity.EntityIDStrings[(int)EntityID.Loader]) { return new Loader(); }
        if (id == Entity.EntityIDStrings[(int)EntityID.Router]) { return new Router(); }
        if (id == Entity.EntityIDStrings[(int)EntityID.Manufactory]) { return new Manufactory(); }
        if (id == Entity.EntityIDStrings[(int)EntityID.Farm]) { return new Farm(); }
        if (id == Entity.EntityIDStrings[(int)EntityID.Depot]) { return new Depot(); }
        else { throw new Exception($"Invalid entity ID: {id}");}
    }
}